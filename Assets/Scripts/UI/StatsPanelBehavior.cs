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
        public Text dayAndTimeText;

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
        public Text stressText;

        /// <summary>
        /// Text display for the number of medicines held
        /// </summary>
        public Text medicineText;

        /// <summary>
        /// Text display for the current job
        /// </summary>
        public Text jobText;

        /// <summary>
        /// Text display for other status that the player has
        /// </summary>
        public Text statusText;

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
            if (dayAndTimeText != null)
            {
                string text = "Day: " + gameState.GetStatValue(Constants.DAY_STAT_NAME);
                text += "  Time: " + RawMinutesToFormattedTimeString(gameState.GetStatValue(Constants.TIME_STAT_NAME));
                dayAndTimeText.text = text;
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
                foodText.text = "Meals: " + gameState.GetStatValue(Constants.FOOD_STAT_NAME);
            }

            if (hungerText != null)
            {
                int hunger = gameState.GetStatValue(Constants.HUNGER_STAT_NAME);
                int hungerState = hunger / 20;
                hungerState = Constants.HUNGER_STATES.Length - hungerState - 1;
                hungerState = Mathf.Max(hungerState, 0);
                hungerText.text = "Hunger: " + Constants.HUNGER_STATES[hungerState]; 
            }

            if (stressText != null)
            {
                stressText.text = "Stress: " + gameState.GetStatValue(Constants.STRESS_STAT_NAME);
            }

            if (medicineText != null)
            {
                medicineText.text = "Medicine: " + gameState.GetStatValue(Constants.MEDICINE_STAT_NAME);
            }

            if (jobText != null)
            {
                string text = "Job: ";
                int jobType = gameState.GetStatValue(Constants.JOB_TYPE_STAT_NAME);
                text += Constants.JOB_NAMES[jobType];

                if (jobType == 5)
                {
                    int timeWorked = gameState.GetStatValue("timeWorked");
                    int timeLeft = 960 - timeWorked;
                    text += "\nWork hours left: " + (timeLeft / 60) + " hours.";
                }
                else if (jobType != 0)
                {
                    int timeWorked = gameState.GetStatValue("timeWorked");
                    int timeLeft = 480 - timeWorked;
                    text += "\nWork hours left: " + (timeLeft / 60) + " hours.";
                }

                jobText.text = text;
            }

            if (statusText != null)
            {
                string text = "";
                if (gameState.IsImmunocompromised)
                {
                    text += "Immunocompromised\n";
                }
                if (gameState.HasDepression)
                {
                    text += "Has Depression\n";
                }

                if (gameState.HasMask)
                {
                    text += "Has Mask\n";
                }
                else
                {
                    text += "No Mask\n";
                }

                if (gameState.GetFlagValue("isSick"))
                {
                    text += "Sick\n";
                }
                statusText.text = text;
            }
        }

        /// <summary>
        /// Convert the provided time in minutes into a formatted string
        /// in the format HH:MM
        /// </summary>
        /// <param name="rawMinutes">Time in minutes.</param>
        /// <returns>Time in string formatted as HH:MM.</returns>
        private string RawMinutesToFormattedTimeString(int rawMinutes)
        {
            int hour = rawMinutes / 60;
            int minutes = rawMinutes % 60;

            return string.Format("{0:D2}:{1:D2}", hour, minutes);
        }
    }
}
