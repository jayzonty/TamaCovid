using System.Collections.Generic;

namespace TamaCovid
{
    [System.Serializable]
    public class Dialogue
    {
        /// <summary>
        /// Class containing data on one line
        /// of a dialogue.
        /// </summary>
        [System.Serializable]
        public class Line
        {
            /// <summary>
            /// Message to be displayed for this dialogue line
            /// </summary>
            public string message;

            /// <summary>
            /// String containing the commands to run
            /// upon reaching this dialogue line.
            /// </summary>
            public string commandsString;

            /// <summary>
            /// String containing the conditions to show
            /// this particular line.
            /// </summary>
            public string conditionsString;
        }

        /// <summary>
        /// List of dialogue lines
        /// </summary>
        public List<Line> lines;

        /// <summary>
        /// String containing the conditions to show
        /// this particular dialogue.
        /// </summary>
        public string conditionsString;
    }
}
