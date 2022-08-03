using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Actions
{
    /// <summary>
    /// A implémenter sur tous les ThreatSO de type "voleur"
    /// </summary>
    [CreateAssetMenu(fileName = "New Thief", menuName = "Card/ThreatOutcome/ThiefActions")]
    internal class ThiefActionsSO : ThreatOutcomeSO
    {
        public override void Failure()
        {
            Debug.Log("Thief has stolen !");
        }

        public override void Success()
        {
            Debug.Log("Thief has been stopped !");
        }
    }
}
