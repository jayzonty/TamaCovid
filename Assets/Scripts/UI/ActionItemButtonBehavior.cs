using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TamaCovid
{
    /// <summary>
    /// Class that handles the behavior of
    /// an action item button UI.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class ActionItemButtonBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        /// <summary>
        /// Data for the action tied to this button.
        /// </summary>
        public SO_Action actionData;

        /// <summary>
        /// Button UI component.
        /// </summary>
        private Button button;

        /// <summary>
        /// Text component of the button.
        /// </summary>
        private Text buttonText;

        /// <summary>
        /// Reference to the text box script.
        /// </summary>
        private TextBoxBehavior textBox;

        /// <summary>
        /// Reference to the main game behavior script.
        /// </summary>
        private MainGameBehavior mainGameBehavior;

        /// <summary>
        /// Reference to the parser behavior script.
        /// </summary>
        private ParserBehavior parserBehavior;

        /// <summary>
        /// Game state data.
        /// </summary>
        private GameState gameState;

        /// <summary>
        /// Unity callback function that is called
        /// when the game object is created.
        /// </summary>
        private void Awake()
        {
            button = GetComponent<Button>();

            buttonText = button.GetComponentInChildren<Text>();

            textBox = GameObject.FindObjectOfType<TextBoxBehavior>();

            mainGameBehavior = GameObject.FindObjectOfType<MainGameBehavior>();

            parserBehavior = GameObject.FindObjectOfType<ParserBehavior>();

            GameStateBehavior gameStateBehavior = GameObject.FindObjectOfType<GameStateBehavior>();
            if (gameStateBehavior != null)
            {
                gameState = gameStateBehavior.Data;
            }
        }

        /// <summary>
        /// Unity callback function that is called
        /// when the script is enabled.
        /// </summary>
        private void OnEnable()
        {
            if (button != null)
            {
                button.onClick.AddListener(HandleButtonClicked);
            }

            if (gameState != null)
            {
                gameState.OnFlagsChanged += StatsOrFlagsChangedHandler;
                gameState.OnStatsChanged += StatsOrFlagsChangedHandler;
            }
        }

        /// <summary>
        /// Unity callback function that is called
        /// when the script is disabled.
        /// </summary>
        private void OnDisable()
        {
            if (button != null)
            {
                button.onClick.RemoveListener(HandleButtonClicked);
            }

            if (gameState != null)
            {
                gameState.OnFlagsChanged -= StatsOrFlagsChangedHandler;
                gameState.OnStatsChanged -= StatsOrFlagsChangedHandler;
            }
        }

        /// <summary>
        /// Unity callback function that is called
        /// before the first update call.
        /// </summary>
        private void Start()
        {
            if ((button != null) && (parserBehavior != null))
            {
                button.interactable = parserBehavior.ParseConditions(actionData.enabledConditionString);
            }
        }

        private void StatsOrFlagsChangedHandler(string flagName)
        {
            if ((button != null) && (parserBehavior != null))
            {
                button.interactable = parserBehavior.ParseConditions(actionData.enabledConditionString);
            }
        }

        /// <summary>
        /// Function to call when the button tied to this
        /// action is clicked.
        /// </summary>
        private void HandleButtonClicked()
        {
            if (actionData != null)
            {
                // TODO: For now, call the DoAction method of the MainGameBehavior script directly.
                // Maybe it's better in the future to pass a message via a messaging system.
                if (mainGameBehavior != null)
                {
                    mainGameBehavior.DoAction(actionData);
                }
            }
        }

        /// <summary>
        /// Unity callback function that is called
        /// when the mouse enters the game object this
        /// script is attached to.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (textBox != null)
            {
                textBox.SetText(actionData.description, 1);
            }
        }

        /// <summary>
        /// Unity callback function that is called
        /// when the mouse leaves the game object this
        /// script is attached to.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            if (textBox != null)
            {
                textBox.SetText("", 1);
            }
        }
    }
}
