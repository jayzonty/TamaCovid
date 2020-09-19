
namespace TamaCovid
{
    /// <summary>
    /// Class containing data about
    /// an effect that affects a particular stat.
    /// </summary>
    [System.Serializable]
    public class StatEffect
    {
        /// <summary>
        /// Target stat affected by this effect
        /// </summary>
        public StatType targetStat;

        /// <summary>
        /// Constant modifier
        /// </summary>
        public int modifierConstant = 0;

        /// <summary>
        /// Factor modifier
        /// </summary>
        public float modifierFactor = 1.0f;
    }
}
