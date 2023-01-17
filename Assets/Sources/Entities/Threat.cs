using Assets.Sources.ScriptableObjects.Cards;
using UnityEngine;

namespace Assets.Sources.Entities
{
    internal class Threat : Card
    {
        public ThreatCardSO cardSO;
        public Card handledBy;

        public override Color DefaultSliderColor { get => GlobalVariables.THREAT_DefaultSliderColor; }

        public override string GetName()
        {
            return cardSO.name;
        }

        public new void Start()
        {
            base.Start();

            LaunchDelayedActionWithTimer(cardSO.Outcomes.DetermineOutcome, cardSO.ThreatTime, null, false);
        }

        public override void TriggerActionsOnSnap(Card receivedCard)
        {
            //Debug.Log($"{cardSO.name} received {receivedCard.GetName()}");
            if (receivedCard is Follower follower)
                handledBy = follower;
        }

        public void AttemptToResolve()
        {
            var rnd = Random.Range(0, 2);
            if (rnd == 0)
                cardSO.Outcomes.SuccessToPrevent();
            else
                cardSO.Outcomes.FailureToPrevent();
        }
    }
}
