using System.Collections.Generic;

namespace TamaCovid
{
    /// <summary>
    /// Class containing data related
    /// to the current state of the game.
    /// </summary>
    public class GameState
    {
        public delegate void StatsChangedDelegate(string statName);
        public event StatsChangedDelegate OnStatsChanged;

        public delegate void FlagsChangedDelegate(string flagName);
        public event FlagsChangedDelegate OnFlagsChanged;

        #region Daily Stats

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
                return GetFlagValue("hasMask");
            }

            set
            {
                SetFlagValue("hasMask", value);
            }
        }

        /// <summary>
        /// Flag determining whether the player has depression.
        /// </summary>
        public bool HasDepression
        {
            get
            {
                return GetFlagValue("hasDepression");
            }

            set
            {
                SetFlagValue("hasDepression", value);
            }
        }

        /// <summary>
        /// Flag determining whether the player has COVID-19 or not.
        /// </summary>
        public bool HasCovid
        {
            get
            {
                return GetFlagValue("hasCovid");
            }

            set
            {
                SetFlagValue("hasCovid", value);
            }
        }

        /// <summary>
        /// Flag determining whether the player has a car or not.
        /// </summary>
        public bool HasCar
        {
            get
            {
                return GetFlagValue("hasCar");
            }

            set
            {
                SetFlagValue("hasCar", value);
            }
        }

        /// <summary>
        /// Flag determining whether the player is immunocompromised or not.
        /// </summary>
        public bool IsImmunocompromised
        {
            get
            {
                return GetFlagValue("isImmunocompromised");
            }

            set
            {
                SetFlagValue("isImmunocompromised", value);
            }
        }

        #endregion

        /// <summary>
        /// Dictionary mapping a stat to its value via its name.
        /// </summary>
        private Dictionary<string, int> statValues = new Dictionary<string, int>();

        /// <summary>
        /// Game flags
        /// </summary>
        private HashSet<string> flags = new HashSet<string>();

        /// <summary>
        /// Add a stat to the list of stats that we are keeping track of.
        /// </summary>
        /// <param name="statName">Name of the stat that will be added.</param>
        /// <param name="initialValue">Initial value of the stat</param>
        public void AddStat(string statName, int initialValue = 0)
        {
            if (!statValues.ContainsKey(statName))
            {
                statValues.Add(statName, initialValue);

                OnStatsChanged?.Invoke(statName);
            }
        }

        /// <summary>
        /// Remove a stat from the list of stats that we are keeping track of.
        /// </summary>
        /// <param name="statName">Name of the stat to remove.</param>
        /// <returns>True if the stat was removed successfully. False otherwise.</returns>
        public bool RemoveStat(string statName)
        {
            bool ret = statValues.Remove(statName);
            OnStatsChanged?.Invoke(statName);
            return ret;
        }

        /// <summary>
        /// Get the value of the given stat name.
        /// </summary>
        /// <param name="statName">Name of the stat</param>
        /// <returns>Returns the value of the given stat (if it exists). Otherwise, returns 0.</returns>
        public int GetStatValue(string statName)
        {
            if (statValues.ContainsKey(statName))
            {
                return statValues[statName];
            }

            return 0;
        }

        /// <summary>
        /// Try to get the value of the given stat name.
        /// </summary>
        /// <param name="statName">Name of the stat</param>
        /// <param name="outValue">Reference to the variable where the result will be placed</param>
        /// <returns>True if the stat exists and the value was successfully set. False otherwise.</returns>
        public bool TryGetStatValue(string statName, out int outValue)
        {
            if (statValues.ContainsKey(statName))
            {
                outValue = statValues[statName];
                return true;
            }

            outValue = 0;
            return false;
        }

        /// <summary>
        /// Set the value of the stat identified by the given stat name.
        /// </summary>
        /// <param name="statName">Name of the stat</param>
        /// <param name="val">Value to set</param>
        /// <returns>True if the stat exists and the value was successfully set. False otherwise.</returns>
        public bool SetStatValue(string statName, int val)
        {
            if (statValues.ContainsKey(statName))
            {
                statValues[statName] = val;
                OnStatsChanged?.Invoke(statName);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Set the value of the given flag to the provided value.
        /// </summary>
        /// <param name="flagName">Name of the flag</param>
        /// <param name="value">New value of the flag</param>
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

            OnFlagsChanged?.Invoke(flagName);
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
