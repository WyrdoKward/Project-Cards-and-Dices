using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Actions
{
    public abstract class ThreatOutcomeSO : ScriptableObject
    {
        /// <summary>
        /// Comportement lorsque la menace arrive à exécution à la fin de son timer
        /// </summary>
        public abstract void ExecuteThreat();

        /// <summary>
        /// Conséquence lorsqu'on a réussi à empêcher la menace
        /// </summary>
        public abstract void SuccessToPrevent();

        /// <summary>
        /// Conséquence lorsqu'on a essayé d'empêcher la menace mais que ca a été un echec
        /// </summary>
        public abstract void FailureToPrevent();
    }
}
