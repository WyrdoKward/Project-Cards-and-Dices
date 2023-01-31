using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Actions
{
    [CreateAssetMenu(fileName = "New Prank", menuName = "Card/ThreatOutcome/PrankActions")]
    internal class PrankOutcomesSO : ThreatOutcomeSO
    {
        protected override bool ConcreteExecuteThreat()
        {
            //Debug.Log("Haha you got pranked !");
            return true;
        }

        protected override void ConcreteFailureToPrevent()
        {
            //Debug.Log("Prank got worse !");
        }

        protected override void ConcreteSuccessToPrevent()
        {
            //Debug.Log("Prank didn't work");
        }
    }
}
