namespace IkemenToolbox.Models
{
    public class Size
    {
        public int XScale { get; set; }
        public int YScale { get; set; }
        public int Ground_Back { get; set; }
        public int Ground_Front { get; set; }
        public int Air_Back { get; set; }
        public int Air_Front { get; set; }
        public int Height { get; set; }
        public int Attack_Dist { get; set; }
        public int Proj_Attack_Dist { get; set; }
        public int Proj_DoScale { get; set; }
        public Position Head_Pos { get; set; }
        public Position Mid_Pos { get; set; }
        public int ShadowOffset { get; set; }
        public Position Draw_Offset { get; set; }
    }
}