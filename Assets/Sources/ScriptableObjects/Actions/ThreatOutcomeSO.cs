using Assets.Sources.Entities;
using Assets.Sources.ScriptableObjects.Cards;
using Assets.Sources.Systems;
using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Actions
{
    public abstract class ThreatOutcomeSO : ScriptableObject
    {
        public GameObject thisCardBodyGameObject;
        public BaseCardSO baseCardSO;
        public bool IsLoop;

        /// <summary>
        /// Comportement lorsque la menace arrive à exécution à la fin de son timer
        /// </summary>
        public void ExecuteThreat()
        {
            ExecuteThreatBeginningHook();
            var continueLoop = ConcreteExecuteThreat();
            ExecuteThreatEndHook(continueLoop);
        }

        /// <summary>
        /// Implémentation concrète de la threat
        /// </summary>
        /// <returns>>False si la meance doit arrêter de boucler</returns>
        protected abstract bool ConcreteExecuteThreat();
        protected virtual void ExecuteThreatBeginningHook() { }
        protected virtual void ExecuteThreatEndHook(bool continueLoop)
        {
            var guid = thisCardBodyGameObject.GetComponentInChildren<Card>().Guid.ToString();
            FunctionTimer.StopTimer(guid);
            if (IsLoop && continueLoop)
            {
                Debug.Log("ExecuteThreatEndHook");
                var duration = ((ThreatCardSO)baseCardSO).ThreatTime;

                thisCardBodyGameObject.GetComponentInChildren<Card>().LaunchDelayedActionWithTimer(ExecuteThreat, duration, guid);
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
            var guid = thisCardBodyGameObject.GetComponentInChildren<Card>().Guid.ToString();
            FunctionTimer.StopTimer(guid);
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
