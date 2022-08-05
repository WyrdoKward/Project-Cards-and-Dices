using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Actions
{
    [CreateAssetMenu(fileName = "New Prank", menuName = "Card/ThreatOutcome/PrankActions")]
    internal class PrankActionsSO : ThreatOutcomeSO
    {
        protected override bool ConcreteExecuteThreat()
        {
            Debug.Log("Haha you got pranked !");
            return true;
        }

        public override void FailureToPrevent()
        {
            Debug.Log("Prank got worse !");
        }

        public override void ConcreteSuccessToPrevent()
        {
            Debug.Log("Prank didn't work");
        }
    }
}
