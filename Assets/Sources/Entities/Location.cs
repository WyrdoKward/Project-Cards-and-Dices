﻿using Assets.Sources.ScriptableObjects.Cards;
using Assets.Sources.Systems;
using UnityEngine;

namespace Assets.Sources.Entities
{
    public class Location : Card
    {
        public LocationCardSO cardSO;
        public FunctionTimer runningAction;

        public float DefaultExplorationTime = 10f;


        public new void Start()
        {
            base.Start();
        }

        public override string GetName()
        {
            return cardSO.name;
        }
        /// <summary>
        /// Triggered when this card receives an other card
        /// </summary>
        public override void TriggerActionsOnSnap(Card receivedCard)
        {
            Debug.Log($"{cardSO.name} received {receivedCard.GetName()}");
            if (receivedCard is Follower follower)
                Explore(follower);
        }

        private void Explore(Follower follower)
        {
            //Calcul de la durée à partir de this et receivedCard
            var duration = DefaultExplorationTime;

            LaunchTimer(SpawnLoot, duration, follower.Guid.ToString());
        }

        private void SpawnLoot()
        {
            Debug.Log($"{cardSO.name} is spawning loot...");
            gameManager.GetComponent<CardSpawner>().GenerateRandomCardFromList(cardSO.Loot);

            //TODO ejecter receivedCard
            Receivedcard.GetComponent<Card>().ReturnToLastPosition();
        }
    }
}
