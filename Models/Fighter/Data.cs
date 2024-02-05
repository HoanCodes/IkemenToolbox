namespace IkemenToolbox.Models
{
    public class Data
    {
        /// <summary>
        /// Amount of life to start with
        /// </summary>
        public int Life { get; set; }

        /// <summary>
        /// Attack power (more is stronger)
        /// </summary>
        public int Attack { get; set; }

        /// <summary>
        /// Defensive power (more is stronger)
        /// </summary>
        public int Defence { get; set; }

        /// <summary>
        /// Percentage to increase defense everytime player is knocked down
        /// </summary>
        public int Fall_DefenceUp { get; set; }

        /// <summary>
        /// Time which player lies down for, before getting up
        /// </summary>
        public int LieDown_Time { get; set; }

        /// <summary>
        /// Number of points for juggling
        /// </summary>
        public int AirJuggle { get; set; }

        /// <summary>
        /// Default hit spark number for HitDefs
        /// </summary>
        public int SparkNo { get; set; }

        /// <summary>
        /// Default guard spark number
        /// </summary>
        public int Guard_SparkNo { get; set; }

        /// <summary>
        /// 1 to enable echo on KO
        /// </summary>
        public int KO_Echo { get; set; }

        /// <summary>
        /// Volume offset (negative for softer)
        /// </summary>
        public int Volume { get; set; }

        /// <summary>
        /// Variables with this index and above will not have their values
        /// reset to 0 between rounds or matches.There are 60 int variables,
        /// indexed from 0 to 59, and 40 float variables, indexed from 0 to 39.
        ///
        /// If omitted, then it defaults to 60 and 40 for integer and float
        /// variables repectively, meaning that none are persistent, i.e.all
        /// are reset.If you want your variables to persist between matches,
        /// you need to override state 5900 from common1.cns.
        /// </summary>
        public int IntPersistIndex { get; set; }

        public int FloatPersistIndex { get; set; }
    }
}