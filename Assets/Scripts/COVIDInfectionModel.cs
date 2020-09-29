using UnityEngine;

namespace TamaCovid
{
    /// <summary>
    /// Class containing functionality related
    /// to calculating odds of contracting/spreading
    /// COVID.
    /// </summary>
    public class COVIDInfectionModel
    {
        /// <summary>
        /// Class containing data regarding the
        /// result of a simulation.
        /// </summary>
        public class InfectionResult
        {
            /// <summary>
            /// Is the source infected?
            /// </summary>
            public bool isInfected;

            /// <summary>
            /// Number of people that the current person
            /// has infected.
            /// </summary>
            public int numInfected;
        }

        /// <summary>
        /// Simulates infections for a particular action.
        /// </summary>
        /// <param name="isOriginallyInfected">Is the current person originally infected.</param>
        /// <param name="susceptability">Susceptability of the person to be infected.</param>
        /// <param name="actionRiskFactor">Risk factor of spreading/contracting COVID associated with the action.</param>
        /// <param name="numPeopleInvolved">Number of other people involved in the action.</param>
        /// <returns></returns>
        public static InfectionResult SimulateInfectionFromAction(bool isOriginallyInfected, float susceptability, float actionRiskFactor, int numPeopleInvolved)
        {
            InfectionResult ret = new InfectionResult();

            int numOriginalInfected = Random.Range(0, numPeopleInvolved + 1);
            int numAtRisk = numPeopleInvolved - numOriginalInfected;

            if (isOriginallyInfected)
            {
                ret.isInfected = true;

                float infectionRoll = Random.Range(0.0f, 1.0f);
                if (infectionRoll <= susceptability)
                {
                    ret.numInfected = Mathf.FloorToInt(numAtRisk * actionRiskFactor);
                }
            }
            else
            {
                float infectionRoll = Random.Range(0.0f, 1.0f);
                if (infectionRoll <= susceptability)
                {
                    ret.isInfected = true;
                }
            }

            return ret;
        }
    }
}
