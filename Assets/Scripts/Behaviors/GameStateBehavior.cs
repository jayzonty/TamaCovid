using UnityEngine;

namespace TamaCovid
{
    /// <summary>
    /// Monobehavior containing the game state data.
    /// </summary>
    public class GameStateBehavior : MonoBehaviour
    {
        /// <summary>
        /// Data regarding the game state.
        /// </summary>
        public GameState Data
        {
            get;
            private set;
        } = new GameState();

        /// <summary>
        /// Is the game over?
        /// </summary>
        public bool IsGameEnd
        {
            get;
            set;
        } = false;

        /// <summary>
        /// Which ending number is the player on.
        /// </summary>
        public int EndingNumber
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// Action that caused the player to contract COVID.
        /// </summary>
        public SO_Action actionThatCauseCOVID;

        /// <summary>
        /// Initialize the game state data from the given character preset.
        /// </summary>
        /// <param name="characterPreset">Character preset.</param>
        public void InitializeGameStateFromPreset(SO_CharacterPreset characterPreset)
        {
            if (characterPreset != null)
            {
                Data.SetStatValue(Constants.MONEY_STAT_NAME, characterPreset.startingMoney);
                Data.SetStatValue(Constants.JOB_TYPE_STAT_NAME, characterPreset.startingJob);

                Data.SetFlagValue("hasDepression", characterPreset.hasDepression);
            }
        }

        /// <summary>
        /// Reset all stats and flags.
        /// </summary>
        public void ResetStatsAndFlags()
        {
            Data.RemoveAllStats();
            Data.ResetAllFlags();

            Data.AddStat(Constants.DAY_STAT_NAME, 1);
            Data.AddStat(Constants.TIME_STAT_NAME, 480);

            Data.AddStat(Constants.MONEY_STAT_NAME);
            Data.AddStat(Constants.ENERGY_STAT_NAME, 100, 0, 100);
            Data.AddStat(Constants.FOOD_STAT_NAME);
            Data.AddStat(Constants.HUNGER_STAT_NAME, 0, 0, 100);
            Data.AddStat(Constants.STRESS_STAT_NAME);

            Data.AddStat(Constants.MEDICINE_STAT_NAME);
            Data.AddStat(Constants.JOB_TYPE_STAT_NAME);

            Data.AddStat(Constants.NUM_INFECTED_STAT_NAME);

            IsGameEnd = false;
            EndingNumber = 0;
            actionThatCauseCOVID = null;
        }

        /// <summary>
        /// Unity callback function that is called
        /// when the game object is created.
        /// </summary>
        private void Awake()
        {
            ResetStatsAndFlags();
        }

        /// <summary>
        /// Unity callback function that is called
        /// before the first update call.
        /// </summary>
        private void Start()
        {
            // Set this game object to persist across scene loading/unloading
            DontDestroyOnLoad(gameObject);
        }
    }
}
