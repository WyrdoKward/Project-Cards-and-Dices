using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Actions
{
    [CreateAssetMenu(fileName = "New Prank", menuName = "Card/ThreatOutcome/PrankActions")]
    internal class PrankActionsSO : ThreatOutcomeSO
    {
        public override void Failure()
        {
            Debug.Log("Prank didn't work");
        }

        public override void Success()
        {
            Debug.Log("Haha you got pranked !");
        }
    }
}
