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

        GameObject gameManager;

        public void Start()
        {
            gameManager = GameObject.Find("_GameManager");
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
            if (receivedCard is Follower follower)
                Explore(follower);
        }

        private void Explore(Follower follower)
        {
            Debug.Log($"{cardSO.name} received {follower.name}");
            //Calcul de la durée à partir de this et receivedCard
            var duration = DefaultExplorationTime;

            //Calcul de la position du slider
            var timerPosition = GetComponent<RectTransform>().position;
            //Debug.Log(cardSO.name + " is at " + timerPosition.ToString());
            timerPosition.y += 20;

            //On affiche le timer
            gameManager.GetComponent<TimeManager>().InstanciateTimerSliderOnCard(duration, timerPosition, this.transform.parent.GetComponent<RectTransform>(), follower.Guid.ToString());


            //https://www.youtube.com/watch?v=1hsppNzx7_0
            FunctionTimer.Create(SpawnLoot, duration, follower.Guid.ToString());
        }

        private void SpawnLoot()
        {
            Debug.Log("Spawning loot...");
            gameManager.GetComponent<CardSpawner>().GenerateRandomCardFromList(cardSO.Loot);

            //TODO ejecter receivedCard
            Receivedcard.GetComponent<Card>().ReturnToLastPosition();
        }
    }
}
