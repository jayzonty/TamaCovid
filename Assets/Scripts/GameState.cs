using System.Collections.Generic;

namespace TamaCovid
{
    /// <summary>
    /// Class containing data related
    /// to the current state of the game.
    /// </summary>
    public class GameState
    {
        #region Daily Stats

        /// <summary>
        /// Player's money.
        /// </summary>
        public int money = 0;

        /// <summary>
        /// Player's energy value.
        /// </summary>
        public int energy = 0;

        /// <summary>
        /// Amount of food the player has.
        /// </summary>
        public int food = 0;

        /// <summary>
        /// Player's hunger value.
        /// </summary>
        public int hunger = 0;
        
        /// <summary>
        /// Player's anxiety level.
        /// </summary>
        public int anxiety = 0;

        /// <summary>
        /// Number of people the player has infected.
        /// </summary>
        public int numInfected = 0;

        /**
         * ===== Flag Macros =====
         * TODO: For now, the flag names are hard-coded.
         * Should take them out to a separate class containing
         * all the flag names or something.
         * =======================
         */

        /// <summary>
        /// Flag determining whether the player is wearing a mask.
        /// </summary>
        public bool HasMask
        {
            get
            {
                return GetFlagValue("#hasMask");
            }

            set
            {
                SetFlagValue("#hasMask", value);
            }
        }

        /// <summary>
        /// Flag determining whether the player has depression.
        /// </summary>
        public bool HasDepression
        {
            get
            {
                return GetFlagValue("#hasDepression");
            }

            set
            {
                SetFlagValue("#hasDepression", value);
            }
        }

        /// <summary>
        /// Flag determining whether the player has COVID-19 or not.
        /// </summary>
        public bool HasCovid
        {
            get
            {
                return GetFlagValue("#hasCovid");
            }

            set
            {
                SetFlagValue("#hasCovid", value);
            }
        }

        /// <summary>
        /// Flag determining whether the player has a car or not.
        /// </summary>
        public bool HasCar
        {
            get
            {
                return GetFlagValue("#hasCar");
            }

            set
            {
                SetFlagValue("#hasCar", value);
            }
        }

        /// <summary>
        /// Flag determining whether the player is immunocompromised or not.
        /// </summary>
        public bool IsImmunocompromised
        {
            get
            {
                return GetFlagValue("#isImmunocompromised");
            }

            set
            {
                SetFlagValue("#isImmunocompromised", value);
            }
        }

        #endregion

        /// <summary>
        /// Game flags
        /// </summary>
        private HashSet<string> flags = new HashSet<string>();

        public void SetFlagValue(string flagName, bool value)
        {
            if (value)
            {
                flags.Add(flagName);
            }
            else
            {
                flags.Remove(flagName);
            }
        }

        /// <summary>
        /// Checks whether the given flag is set or not.
        /// </summary>
        /// <param name="flagName">Name of the flag to check</param>
        /// <returns>True if the given flag is set. False otherwise.</returns>
        public bool GetFlagValue(string flagName)
        {
            return flags.Contains(flagName);
        }
    }
}
