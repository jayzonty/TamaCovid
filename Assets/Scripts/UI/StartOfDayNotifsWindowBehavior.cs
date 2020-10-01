using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace TamaCovid
{
    /// <summary>
    /// Class containing functionality related to the
    /// popup window that displays the start of day dialogues.
    /// </summary>
    public class StartOfDayNotifsWindowBehavior : MonoBehaviour
    {
        /// <summary>
        /// Is the popup window open?
        /// </summary>
        public bool IsOpen
        {
            get;
            private set;
        }

        /// <summary>
        /// Button component of the Next button
        /// </summary>
        public Button nextButton;

        /// <summary>
        /// Text component for the content
        /// </summary>
        public Text contentText;

        /// <summary>
        /// Reference to the parser behavior script
        /// </summary>
        private ParserBehavior parserBehavior;

        /// <summary>
        /// List of dialogues to display
        /// </summary>
        private List<Dialogue> dialogues;

        /// <summary>
        /// Index of the current dialogue we're displaying.
        /// </summary>
        private int currentDialogueIndex;

        /// <summary>
        /// Index of the current line of the dialogue we're displaying.
        /// </summary>
        private int currentLineIndex;

        /// <summary>
        /// Show the provided dialogues.
        /// </summary>
        /// <param name="dialogues">List of dialogues to show</param>
        /// <returns>Returns true if successful. False otherwise.</returns>
        public bool ShowDialogues(List<Dialogue> dialogues)
        {
            this.dialogues = dialogues;
            if (dialogues == null)
            {
                return false;
            }

            if (dialogues.Count > 0)
            {
                currentDialogueIndex = 0;
                currentLineIndex = 0;
                PlayCurrentDialogueLine();

                gameObject.SetActive(true);
                IsOpen = true;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Hides the window.
        /// </summary>
        public void Hide()
        {
            gameObject.SetActive(false);
            IsOpen = false;
        }

        /// <summary>
        /// Unity callback function that is called
        /// when the game object is created.
        /// </summary>
        private void Awake()
        {
            parserBehavior = GameObject.FindObjectOfType<ParserBehavior>();
        }

        /// <summary>
        /// Unity callback function that is called
        /// when the script is enabled.
        /// </summary>
        private void OnEnable()
        {
            if (nextButton != null)
            {
                nextButton.onClick.AddListener(HandleNextButtonPressed);
            }
        }

        /// <summary>
        /// Unity callback function that is called
        /// when the script is disabled.
        /// </summary>
        private void OnDisable()
        {
            if (nextButton != null)
            {
                nextButton.onClick.RemoveListener(HandleNextButtonPressed);
            }
        }

        /// <summary>
        /// Handler function for when the next button was pressed.
        /// </summary>
        private void HandleNextButtonPressed()
        {
            Dialogue currentDialogue = dialogues[currentDialogueIndex];
            if (HasNextDialogueLine(currentDialogue, currentLineIndex))
            {
                currentLineIndex = GetNextDialogueLineIndex(currentDialogue, currentLineIndex);
                PlayCurrentDialogueLine();
            }
            else
            {
                if (currentDialogueIndex < dialogues.Count - 1)
                {
                    ++currentDialogueIndex;
                    currentDialogue = dialogues[currentDialogueIndex];
                    currentLineIndex = GetNextDialogueLineIndex(currentDialogue, -1);
                    PlayCurrentDialogueLine();
                }
                else
                {
                    Hide();
                }
            }
        }

        /// <summary>
        /// Play the current dialogue line.
        /// </summary>
        private void PlayCurrentDialogueLine()
        {
            Dialogue currentDialogue = dialogues[currentDialogueIndex];

            if (contentText != null)
            {
                contentText.text = currentDialogue.lines[currentLineIndex].message;
            }

            parserBehavior.ParseCommands(currentDialogue.lines[currentLineIndex].commands);

            if (nextButton != null)
            {
                if (HasNextDialogueLine(currentDialogue, currentLineIndex)
                    || (currentDialogueIndex < dialogues.Count - 1))
                {
                    nextButton.GetComponentInChildren<Text>().text = "Next";
                }
                else
                {
                    nextButton.GetComponentInChildren<Text>().text = "Finish";
                }
            }
        }

        /// <summary>
        /// Gets the index of the next valid dialogue line for the current dialogue.
        /// </summary>
        /// <param name="dialogue">Dialogue to search.</param>
        /// <param name="startingIndex">Index where the search will start.</param>
        /// <returns>The index of the next valid dialogue line. Returns -1 if a valid line was not found.</returns>
        private int GetNextDialogueLineIndex(Dialogue dialogue, int startingIndex)
        {
            if (dialogue != null)
            {
                for (int i = startingIndex + 1; i < dialogue.lines.Count; ++i)
                {
                    if (parserBehavior.ParseConditions(dialogue.lines[i].messageCondition))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// Determines whether there is a next dialogue line
        /// that can be displayed.
        /// </summary>
        /// <param name="dialogue">Dialogue to search.</param>
        /// <param name="startingIndex">Index where the search will start.</param>
        /// <returns>True if there is a next dialogue line. False otherwise.</returns>
        private bool HasNextDialogueLine(Dialogue dialogue, int startingIndex)
        {
            return GetNextDialogueLineIndex(dialogue, startingIndex) != -1;
        }
    }
}
