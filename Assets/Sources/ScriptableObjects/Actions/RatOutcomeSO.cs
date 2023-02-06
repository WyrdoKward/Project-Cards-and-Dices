using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Actions
{
    [CreateAssetMenu(fileName = "RatOutcomes", menuName = "Card/ThreatOutcome/RatOutcomes")]
    internal class RatOutcomeSO : ThreatOutcomeSO
    {
        protected override bool ConcreteExecuteThreat()
        {
            Debug.Log("Squeeeek");
            _thisCard.DestroySelf();
            return IsLoop;
        }

        protected override void ConcreteFailureToPrevent()
        {
            Debug.Log("Rat has bitten your follower !");
        }

        protected override void ConcreteSuccessToPrevent()
        {
            Debug.Log("Rat is dead.");
            _thisCard.DestroySelf();
        }
    }
}
