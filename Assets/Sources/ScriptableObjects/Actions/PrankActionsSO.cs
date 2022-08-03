using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Actions
{
    [CreateAssetMenu(fileName = "New Prank", menuName = "Card/ThreatOutcome/PrankActions")]
    internal class PrankActionsSO : ThreatOutcomeSO
    {
        public override void ExecuteThreat()
        {
            Debug.Log("Haha you got pranked !");
        }

        public override void FailureToPrevent()
        {
            Debug.Log("Prank got worse !");
        }

        public override void SuccessToPrevent()
        {
            Debug.Log("Prank didn't work");
        }
    }
}
