﻿
namespace TamaCovid
{
    /// <summary>
    /// Utility class containing values for different constants
    /// used in the application.
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Constant for the name of the day stat.
        /// </summary>
        public static string DAY_STAT_NAME = "day";

        /// <summary>
        /// Constant for the name of the time stat.
        /// </summary>
        public static string TIME_STAT_NAME = "time";

        /// <summary>
        /// Constant for the name of the money stat.
        /// </summary>
        public static string MONEY_STAT_NAME = "money";

        /// <summary>
        /// Constant for the name of the energy stat.
        /// </summary>
        public static string ENERGY_STAT_NAME = "energy";

        /// <summary>
        /// Constant for the name of the food stat.
        /// </summary>
        public static string FOOD_STAT_NAME = "food";

        /// <summary>
        /// Constant for the name of the hunger stat.
        /// </summary>
        public static string HUNGER_STAT_NAME = "hunger";

        /// <summary>
        /// Constant for the name of the stress stat.
        /// </summary>
        public static string STRESS_STAT_NAME = "stress";

        /// <summary>
        /// Constant for the name of the medicine stat.
        /// </summary>
        public static string MEDICINE_STAT_NAME = "medicine";

        /// <summary>
        /// Constant for the name of the job type stat.
        /// </summary>
        public static string JOB_TYPE_STAT_NAME = "job";

        /// <summary>
        /// Constant for the name of the num infected stat.
        /// </summary>
        public static string NUM_INFECTED_STAT_NAME = "numInfected";

        public static readonly string[] JOB_NAMES = 
            { "Unemployed", "Full-time Work-from-home", "Full-time On-site", "Part-time Work-from-home", "Part-time On-site", "Freelance" };

        public static readonly string[] HUNGER_STATES =
            { "Very Hungry", "Hungry", "Neutral", "Full", "Very full" };
    }
}
