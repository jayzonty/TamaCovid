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
        /// Player's hunger value.
        /// </summary>
        public int hunger = 0;

        /// <summary>
        /// Player's happiness value.
        /// </summary>
        public int happiness = 0;
        
        /// <summary>
        /// Player's anxiety level.
        /// </summary>
        public int anxiety = 0;

        #endregion

        /// <summary>
        /// Game flags
        /// </summary>
        private HashSet<string> flags = new HashSet<string>();

        /// <summary>
        /// Sets the value for the given flag to true
        /// </summary>
        /// <param name="flagName">Flag that will be set to true</param>
        public void SetFlag(string flagName)
        {
            flags.Add(flagName);
        }

        /// <summary>
        /// Sets the value for the given flags to true
        /// </summary>
        /// <param name="flagNames">Flags that will be set to true</param>
        public void SetFlags(params string[] flagNames)
        {
            foreach (string flagName in flagNames)
            {
                SetFlag(flagName);
            }
        }

        /// <summary>
        /// Sets the value for the given flag to false
        /// </summary>
        /// <param name="flagName">Flag that will be set to false</param>
        public void UnsetFlag(string flagNames)
        {
            flags.Remove(flagNames);
        }

        /// <summary>
        /// Sets the value for the given flags to false
        /// </summary>
        /// <param name="flagNames">Flags that will be set to false</param>
        public void UnsetFlags(params string[] flagNames)
        {
            foreach (string flagName in flagNames)
            {
                UnsetFlag(flagName);
            }
        }
    }
}
