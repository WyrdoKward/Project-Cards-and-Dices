using Assets.Sources.ScriptableObjects.Cards;
using Assets.Sources.Systems;
using UnityEngine;

namespace Assets.Sources.Entities
{
    public class Location : Card
    {
        public LocationCardSO cardSO;
        public FunctionTimer runningAction;

        public float DefaultExplorationTime = 10f;
        public override Color DefaultSliderColor { get => GlobalVariables.LOCATION_DefaultSliderColor; }


        public new void Start()
        {
            base.Start();
        }

        public override string GetName() => cardSO.name;

        protected override void TriggerActionsOnSnap(Card receivedCard)
        {
            //Debug.Log($"{cardSO.name} received {receivedCard.GetName()}");
            if (receivedCard is Follower follower)
                Explore(follower);
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
