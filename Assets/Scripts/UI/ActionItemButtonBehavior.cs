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
        /// Unity callback function that is called
        /// when the game object is created.
        /// </summary>
        private void Awake()
        {
            button = GetComponent<Button>();

            buttonText = button.GetComponentInChildren<Text>();

            textBox = GameObject.FindObjectOfType<TextBoxBehavior>();
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
        }

        /// <summary>
        /// Unity callback function that is called
        /// before the first update call.
        /// </summary>
        private void Start()
        {
        }

        /// <summary>
        /// Function to call when the button tied to this
        /// action is clicked.
        /// </summary>
        private void HandleButtonClicked()
        {
            if (actionData != null)
            {
                // For now, print out the display name when this button is clicked.
                Debug.Log(actionData.displayName);
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
                textBox.SetText(actionData.description);
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
                textBox.SetText("");
            }
        }
    }
}
