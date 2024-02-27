using IkemenToolbox.Extensions;
using IkemenToolbox.Models;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IkemenToolbox.Services
{
    public class FighterWriter
    {
        private readonly Fighter _fighter;
        private readonly StringBuilder _builder = new();
        static readonly PropertyInfo[] _properties = typeof(Fighter).GetProperties();
        public FighterWriter(Fighter fighter) => _fighter = fighter;
        public void WriteKeyValue(string propertyName, string key = null, bool isString = false)
        {
            var property = propertyName.GetPropertyInfo(_properties);
            var value = property.GetValue(_fighter)?.ToString();

            if (value == null)
            {
                return;
            }

            if (isString)
            {
                value = $"\"{value}\"";
            }

            if (key == null)
            {
                key = propertyName.Replace('_', '.').ToLower();
            }

            if (property != null)
            {
                _builder.AppendKeyValue(key, value);
            }
        }

        private async Task ExportFileAsync(string name)
        {
            var filePath = _fighter.FolderPath + name;

#if DEBUG
            var samplePath = _fighter.FolderPath + "IkemenToolbox_Sample\\";
            if (!Directory.Exists(samplePath))
            {
                Directory.CreateDirectory(samplePath);
            }
            filePath = samplePath + name;
#endif

            await File.WriteAllTextAsync(filePath, _builder.ToString());
        }

        public async Task WriteDefAsync()
        {
            _builder.AppendSection(SectionType.Info);

            WriteKeyValue(nameof(_fighter.Name), isString: true);
            WriteKeyValue(nameof(_fighter.DisplayName), isString: true);
            WriteKeyValue(nameof(_fighter.VersionDate));
            WriteKeyValue(nameof(_fighter.MugenVersion));
            WriteKeyValue(nameof(_fighter.Author), isString: true);
            WriteKeyValue(nameof(_fighter.Pal_Defaults));
            WriteKeyValue(nameof(_fighter.LocalCoord));

            _builder.AppendLine();
            _builder.AppendSection(SectionType.Files);

            WriteKeyValue(nameof(_fighter.Cmd));
            WriteKeyValue(nameof(_fighter.Cns));
            WriteKeyValue(nameof(_fighter.Sprite));
            WriteKeyValue(nameof(_fighter.Anim));
            WriteKeyValue(nameof(_fighter.Sound));
            WriteKeyValue(nameof(_fighter.Ai));
            WriteKeyValue(nameof(_fighter.MoveList));
            WriteKeyValue(nameof(_fighter.StCommon));
            for (var i = 0; i < _fighter.StFiles.Count; i++)
            {
                _builder.AppendKeyValue("st" + i, _fighter.StFiles[i]);
            }

            _builder.AppendLine();
            _builder.AppendSection(SectionType.Palette_Keymap);

            WriteKeyValue(nameof(_fighter.X));
            WriteKeyValue(nameof(_fighter.Y));
            WriteKeyValue(nameof(_fighter.Z));
            WriteKeyValue(nameof(_fighter.A));
            WriteKeyValue(nameof(_fighter.B));
            WriteKeyValue(nameof(_fighter.C));
            WriteKeyValue(nameof(_fighter.X2));
            WriteKeyValue(nameof(_fighter.Y2));
            WriteKeyValue(nameof(_fighter.Z2));
            WriteKeyValue(nameof(_fighter.A2));
            WriteKeyValue(nameof(_fighter.B2));
            WriteKeyValue(nameof(_fighter.C2));

            _builder.AppendLine();
            _builder.AppendSection(SectionType.Arcade);

            WriteKeyValue(nameof(_fighter.Intro_Storyboard));
            WriteKeyValue(nameof(_fighter.Ending_Storyboard));

            await ExportFileAsync(Path.GetFileName(_fighter.DefinitionPath));
        }

        public async Task WriteCnsAsync()
        {
            _builder.AppendSection(SectionType.Data);

            WriteKeyValue(nameof(_fighter.Life));
            WriteKeyValue(nameof(_fighter.Attack));
            WriteKeyValue(nameof(_fighter.Defence));
            WriteKeyValue(nameof(_fighter.Fall_Defence_Up), "fall.defence_up");
            WriteKeyValue(nameof(_fighter.LieDown_Time));
            WriteKeyValue(nameof(_fighter.AirJuggle));

            WriteKeyValue(nameof(_fighter.SparkNo));
            WriteKeyValue(nameof(_fighter.Guard_SparkNo));

            WriteKeyValue(nameof(_fighter.KO_Echo), "KO.echo");
            WriteKeyValue(nameof(_fighter.Volume));

            WriteKeyValue(nameof(_fighter.IntPersistIndex), "IntPersistIndex");
            WriteKeyValue(nameof(_fighter.FloatPersistIndex), "FloatPersistIndex");

            _builder.AppendLine();
            _builder.AppendSection(SectionType.Size);

            WriteKeyValue(nameof(_fighter.XScale));
            WriteKeyValue(nameof(_fighter.YScale));

            WriteKeyValue(nameof(_fighter.Ground_Back));
            WriteKeyValue(nameof(_fighter.Ground_Front));
            WriteKeyValue(nameof(_fighter.Air_Back));
            WriteKeyValue(nameof(_fighter.Air_Front));

            WriteKeyValue(nameof(_fighter.Height));
            WriteKeyValue(nameof(_fighter.Attack_Dist));

            WriteKeyValue(nameof(_fighter.Proj_Attack_Dist));
            WriteKeyValue(nameof(_fighter.Proj_DoScale));

            WriteKeyValue(nameof(_fighter.Head_Pos));
            WriteKeyValue(nameof(_fighter.Mid_Pos));

            WriteKeyValue(nameof(_fighter.ShadowOffset));
            WriteKeyValue(nameof(_fighter.Draw_Offset));

            _builder.AppendLine();
            _builder.AppendSection(SectionType.Velocity);

            WriteKeyValue(nameof(_fighter.Walk_Fwd));
            WriteKeyValue(nameof(_fighter.Walk_Back));
            WriteKeyValue(nameof(_fighter.Run_Fwd));
            WriteKeyValue(nameof(_fighter.Run_Back));

            WriteKeyValue(nameof(_fighter.Jump_Neu));
            WriteKeyValue(nameof(_fighter.Jump_Back));
            WriteKeyValue(nameof(_fighter.Jump_Fwd));

            WriteKeyValue(nameof(_fighter.RunJump_Back));
            WriteKeyValue(nameof(_fighter.RunJump_Fwd));

            WriteKeyValue(nameof(_fighter.AirJump_Neu));
            WriteKeyValue(nameof(_fighter.AirJump_Back));
            WriteKeyValue(nameof(_fighter.AirJump_Fwd));

            WriteKeyValue(nameof(_fighter.Air_GetHit_GroundRecover));
            WriteKeyValue(nameof(_fighter.Air_GetHit_AirRecover_Mul));
            WriteKeyValue(nameof(_fighter.Air_GetHit_AirRecover_Add));

            WriteKeyValue(nameof(_fighter.Air_GetHit_AirRecover_Back));
            WriteKeyValue(nameof(_fighter.Air_GetHit_AirRecover_Fwd));
            WriteKeyValue(nameof(_fighter.Air_GetHit_AirRecover_Up));
            WriteKeyValue(nameof(_fighter.Air_GetHit_AirRecover_Down));

            _builder.AppendLine();
            _builder.AppendSection(SectionType.Movement);

            WriteKeyValue(nameof(_fighter.AirJump_Num));
            WriteKeyValue(nameof(_fighter.AirJump_Height));
            WriteKeyValue(nameof(_fighter.YAccel));

            WriteKeyValue(nameof(_fighter.Stand_Friction));
            WriteKeyValue(nameof(_fighter.Crouch_Friction));
            WriteKeyValue(nameof(_fighter.Stand_Friction_Threshold));
            WriteKeyValue(nameof(_fighter.Crouch_Friction_Threshold));

            WriteKeyValue(nameof(_fighter.Air_GetHit_GroundLevel));
            WriteKeyValue(nameof(_fighter.Air_GetHit_GroundRecover_Ground_Threshold));
            WriteKeyValue(nameof(_fighter.Air_GetHit_GroundRecover_GroundLevel));

            WriteKeyValue(nameof(_fighter.Air_GetHit_AirRecover_Threshold));
            WriteKeyValue(nameof(_fighter.Air_GetHit_AirRecover_YAccel));
            WriteKeyValue(nameof(_fighter.Air_GetHit_Trip_GroundLevel));

            WriteKeyValue(nameof(_fighter.Down_Bounce_Offset));
            WriteKeyValue(nameof(_fighter.Down_Bounce_YAccel));
            WriteKeyValue(nameof(_fighter.Down_Bounce_GroundLevel));
            WriteKeyValue(nameof(_fighter.Down_Friction_Threshold));

            _builder.AppendLine();
            _builder.AppendSection(SectionType.Quotes);

            for (var i = 0; i < _fighter.Quotes.Count; i++)
            {
                _builder.AppendKeyValue("victory" + (i+1), _fighter.Quotes[i], addQuotes: true);
            }

            _builder.AppendLine();
            _builder.AppendSection(SectionType.Quotes, language:"ja");


            for (var i = 0; i < _fighter.Ja_Quotes.Count; i++)
            {
                _builder.AppendKeyValue("victory" + (i+1), _fighter.Ja_Quotes[i], addQuotes: true);
            }

            await ExportFileAsync(_fighter.Cns);
        }
    }
}
