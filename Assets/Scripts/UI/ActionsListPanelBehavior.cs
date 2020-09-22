using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TamaCovid
{
    /// <summary>
    /// Class that handles the behavior of an
    /// action list panel UI element.
    /// </summary>
    public class ActionsListPanelBehavior : MonoBehaviour
    {
        /// <summary>
        /// Prefab for an action item button
        /// </summary>
        public GameObject actionItemButtonPrefab;

        /// <summary>
        /// Category name for the list of actions
        /// </summary>
        public string categoryName;

        /// <summary>
        /// List of actions for this action list panel
        /// </summary>
        public List<SO_Action> actionList;

        /// <summary>
        /// Text element displaying the category name
        /// </summary>
        public Text categoryText;

        /// <summary>
        /// Transform component of the gameobject
        /// where the list of action item buttons will be placed.
        /// </summary>
        public Transform actionItemsPanelTransform;

        /// <summary>
        /// Unity callback function that is called
        /// before the first update call.
        /// </summary>
        private void Start()
        {
            if (categoryText != null)
            {
                categoryText.text = categoryName;
            }
        }
    }
}
