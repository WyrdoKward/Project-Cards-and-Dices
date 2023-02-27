using System;
using UnityEngine;

namespace Assets.Sources.Systems
{
    /// <summary>
    /// Class executing a chosen action after a delay.
    /// </summary>
    public class FunctionTimer
    {
        #region Fields
        public bool HasToStopWhenCardIsMoving;

        private Action action;
        private float remainingTime;
        private string name;

        #endregion

        /// <summary>
        /// Public function to use to instanciate a new timer with an action
        /// </summary>
        public static FunctionTimer Create(Action action, float delay, bool hasToStopWhenCardIsMoving, string timerName = "")
        {
            //Debug.Log("Creating " + timerName);
            var functionTimer = new FunctionTimer(action, delay, timerName, hasToStopWhenCardIsMoving);

            return functionTimer;
        }

        #region Private methods

        private FunctionTimer(Action action, float delay, string timerName, bool hasToStopWhenCardIsMoving)
        {
            this.action = action;
            this.remainingTime = delay;
            this.name = timerName;
            HasToStopWhenCardIsMoving = hasToStopWhenCardIsMoving;
        }

        /// <summary>
        /// Checks if timer is complete
        /// </summary>
        /// <returns>True if remainingTime is < 0</returns>
        public bool Update()
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime < 0)
            {
                action();
                return true;
            }
            return false;
        }

        #endregion
    }
}
