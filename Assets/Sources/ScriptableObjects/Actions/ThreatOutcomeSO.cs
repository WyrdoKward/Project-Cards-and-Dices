using Assets.Sources.Entities;
using Assets.Sources.ScriptableObjects.Cards;
using Assets.Sources.Systems;
using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Actions
{
    /// <summary>
    /// Method template design pattern to handle threats
    /// </summary>
    public abstract class ThreatOutcomeSO : ScriptableObject
    {
        public BaseCardSO baseCardSO;
        public bool IsLoop;

        protected GameObject thisCardBodyGameObject;

        public GameObject GameManager { get; internal set; }

        internal void SetCardBodyGameObject(GameObject cardBodyGO)
        {
            thisCardBodyGameObject = cardBodyGO;
        }

        /// <summary>
        /// Comportement lorsque la menace arrive à exécution à la fin de son timer
        /// </summary>
        public void ExecuteThreat()
        {
            if (thisCardBodyGameObject == null)
                return;

            ExecuteThreatBeginningHook();
            var continueLoop = ConcreteExecuteThreat();
            ExecuteThreatEndHook(continueLoop);
        }

        /// <summary>
        /// Implémentation concrète de la threat
        /// </summary>
        /// <returns>False si la meance doit arrêter de boucler</returns>
        protected abstract bool ConcreteExecuteThreat();
        protected virtual void ExecuteThreatBeginningHook() { }
        protected virtual void ExecuteThreatEndHook(bool continueLoop)
        {
            var guid = thisCardBodyGameObject.GetComponentInChildren<Card>().attachedTimerGuid;
            TimeManager.StopTimer(guid);
            if (IsLoop && continueLoop)
            {
                //Debug.Log("ExecuteThreatEndHook");
                var duration = ((ThreatCardSO)baseCardSO).ThreatTime;

                thisCardBodyGameObject.GetComponentInChildren<Card>().LaunchDelayedActionWithTimer(ExecuteThreat, duration, null /*thisCardBodyGameObject.GetComponentInChildren<Card>()*/, false);
            }
        }

        /// <summary>
        /// Conséquence lorsqu'on a réussi à empêcher la menace
        /// </summary>
        public void SuccessToPrevent()
        {
            SuccessToPreventBeginningHook();
            ConcreteSuccessToPrevent();
            SuccessToPreventEndHook();
        }
        protected abstract void ConcreteSuccessToPrevent();
        protected virtual void SuccessToPreventBeginningHook() { }
        protected virtual void SuccessToPreventEndHook()
        {
            var guid = thisCardBodyGameObject.GetComponentInChildren<Card>().attachedTimerGuid;
            TimeManager.StopTimer(guid);
        }

        /// <summary>
        /// Conséquence lorsqu'on a essayé d'empêcher la menace mais que ca a été un echec
        /// </summary>
        public void FailureToPrevent()
        {
            FailureToPreventBeginningHook();
            ConcreteFailureToPrevent();
            FailureToPreventEndHook();
        }
        protected abstract void ConcreteFailureToPrevent();
        protected virtual void FailureToPreventBeginningHook() { }
        protected virtual void FailureToPreventEndHook()
        {
            //Ejecter la carte du follower
            thisCardBodyGameObject.GetComponentInChildren<Card>().Receivedcard.GetComponent<Card>().ReturnToLastPosition();
        }
    }
}
