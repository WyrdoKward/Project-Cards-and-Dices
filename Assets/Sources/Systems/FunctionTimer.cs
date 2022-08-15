﻿using System;
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
        public static FunctionTimer Create(Action action, float delay, bool hasToStopWhenCardIsMoving, string timerName = null)
        {
            //Debug.Log("Creating " + timerName);
            InitIfNeeded();
            var go = new GameObject("FunctionTimer", typeof(MonoBehaviourHook));
            var functionTimer = new FunctionTimer(action, delay, timerName, go, hasToStopWhenCardIsMoving);
            go.GetComponent<MonoBehaviourHook>().onUpdate = functionTimer.Update;

            activeTimers.Add(functionTimer);

            return functionTimer;
        }

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

        internal static void StopTimerFromMovement(string receivedCardGuid)
        {
            StopTimer(receivedCardGuid, true);
        }

        #region Private methods

        private FunctionTimer(Action action, float delay, string timerName, GameObject go, bool hasToStopWhenCardIsMoving)
        {
            this.action = action;
            this.remainingTime = delay;
            this.name = timerName;
            this.go = go;
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

        private void Update()
        {
            if (isFinished)
                return;

            remainingTime -= Time.deltaTime;
            if (remainingTime < 0)
            {
                action();
                DestroySelf();
            }
        }


        private void DestroySelf()
        {
            isFinished = true;
            UnityEngine.Object.Destroy(go);
            RemoveTimer(this);
        }

        private static void RemoveTimer(FunctionTimer functionTimer)
        {
            InitIfNeeded();
            activeTimers.Remove(functionTimer);
        }

        /// <summary>
        /// Dummy class to access MonoBehaviour functions
        /// </summary>
        private class MonoBehaviourHook : MonoBehaviour
        {
            public Action onUpdate;
            private void Update()
            {
                if (onUpdate != null)
                    onUpdate();
            }
        }

        #endregion
    }
}
