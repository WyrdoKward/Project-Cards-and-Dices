using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Actions
{
    /// <summary>
    /// A implémenter sur tous les ThreatSO de type "voleur"
    /// </summary>
    [CreateAssetMenu(fileName = "New Thief", menuName = "Card/ThreatOutcome/ThiefActions")]
    internal class ThiefActionsSO : ThreatOutcomeSO
    {
        public bool IsLoop;
        public override void ExecuteThreat()
        {
            Debug.Log("Thief has stolen !");
            if (IsLoop)
                Debug.Log("And he'll do it again !!");

        }

        public override void FailureToPrevent()
        {
            Debug.Log("Thief has hurt your follower !");
        }

        public override void SuccessToPrevent()
        {
            Destroy(thisCardGameObject.transform.parent.gameObject);
            Debug.Log("Thief has been stopped !");
        }
    }
}
