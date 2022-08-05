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

            LaunchTimer(cardSO.Actions.ExecuteThreat, cardSO.ThreatTime, this.Guid.ToString());
        }


        public override void TriggerActionsOnSnap(Card receivedCard)
        {
            Debug.Log($"{cardSO.name} received {receivedCard.GetName()}");
            if (receivedCard is Follower follower)
                LaunchTimer(cardSO.Actions.ExecuteThreat, cardSO.NegateTime, this.Guid.ToString());
        }


        private void DetermineOutcome()
        {
            if (true)
                cardSO.Actions.SuccessToPrevent();
            else
                cardSO.Actions.FailureToPrevent();
        }
    }
}
