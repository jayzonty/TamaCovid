using UnityEngine;
using UnityEngine.UI;

namespace TamaCovid
{
    /// <summary>
    /// Class containing the behavior of the
    /// panel displaying the stats.
    /// </summary>
    public class StatsPanelBehavior : MonoBehaviour
    {
        /// <summary>
        /// Text display for the in-game day
        /// </summary>
        public Text dayText;

        /// <summary>
        /// Text display for the in-game time
        /// </summary>
        public Text timeText;

        /// <summary>
        /// Text display for the money
        /// </summary>
        public Text moneyText;

        /// <summary>
        /// Text display for the energy
        /// </summary>
        public Text energyText;

        /// <summary>
        /// Text display for the food
        /// </summary>
        public Text foodText;

        /// <summary>
        /// Text display for the hunger
        /// </summary>
        public Text hungerText;

        /// <summary>
        /// Text display for the anxiety level
        /// </summary>
        public Text anxietyText;

        /// <summary>
        /// Game state data
        /// </summary>
        private GameState gameState;

        /// <summary>
        /// Unity callback function that is called
        /// before the first update call.
        /// </summary>
        private void Start()
        {
            GameStateBehavior gameStateBehavior = GameObject.FindObjectOfType<GameStateBehavior>();
            if (gameStateBehavior != null)
            {
                gameState = gameStateBehavior.Data;
            }
        }

        /// <summary>
        /// Unity callback function that is called
        /// every frame.
        /// </summary>
        private void Update()
        {
            if (dayText != null)
            {
                dayText.text = "Day: " + gameState.GetStatValue(Constants.DAY_STAT_NAME);
            }

            if (timeText != null)
            {
                timeText.text = "Time: " + gameState.GetStatValue(Constants.TIME_STAT_NAME);
            }

            if (moneyText != null)
            {
                moneyText.text = "Money: " + gameState.GetStatValue(Constants.MONEY_STAT_NAME);
            }

            if (energyText != null)
            {
                energyText.text = "Energy: " + gameState.GetStatValue(Constants.ENERGY_STAT_NAME);
            }

            if (foodText != null)
            {
                foodText.text = "Food: " + gameState.GetStatValue(Constants.FOOD_STAT_NAME);
            }

            if (hungerText != null)
            {
                hungerText.text = "Hunger: " + gameState.GetStatValue(Constants.HUNGER_STAT_NAME);
            }

            if (anxietyText != null)
            {
                anxietyText.text = "Anxiety: " + gameState.GetStatValue(Constants.ANXIETY_STAT_NAME);
            }
        }
    }
}
