using CommunityToolkit.Mvvm.ComponentModel;
using IkemenToolbox.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IkemenToolbox.Models
{
    public partial class Fighter : ObservableObject
    {
        #region Display Values

        public string DisplayName => Info.TryGetValue("displayname");

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

        [ObservableProperty]
        private Dictionary<string, string> _remaps = new();

        [ObservableProperty]
        private Dictionary<string, int> _defaults = new();

        [ObservableProperty]
        private ObservableCollection<InputCommand> _commands = new();

        [ObservableProperty]
        private ObservableCollection<StateDefinition> _stateDefinitions = new();

        [ObservableProperty]
        private ObservableCollection<State> _entryStates = new();

        private async Task<string> ReadFileAsync(string key)
        {
            if (Files.ContainsKey(key))
            {
                return await File.ReadAllTextAsync(FolderPath + Files[key]);
            }

            return null;
        }

        private string[] SplitData(string data)
        {
            var split = data.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            // Remove comments
            for (var i = 0; i < split.Length; i++)
            {
                var index = split[i].IndexOf(';');
                if (index != -1)
                {
                    split[i] = split[i].Substring(0, index);
                }
            }

            return split;
        }

        internal async Task InitializeAsync(string definitionPath)
        {
            DefinitionPath = definitionPath;
            FolderPath = Path.GetDirectoryName(definitionPath) + "/";
            ParseDef(await File.ReadAllTextAsync(definitionPath));
            await Task.WhenAll(
                PopulateCommandsAsync(),
                PopulateConstantsAsync()
            );

            RaiseDisplayPropertiesChanged();
        }

        private void RaiseDisplayPropertiesChanged()
        {
            OnPropertyChanged(nameof(DisplayName));
        }

        private void ParseDef(string data)
        {
            foreach (var line in SplitData(data))
            {
                if (line.StartsWith(";") || line.StartsWith("["))
                {
                    continue;
                }

                var keyValue = line.Split('=');

                if (keyValue.Length != 2)
                {
                    continue;
                }

                var key = keyValue[0].Trim();
                var value = keyValue[1].Trim();

                switch (key)
                {
                    case "name":
                    case "displayname":
                    case "versiondate":
                    case "mugenversion":
                    case "author":
                    case "pal.defaults":
                    case "localCoord":
                        Info.Add(key, value);
                        break;

                    case "sprite":
                    case "anim":
                    case "sound":
                    case "cmd":
                    case "cns":
                    case "stcommon":
                    case "st":
                    case "st0":
                    case "st1":
                    case "st2":
                    case "st3":
                    case "st4":
                    case "st5":
                    case "st6":
                    case "st7":
                    case "st8":
                    case "st9":
                        Files.Add(key, value);
                        break;
                }
            }
        }

        private async Task PopulateConstantsAsync()
        {
            var data = await ReadFileAsync("cns");
            var dataArray = SplitData(data);

            Section currentSection = null;
            for (var i = 0; i < dataArray.Length; i++)
            {
                var line = dataArray[i].Trim();

                if (line.TryGetSection(out var section))
                {
                    currentSection = section;
                    continue;
                }

                var keyValue = line.Split('=');

                if (keyValue.Length < 2)
                {
                    continue;
                }

                var key = keyValue[0].Trim();
                var value = keyValue[1].Trim();

                switch (currentSection.Type)
                {
                    case SectionType.Remap:
                        Remaps.Add(key, value);
                        break;

                    case SectionType.Defaults:
                        Defaults.Add(key, int.Parse(value));
                        break;

                    case SectionType.State:
                        if (int.TryParse(key, out var id))
                        {
                            AddState(id, currentSection.Name, new State { Name = key, Value = value });
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Parse the cmd file and populate the commands and entry states (Statedef -1).
        /// </summary>
        /// <returns></returns>
        private async Task PopulateCommandsAsync()
        {
            var data = await ReadFileAsync("cmd");
            var dataArray = SplitData(data);

            var command = new InputCommand();
            var entryState = new State();
            List<KeyValuePair<string, string>> triggers = null;
            Section currentSection = null;

            for (var i = 0; i < dataArray.Length; i++)
            {
                var line = dataArray[i].Trim();

                if (line.TryGetSection(out var section))
                {
                    if (currentSection != null)
                    {
                        switch (currentSection.Type)
                        {
                            case SectionType.Command:
                                Commands.Add(command);
                                command = new InputCommand();
                                break;

                            case SectionType.State:
                                if (triggers != null)
                                {
                                    triggers.GroupBy(x => x.Key)
                                        .ToList()
                                        .ForEach(group => entryState.Triggers.Add(new Trigger(GetTriggerNum(group.Key), new ObservableCollection<string>(group.Select(trigger => trigger.Value)))));
                                    triggers = null;
                                }

                                entryState.Name = section.Name;
                                EntryStates.Add(entryState);

                                entryState = new State();
                                break;
                        }
                    }

                    currentSection = section;
                    continue;
                }

                var keyValue = line.Split('=', 2);

                if (keyValue.Length < 2)
                {
                    continue;
                }

                var key = keyValue[0].Trim();
                var value = keyValue[1].Trim();

                switch (currentSection.Type)
                {
                    case SectionType.Remap:
                        Remaps.Add(key, value);
                        break;

                    case SectionType.Defaults:
                        Defaults.Add(key, int.Parse(value));
                        break;

                    case SectionType.Command:
                        switch (key)
                        {
                            case "name":
                                command.Name = value;
                                break;

                            case "command":
                                command.Command = value.Split(',');
                                break;

                            case "time":
                                command.Time = int.Parse(value);
                                break;
                        }
                        break;

                    case SectionType.State:
                        switch (key)
                        {
                            case "type":
                                entryState.Type = value;
                                break;

                            case "value":
                                entryState.Value = value;
                                break;

                            default:
                                triggers ??= new List<KeyValuePair<string, string>>();
                                if (key.StartsWith("trigger"))
                                {
                                    triggers.Add(new KeyValuePair<string, string>(key, value));
                                }
                                break;
                        }
                        break;
                }

                if (i == dataArray.Length - 1)
                {
                    switch (currentSection.Type)
                    {
                        case SectionType.Command:
                            Commands.Add(command);
                            break;

                        case SectionType.State:
                            EntryStates.Add(entryState);
                            break;
                    }
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