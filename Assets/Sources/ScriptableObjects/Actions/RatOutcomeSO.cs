using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Actions
{
    [CreateAssetMenu(fileName = "RatOutcomes", menuName = "Card/ThreatOutcome/RatOutcomes")]
    internal class RatOutcomeSO : ThreatOutcomeSO
    {
        protected override bool ConcreteExecuteThreat()
        {
            Debug.Log("Squeeeek");
            Destroy(thisCardBodyGameObject.transform.parent.gameObject);
            return IsLoop;
        }

        protected override void ConcreteFailureToPrevent()
        {
            Debug.Log("Rat has bitten your follower !");
        }

        protected override void ConcreteSuccessToPrevent()
        {
            Destroy(thisCardBodyGameObject.transform.parent.gameObject);
            Debug.Log("Rat is dead.");
        }
    }
}
