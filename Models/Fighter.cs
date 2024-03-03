using CommunityToolkit.Mvvm.ComponentModel;
using IkemenToolbox.Extensions;
using IkemenToolbox.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkemenToolbox.Models
{
    public partial class Fighter : ObservableObject
    {
        [ObservableProperty] private string _definitionPath;
        [ObservableProperty] private string _exportPath;
        public string FolderPath { get; set; }

        #region def

        #region Info
        [Display(Description = "Name of character")]
        [ObservableProperty] private string _name;
        [Display(Description = "Name of character to display")]
        [ObservableProperty] private string _displayName;
        [Display(Description = "Version of character (MM-DD-YYYY or X.XX)")]
        [ObservableProperty] private string _versionDate;
        [Display(Description = "Version of M.U.G.E.N character works on (X.XX)")]
        [ObservableProperty] private string _mugenVersion;
        [Display(Description = "Character author name")]
        [ObservableProperty] private string _author;
        [Display(Description = "Default palettes in order of preference (up to 4)\nNumbering starts from 1")]
        [ObservableProperty] private string _pal_Defaults;
        [Display(Description = "Local coordinate space width and height")]
        [ObservableProperty] private string _localCoord;
        #endregion

        #region Files
        [Display(Description = "TBA")]
        [ObservableProperty] private string _cmd;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _cns;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _stCommon;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _sprite;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _anim;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _sound;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _ai;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _moveList;
        [ObservableProperty] private ObservableCollection<string> _stFiles = new();
        #endregion

        #region Palette Keymap
        [Display(Description = "TBA")]
        [ObservableProperty] private string _x;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _y;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _z;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _a;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _b;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _c;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _x2;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _y2;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _z2;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _a2;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _b2;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _c2;
        #endregion

        #region Arcade
        [Display(Description = "TBA")]
        [ObservableProperty] private string _intro_Storyboard;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _ending_Storyboard;
        #endregion

        #endregion .def

        #region cns

        #region Data
        
        [Display(Description = 
            "Amount of life the character starts with.\r\n" +
            "Default = 1000\r\n\r\n" +
            "You usually don't need to change this, but for characters from some games you'll have to do some math.\r\n\r\n" +
            "Let's take CvS2 Sakura as example, she has 13600 life points when the average value is 14400. To recreate that in Mugen you'll have to do this calculation:\r\n\r\n" +
            "(Char's Game Life) * (Mugen's Average Life) / (Game's Average Life)\r\n\r\n" +
            "In Sakura's case, equals to:\r\n" +
            "13600 * 1000 / 14400 = 944.444...\r\n" +
            "So you'd use 944 or 945 as the life value, since Mugen doesn't accept floats there.")]
        [ObservableProperty] private int _life;
        [Display(Description = "TBA")]
        [ObservableProperty] private int _attack;
        [Display(Description = "TBA")]
        [ObservableProperty] private int _defence;
        [Display(Description = "TBA")]
        [ObservableProperty] private int _fall_Defence_Up;
        [Display(Description = "TBA")]
        [ObservableProperty] private int _lieDown_Time;
        [Display(Description = "TBA")]
        [ObservableProperty] private int _airJuggle;
        [Display(Description = "TBA")]
        [ObservableProperty] private int _sparkNo;
        [Display(Description = "TBA")]
        [ObservableProperty] private int _guard_SparkNo;
        [Display(Description = "TBA")]
        [ObservableProperty] private int _kO_Echo;
        [Display(Description = "TBA")]
        [ObservableProperty] private int _volume;
        [Display(Description = "TBA")]
        [ObservableProperty] private int _intPersistIndex;
        [Display(Description = "TBA")]
        [ObservableProperty] private int _floatPersistIndex;
        #endregion

        #region Size
        
        [Display(Description = "TBA")]
        [ObservableProperty] private int _xScale;
        [Display(Description = "TBA")]
        [ObservableProperty] private int _yScale;
        [Display(Description = "TBA")]
        [ObservableProperty] private int _ground_Back;
        [Display(Description = "TBA")]
        [ObservableProperty] private int _ground_Front;
        [Display(Description = "TBA")]
        [ObservableProperty] private int _air_Back;
        [Display(Description = "TBA")]
        [ObservableProperty] private int _air_Front;
        [Display(Description = "TBA")]
        [ObservableProperty] private int _height;
        [Display(Description = "TBA")]
        [ObservableProperty] private int _attack_Dist;
        [Display(Description = "TBA")]
        [ObservableProperty] private int _proj_Attack_Dist;
        [Display(Description = "TBA")]
        [ObservableProperty] private int _proj_DoScale;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _head_Pos;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _mid_Pos;
        [Display(Description = "TBA")]
        [ObservableProperty] private int _shadowOffset;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _draw_Offset;
        #endregion

        #region Velocity
        [Display(Description = "TBA")]
        [ObservableProperty] private string _walk_Fwd;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _walk_Back;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _run_Fwd;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _run_Back;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _jump_Neu;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _jump_Back;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _jump_Fwd;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _runJump_Back;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _runJump_Fwd;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _airJump_Neu;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _airJump_Back;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _airJump_Fwd;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _air_GetHit_GroundRecover;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _air_GetHit_AirRecover_Mul;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _air_GetHit_AirRecover_Add;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _air_GetHit_AirRecover_Back;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _air_GetHit_AirRecover_Fwd;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _air_GetHit_AirRecover_Up;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _air_GetHit_AirRecover_Down;
        [Display(Description = "TBA")]
        #endregion

        #region Movement
        [ObservableProperty] private string _airJump_Num;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _airJump_Height;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _yAccel;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _stand_Friction;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _crouch_Friction;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _stand_Friction_Threshold;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _crouch_Friction_Threshold;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _air_GetHit_GroundLevel;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _air_GetHit_GroundRecover_Ground_Threshold;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _air_GetHit_GroundRecover_GroundLevel;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _air_GetHit_AirRecover_Threshold;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _air_GetHit_AirRecover_YAccel;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _air_GetHit_Trip_GroundLevel;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _down_Bounce_Offset;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _down_Bounce_YAccel;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _down_Bounce_GroundLevel;
        [Display(Description = "TBA")]
        [ObservableProperty] private string _down_Friction_Threshold;
        #endregion

        public ObservableCollection<string> Quotes { get; set; } = new();
        public ObservableCollection<string> Ja_Quotes { get; set; } = new();

        #endregion

        #region cmd
        public ObservableCollection<StringStringKeyValue> Defaults { get; set; } = new();
        public ObservableCollection<StringStringKeyValue> Remaps { get; set; } = new();
        public ObservableCollection<CommandDefinition> CommandDefinitions { get; } = new();
        #endregion

        public ObservableCollection<StateDefinition> StateDefinitions { get; } = new();
        [ObservableProperty] private StateDefinition _entryStateDefinition;

        private async Task<string> ReadFileAsync(string shortFilePath)
        {
            var filePath = FolderPath + shortFilePath;

            if (!File.Exists(filePath))
            {
                throw new InvalidDataException("No file exists at " + filePath);
            }

            return await File.ReadAllTextAsync(filePath);
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
            FolderPath = Path.GetDirectoryName(definitionPath) + '\\';

#if DEBUG
            ExportPath = FolderPath + "IkemenToolbox_Sample\\";
            if (!Directory.Exists(ExportPath))
            {
                Directory.CreateDirectory(ExportPath);
            }
#endif

            Parse(await File.ReadAllTextAsync(definitionPath));
            await Task.WhenAll(
                ParseFileAsync(Cmd),
                ParseFileAsync(Cns)
            );

            EntryStateDefinition = StateDefinitions.FirstOrDefault(x => x.Id == -1);
            StateDefinitions.Remove(EntryStateDefinition);
        }

        private async Task ParseFileAsync(string shortFilePath, params string[] ignoredSections) => Parse(await ReadFileAsync(shortFilePath), ignoredSections);
        private void Parse(string data, params string[] ignoredSections)
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
                        case SectionType.Info:
                        case SectionType.Arcade:
                        case SectionType.Palette_Keymap:
                        case SectionType.Data:
                        case SectionType.Size:
                        case SectionType.Velocity:
                        case SectionType.Movement:
                            PropertyHelper.SetValue(this, key, value);
                            break;
                        case SectionType.Files:
                            if (key.StartsWith("st") && key.Length <= 4)
                            {
                                StFiles.Add(value);
                                continue;
                            }
                            PropertyHelper.SetValue(this, key, value);
                            break;
                        case SectionType.Remap: Remaps.Add(new StringStringKeyValue(key, value)); break;
                        case SectionType.Defaults: Defaults.Add(new StringStringKeyValue(key, value)); break;
                        case SectionType.Quotes: Quotes.Add(value.Trim('"')); break;
                        case SectionType.Ja_Quotes: Ja_Quotes.Add(value.Trim('"')); break;
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
                var onLastLine = i == last;

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

        public async Task ExportDefinitionAsync()
        {
            var fileName = Path.GetFileName(DefinitionPath);
            var builder = new StringBuilder();

            builder.AppendSection(SectionType.Info);
            builder.AppendKeyValue("name", Name, true);
            builder.AppendKeyValue("displayname", DisplayName, true);
            builder.AppendKeyValue("versiondate", VersionDate);
            builder.AppendKeyValue("mugenversion", MugenVersion);
            builder.AppendKeyValue("author", Author, true);
            builder.AppendKeyValue("pal.defaults", Pal_Defaults);
            builder.AppendKeyValue("localcoord", LocalCoord);
            builder.AppendLine();

            builder.AppendSection(SectionType.Files);
            builder.AppendKeyValue(CommonFile.cmd, Cmd);
            builder.AppendKeyValue(CommonFile.cns, Cns);
            builder.AppendKeyValue(CommonFile.sprite, Sprite);
            builder.AppendKeyValue(CommonFile.anim, Anim);
            builder.AppendKeyValue(CommonFile.sound, Sound);
            builder.AppendKeyValue(CommonFile.ai, Ai);
            builder.AppendKeyValue(CommonFile.movelist, MoveList);
            builder.AppendKeyValue(CommonFile.stcommon, StCommon);
            for (var i = 0; i < StFiles.Count; i++)
            {
                builder.AppendKeyValue("st" + i, StFiles[i]);
            }
            builder.AppendLine();

            builder.AppendSection(SectionType.Palette_Keymap);
            builder.AppendKeyValue("x", X);
            builder.AppendKeyValue("y", Y);
            builder.AppendKeyValue("z", Z);
            builder.AppendKeyValue("a", A);
            builder.AppendKeyValue("b", B);
            builder.AppendKeyValue("c", C);
            builder.AppendKeyValue("x2", X2);
            builder.AppendKeyValue("y2", Y2);
            builder.AppendKeyValue("z2", Z2);
            builder.AppendKeyValue("a2", A2);
            builder.AppendKeyValue("b2", B2);
            builder.AppendKeyValue("c2", C2);
            builder.AppendLine();

            builder.AppendSection(SectionType.Arcade);
            builder.AppendKeyValue("intro.storyboard", Intro_Storyboard);
            builder.AppendKeyValue("ending.storyboard", Ending_Storyboard);

            await ExportFileAsync(fileName, builder.ToString());
        }

        private async Task ExportFileAsync(string name, string data)
        {
            var folderPath = !string.IsNullOrWhiteSpace(ExportPath) ? ExportPath : FolderPath;
            var filePath = folderPath.Trim('\\') + '\\' +  name;

            await File.WriteAllTextAsync(filePath, data);
        }
    }
}