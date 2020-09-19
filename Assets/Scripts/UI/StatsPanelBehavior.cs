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
        /// Text display for the money
        /// </summary>
        public Text moneyText;

        /// <summary>
        /// Text display for the hunger
        /// </summary>
        public Text hungerText;

        /// <summary>
        /// Text display for the happiness
        /// </summary>
        public Text happinessText;

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
            MainGameBehavior mainGameBehavior = GameObject.FindObjectOfType<MainGameBehavior>();
            if (mainGameBehavior != null)
            {
                gameState = mainGameBehavior.GameState;
            }
        }

        /// <summary>
        /// Unity callback function that is called
        /// every frame.
        /// </summary>
        private void Update()
        {
            if (moneyText != null)
            {
                moneyText.text = "Money: " + gameState.money;
            }

            if (hungerText != null)
            {
                hungerText.text = "Hunger: " + gameState.hunger;
            }

            if (happinessText != null)
            {
                happinessText.text = "Happiness: " + gameState.happiness;
            }

            if (anxietyText != null)
            {
                anxietyText.text = "Anxiety: " + gameState.anxiety;
            }
        }
    }
}
