using Assets.Sources.ScriptableObjects.Cards;
using Assets.Sources.Systems;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sources.Entities
{
    public class Location : Card
    {
        public LocationCardSO cardSO;
        public FunctionTimer runningAction;

        public float DefaultExplorationTime = 10f;
        public override Color DefaultSliderColor { get => GlobalVariables.LOCATION_DefaultSliderColor; }

        protected override List<Type> AllowedTypes => new() { typeof(Follower) };

        public new void Start()
        {
            base.Start();
        }

        public override string GetName() => cardSO.name;

        /// <summary>
        /// Triggered when this Location receives an other card.
        /// </summary>
        /// <returns>True if an action has been executed</returns>
        protected override bool TriggerActionsOnSnap(List<Card> stack)
        {
            if (!base.TriggerActionsOnSnap(stack))
                return false;

            //Execute actions on Location here
            if (stackHolder.Followers.Count == 1)
            {
                Explore(stackHolder.Followers[0]);
                return true;
            }

            return false;
        }

        private void Explore(Follower follower)
        {
            //Calcul de la durée à partir de this et receivedCard
            var duration = DefaultExplorationTime;

            LaunchDelayedActionWithTimer(EndExploration, duration, follower, true);
        }

        private void EndExploration()
        {
            SpawnLoot();
            SnapOutOfIt(true);
        }

        private void SpawnLoot()
        {
            //Debug.Log($"{cardSO.name} is spawning loot...");
            gameManager.GetComponent<CardSpawner>().GenerateRandomCardFromList(cardSO.Loot);

            //TODO ejecter receivedCard
            if (NextCardInStack != null)
                NextCardInStack.GetComponent<Card>().ReturnToLastPosition();
        }

        public override void SnapOutOfIt(bool expulseCardOnUI = false)
        {
            base.SnapOutOfIt(expulseCardOnUI);
            TimeManager.StopTimer(attachedTimerGuid);
        }

        public override Color ComputeSpecificSliderColor()
        {
            return DefaultSliderColor;
        }

        public override BaseCardSO GetCardSO()
        {
            return cardSO;
        }
    }
}
