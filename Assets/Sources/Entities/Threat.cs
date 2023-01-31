using Assets.Sources.ScriptableObjects.Cards;
using UnityEngine;

namespace Assets.Sources.Entities
{
    internal class Threat : Card
    {
        public ThreatCardSO cardSO;
        public Card handledBy; //remettre à null si le follower part

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

        protected override void TriggerActionsOnSnap(Card receivedCard)
        {
            //Debug.Log($"{cardSO.name} received {receivedCard.GetName()}");
            if (receivedCard is Follower follower)
                handledBy = follower;
        }

        public void AttemptToResolve()
        {
            //TODO Remplacer le random par le 'test'
            //TODO A déplacer directemlent dans ThreatOutcomeSO ?
            var rnd = Random.Range(0, 2);
            if (rnd == 0)
                cardSO.Outcomes.SuccessToPrevent();
            else
                cardSO.Outcomes.FailureToPrevent();
        }

        public override void SnapOnIt(GameObject receivedCard)
        {
            base.SnapOnIt(receivedCard);
            handledBy = receivedCard.GetComponentInChildren<Card>();
        }

        public override void SnapOutOfIt(bool expulseCardOnUI = false)
        {
            base.SnapOutOfIt(expulseCardOnUI);
            handledBy = null;
        }

        public override Color ComputeSpecificSliderColor()
        {
            if (handledBy != null)
                return Color.yellow;

            return DefaultSliderColor;
        }

        public override BaseCardSO GetCardSO()
        {
            return cardSO;
        }
    }
}
