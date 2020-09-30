using UnityEngine;

namespace TamaCovid
{
    /// <summary>
    /// Class describing information about a 
    /// stat's value, along with additional info
    /// such as minimum and maximum values.
    /// </summary>
    public class StatValue
    {
        /// <summary>
        /// Getter/Setter for the value of the stat.
        /// </summary>
        public int Value
        {
            get
            {
                return val;
            }

            set
            {
                val = Mathf.Clamp(value, minValue, maxValue);
            }
        }

        /// <summary>
        /// Getter/Setter for the minimum value of the stat.
        /// </summary>
        public int MinValue
        {
            get
            {
                return minValue;
            }

            set
            {
                minValue = value;

                // Just make sure that minValue <= maxValue
                if (minValue > maxValue)
                {
                    int temp = minValue;
                    minValue = maxValue;
                    maxValue = temp;
                }

                val = Mathf.Clamp(val, minValue, maxValue);
            }
        }

        /// <summary>
        /// Getter/Setter for the mMaximum value of the stat.
        /// </summary>
        public int MaxValue
        {
            get
            {
                return maxValue;
            }

            set
            {
                maxValue = value;

                // Just make sure that minValue <= maxValue
                if (minValue > maxValue)
                {
                    int temp = minValue;
                    minValue = maxValue;
                    maxValue = temp;
                }

                val = Mathf.Clamp(val, minValue, maxValue);
            }
        }

        /// <summary>
        /// Value of the stat.
        /// </summary>
        private int val;

        /// <summary>
        /// Minimum value of the stat.
        /// </summary>
        private int minValue;

        /// <summary>
        /// Maximum value of the stat.
        /// </summary>
        private int maxValue;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="minValue">Minimum value of the stat.</param>
        /// <param name="maxValue">Maximum value of the stat.</param>
        /// <param name="initialValue">Initial value of the stat.</param>
        public StatValue(int initialValue = 0, int minValue = 0, int maxValue = int.MaxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            Value = initialValue;
        }
    }
}
