using Assets.Sources.ScriptableObjects.Cards;
using UnityEngine;

namespace Assets.Sources.Entities
{
    internal class Threat : Card
    {
        public ThreatCardSO cardSO;
        public override string GetName()
        {
            return cardSO.name;
        }

        public new void Start()
        {
            base.Start();

            LaunchDelayedActionWithTimer(cardSO.Actions.ExecuteThreat, cardSO.ThreatTime, this.Guid.ToString());
        }


        public override void TriggerActionsOnSnap(Card receivedCard)
        {
            Debug.Log($"{cardSO.name} received {receivedCard.GetName()}");
            if (receivedCard is Follower follower)
                LaunchDelayedActionWithTimer(DetermineOutcome, cardSO.NegateTime, this.Guid.ToString());
        }


        private void DetermineOutcome()
        {
            var rnd = Random.Range(0, 2);
            if (rnd == 0)
                cardSO.Actions.SuccessToPrevent();
            else
                cardSO.Actions.FailureToPrevent();
        }
    }
}
