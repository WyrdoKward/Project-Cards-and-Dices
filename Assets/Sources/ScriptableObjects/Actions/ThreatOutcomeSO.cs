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
        private Threat _thisCard { get => thisCardBodyGameObject.GetComponentInChildren<Card>() as Threat; }

        public GameObject GameManager { get; internal set; }

        internal void SetCardBodyGameObject(GameObject cardBodyGO)
        {
            thisCardBodyGameObject = cardBodyGO;
        }

        /// <summary>
        /// Comportement lorsque la menace arrive à la fin de son timer selon si elle est gérée ou non
        /// </summary>
        public void DetermineOutcome()
        {
            if (thisCardBodyGameObject == null)
                return;

            if (_thisCard.handledBy != null)
                _thisCard.AttemptToResolve();
            else
            {
                ExecuteThreatBeginningHook();
                var continueLoop = ConcreteExecuteThreat();
                ExecuteThreatEndHook(continueLoop);
            }
        }

        /// <summary>
        /// Implémentation concrète de la threat
        /// </summary>
        /// <returns>False si la meance doit arrêter de boucler</returns>
        protected abstract bool ConcreteExecuteThreat();
        protected virtual void ExecuteThreatBeginningHook() { }
        protected virtual void ExecuteThreatEndHook(bool continueLoop)
        {
            var guid = _thisCard.attachedTimerGuid;
            TimeManager.StopTimer(guid);
            LoopIfNeeded(continueLoop);
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
            var guid = _thisCard.attachedTimerGuid;
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
            var guid = _thisCard.attachedTimerGuid;
            TimeManager.StopTimer(guid);
            LoopIfNeeded(true);
            //Ejecter la carte du follower
            //_thisCard.Receivedcard.GetComponent<Card>().ReturnToLastPosition();
            _thisCard.SnapOutOfIt(true);
        }

        private void LoopIfNeeded(bool continueLoop)
        {
            if (IsLoop && continueLoop)
            {
                //Debug.Log("ExecuteThreatEndHook");
                var duration = ((ThreatCardSO)baseCardSO).ThreatTime;

                _thisCard.LaunchDelayedActionWithTimer(DetermineOutcome, duration, null, false);
            }
        }
    }
}
