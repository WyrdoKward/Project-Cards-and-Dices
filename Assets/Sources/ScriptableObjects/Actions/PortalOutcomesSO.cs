using Assets.Sources.ScriptableObjects.Cards;
using Assets.Sources.Systems;
using Assets.Sources.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Actions
{
    [CreateAssetMenu(fileName = "PortalOutcomes", menuName = "Card/ThreatOutcome/PortalOutcomes")]
    internal class PortalOutcomesSO : ThreatOutcomeSO
    {
        public List<BaseCardSO> EnemiesSO;
        public List<int> EnemiesWeights;
        public DictionaryForInspector<BaseCardSO, int> enemies
        {
            get { return new DictionaryForInspector<BaseCardSO, int>(EnemiesSO, EnemiesWeights); }
        }

        public List<BaseCardSO> Loot;

        protected override bool ConcreteExecuteThreat()
        {
            //Spawn un enemi
            GameManager.GetComponent<CardSpawner>().GenerateRandomCardFromWeightedList(enemies);

            Debug.Log("An enemy came out of the portal !");
            return true;
        }

        protected override void ConcreteFailureToPrevent()
        {
            Debug.Log("Portal has killed your follower !");
        }

        protected override void ConcreteSuccessToPrevent()
        {
            Destroy(thisCardBodyGameObject.transform.parent.gameObject);
            Debug.Log("Portal has been destroyed");
        }
    }
}
