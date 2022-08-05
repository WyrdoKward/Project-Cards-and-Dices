using Assets.Sources.Entities;
using Assets.Sources.Providers;
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
            //Récupérer une ressource au hazard
            var randomResource = GameObject.Find("_GameManager").GetComponent<CardProvider>().GetRandomCard<Resource>();

            if (randomResource == null)
            {
                Debug.Log("Nothing to steal, I'll be back !");
                Destroy(thisCardGameObject.transform.parent.gameObject);
                return;
            }

            Debug.Log($"Thief has stolen {randomResource.name}!");
            if (IsLoop)
                Debug.Log("And he'll do it again !!");

            Destroy(randomResource);
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
