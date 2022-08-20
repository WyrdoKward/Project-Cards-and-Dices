using System;
using System.Collections.Generic;
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
        private GameObject go;
        private bool isFinished;

        private static List<FunctionTimer> activeTimers;
        private static GameObject initGameObject;

        #endregion

        /// <summary>
        /// Public function to use to instanciate a new timer with an action
        /// </summary>
        public static FunctionTimer Create(Action action, float delay, bool hasToStopWhenCardIsMoving, string timerName = "")
        {
            //Debug.Log("Creating " + timerName);
            InitIfNeeded();
            //var go = new GameObject($"Timer-{timerName}", typeof(MonoBehaviourHook));
            var functionTimer = new FunctionTimer(action, delay, timerName, hasToStopWhenCardIsMoving);
            //go.GetComponent<MonoBehaviourHook>().onUpdate = functionTimer.Update;

            //activeTimers.Add(functionTimer);

            return functionTimer;
        }

        [Obsolete]
        public static void StopTimer(string timerName, bool fromMovement = false)
        {
            InitIfNeeded();
            var hasBeenStopped = false;
            //Debug.Log("Stopping " + timerName);


            for (var i = 0; i < activeTimers.Count; i++)
            {
                if (activeTimers[i].name == timerName)
                {
                    if (fromMovement && !activeTimers[i].HasToStopWhenCardIsMoving)
                        continue;

                    activeTimers[i].DestroySelf();
                    i--; // On décrémente pour ne pas en skipper un, puisque qu'on en a viré un de la liste
                    //Debug.Log("Stopping " + timerName + " OK");
                    hasBeenStopped = true;
                }
            }
            if (!hasBeenStopped)
                Debug.Log("Stopping " + timerName + " FAILED - no such timer exists");

        }

        [Obsolete]
        internal static void StopTimerFromMovement(string receivedCardGuid)
        {
            StopTimer(receivedCardGuid, true);
        }

        #region Private methods

        private FunctionTimer(Action action, float delay, string timerName, bool hasToStopWhenCardIsMoving)
        {
            this.action = action;
            this.remainingTime = delay;
            this.name = timerName;
            HasToStopWhenCardIsMoving = hasToStopWhenCardIsMoving;
        }

        private static void InitIfNeeded()
        {
            if (initGameObject == null)
            {
                initGameObject = new GameObject("FunctionTimer_InitGameObject");
                activeTimers = new List<FunctionTimer>();
            }
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

        [Obsolete]
        public void DestroySelf()
        {
            isFinished = true;
            UnityEngine.Object.Destroy(go);
            RemoveTimer(this);
        }

        [Obsolete]
        private static void RemoveTimer(FunctionTimer functionTimer)
        {
            InitIfNeeded();
            activeTimers.Remove(functionTimer);
        }



        #endregion
    }
}
