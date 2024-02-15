using CommunityToolkit.Mvvm.ComponentModel;
using IkemenToolbox.Extensions;
using IkemenToolbox.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IkemenToolbox.Models
{
    public partial class Fighter : ObservableObject
    {
        #region Display Values

        public string DisplayName => Info.TryGetValue("displayname");
        public StateDefinition EntryStateDefinition => StateDefinitions.FirstOrDefault(x => x.Id == -1);

        private void RaiseDisplayPropertiesChanged()
        {
            OnPropertyChanged(nameof(DisplayName));
            OnPropertyChanged(nameof(EntryStateDefinition));
        }

        #endregion Display Values

        public string DefinitionPath { get; set; }
        public string FolderPath { get; set; }

        #region .def

        [ObservableProperty]
        private Dictionary<string, string> _info = new();

        [ObservableProperty]
        private Dictionary<string, string> _files = new();

        [ObservableProperty]
        private Dictionary<string, string> _arcade = new();

        #endregion .def

        #region .cns

        [ObservableProperty]
        private Dictionary<string, string> _remaps = new();

        [ObservableProperty]
        private Dictionary<string, string> _defaults = new();

        public ObservableCollection<CommandDefinition> CommandDefinitions { get; } = new();

        #endregion .cns

        public ObservableCollection<StateDefinition> StateDefinitions { get; } = new();

        private async Task<string> ReadFileAsync(string key)
        {
            if (!Files.ContainsKey(key))
            {
                throw new InvalidDataException("No file under the alias: " + key);
            }

            return await File.ReadAllTextAsync(FolderPath + Files[key]);
        }

        private static string[] SplitData(string data)
        {
            // Remove empty lines
            var split = data.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            for (var i = split.Count - 1; i >= 0; i--)
            {
                // Remove comments
                if (split[i].StartsWith(';'))
                {
                    split.RemoveAt(i);
                    continue;
                }

                // Remove inline comments
                var index = split[i].IndexOf(';');
                if (index != -1)
                {
                    split[i] = split[i][..index];
                }

                split[i].Trim();
            }

            return split.ToArray();
        }

        internal async Task InitializeAsync(string definitionPath)
        {
            DefinitionPath = definitionPath;
            FolderPath = Path.GetDirectoryName(definitionPath) + "/";
            Parse(await File.ReadAllTextAsync(definitionPath));
            await Task.WhenAll(
                ParseFileAsync("cmd"),
                ParseFileAsync("cns")
            );

            RaiseDisplayPropertiesChanged();
        }

        private async Task ParseFileAsync(string fileName, params SectionType[] ignoredSections) => Parse(await ReadFileAsync(fileName), ignoredSections);
        private void Parse(string data, params SectionType[] ignoredSections)
        {
            var dataArray = SplitData(data);
            var last = dataArray.Length - 1;

            var command = new InputCommand();
            List<InputCommand> commands = null;

            var state = new State();
            var triggers = new List<KeyValuePair<string, string>>();

            Section section = null;
            StateDefinition stateDefinition = null;

            for (var i = 0; i < dataArray.Length; i++)
            {
                var line = dataArray[i];

                if (line.TryGetKeyValue(out var key, out var value))
                {
                    switch (section.Type)
                    {
                        case SectionType.Info: Info.Add(key, value); break;
                        case SectionType.Files: Files.Add(key, value); break;
                        case SectionType.Remap: Remaps.Add(key, value); break;
                        case SectionType.Defaults: Defaults.Add(key, value); break;

                        case SectionType.Command:
                            PropertyHelper.SetValue(command, key, value);
                            break;

                        case SectionType.State:
                            if (key.StartsWith("trigger"))
                            {
                                triggers.Add(new(key, value));
                            }
                            else
                            {
                                PropertyHelper.SetValue(state, key, value);
                            }
                            break;

                        case SectionType.Statedef:
                            PropertyHelper.SetValue(stateDefinition, key, value);
                            break;
                    }
                }

                Section nextSection = null;
                var next = i + 1;

                if (i == 0 && line.TryGetSection(out nextSection))
                {
                    section = nextSection;
                }
                else if (i == last || dataArray[next].TryGetSection(out nextSection))
                {
                    if (section != null)
                    {
                        switch (section.Type)
                        {
                            case SectionType.Command:
                                commands ??= new List<InputCommand>();
                                commands.Add(command);
                                command = new();
                                break;

                            case SectionType.State:
                                if (triggers != null)
                                {
                                    triggers.GroupBy(x => x.Key)
                                        .ToList()
                                        .ForEach(group => state.Triggers.Add(new Trigger(GetTriggerNum(group.Key), new ObservableCollection<string>(group.Select(trigger => trigger.Value)))));
                                    triggers = new();
                                }

                                state.Name = section.Name;
                                stateDefinition.States.Add(state);

                                state = new State();
                                break;
                        }
                    }

                    if (i == last && stateDefinition != null)
                    {
                        StateDefinitions.Add(stateDefinition);
                    }
                    else if (nextSection?.Type == SectionType.Statedef)
                    {
                        if (stateDefinition != null)
                        {
                            StateDefinitions.Add(stateDefinition);
                        }
                        stateDefinition = new StateDefinition((int)nextSection.Id, nextSection.Name);
                    }

                    if (i == last && commands != null)
                    {
                        var tempCommands = commands.DistinctBy(x => x.Name).ToList();
                        foreach (var tempCommand in tempCommands)
                        {
                            CommandDefinitions.Add(new CommandDefinition
                            {
                                Name = tempCommand.Name.Trim('"'),
                                Commands = new ObservableCollection<InputCommand>(commands.Where(x => x.Name == tempCommand.Name).ToList()),
                            });
                        }
                    }

                    section = nextSection;
                }
            }
        }

        private void AddState(int id, string name, State state)
        {
            var collection = StateDefinitions.FirstOrDefault(x => x.Name == name);
            if (collection == null)
            {
                collection = new StateDefinition(id, name);
                StateDefinitions.Add(collection);
            }
            collection.States.Add(state);
        }

        private static int GetTriggerNum(string key)
        {
            key = key.Replace("trigger", "");

            if (int.TryParse(key, out var num))
            {
                return num;
            }

            return 0;
        }
    }
}