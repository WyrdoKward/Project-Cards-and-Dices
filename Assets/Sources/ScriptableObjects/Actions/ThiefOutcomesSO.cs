using Assets.Sources.Entities;
using Assets.Sources.Providers;
using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Actions
{
    /// <summary>
    /// A implémenter sur tous les ThreatSO de type "voleur"
    /// </summary>
    [CreateAssetMenu(fileName = "ThiefOutcomes", menuName = "Card/ThreatOutcome/ThiefOutcomes")]
    internal class ThiefOutcomesSO : ThreatOutcomeSO
    {
        protected override bool ConcreteExecuteThreat()
        {
            //Récupérer une ressource au hazard
            var randomResource = GameObject.Find("_GameManager").GetComponent<CardProvider>().GetRandomCard<Resource>();

            if (randomResource == null) //Rien à voler, on fait disparaître le voleur et on arrete la boucle avec (return false)
            {
                Debug.Log("Nothing to steal, I'll be back !");
                _thisCard.DestroySelf();
                return false;
            }

            Debug.Log($"Thief has stolen {randomResource.name}!");

            Destroy(randomResource);
            return true;
        }

        protected override void ConcreteFailureToPrevent()
        {
            Debug.Log("Thief has hurt your follower !");
        }

        protected override void ConcreteSuccessToPrevent()
        {
            Debug.Log("Thief has been stopped !");
            _thisCard.DestroySelf();
        }
    }
}
