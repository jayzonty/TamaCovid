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
        /// Unity callback function that is called
        /// when the game object is created.
        /// </summary>
        private void Awake()
        {
            Data.AddStat(Constants.DAY_STAT_NAME);
            Data.AddStat(Constants.TIME_STAT_NAME);

            Data.AddStat(Constants.MONEY_STAT_NAME);
            Data.AddStat(Constants.ENERGY_STAT_NAME, 0, 0, 100);
            Data.AddStat(Constants.FOOD_STAT_NAME);
            Data.AddStat(Constants.HUNGER_STAT_NAME, 0, 0, 100);
            Data.AddStat(Constants.STRESS_STAT_NAME);

            Data.AddStat(Constants.MEDICINE_STAT_NAME);
            Data.AddStat(Constants.JOB_TYPE_STAT_NAME);
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
