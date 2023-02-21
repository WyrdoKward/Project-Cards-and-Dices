using Assets.Sources.ScriptableObjects.Cards;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sources.Entities
{
    public class Threat : Card
    {
        public ThreatCardSO cardSO;
        public Card handledBy; //remettre à null si le follower part

        public override Color DefaultSliderColor { get => GlobalVariables.THREAT_DefaultSliderColor; }

        protected override List<System.Type> AllowedTypes => new() { typeof(Follower) };

        public override string GetName()
        {
            return cardSO.name;
        }

        public new void Start()
        {
            base.Start();

            LaunchDelayedActionWithTimer(cardSO.Outcomes.DetermineOutcome, cardSO.ThreatTime, null, false);
        }

        /// <summary>
        /// Triggered when this Threat receives an other card.
        /// </summary>
        /// <returns>True if an action has been executed</returns>
        protected override bool TriggerActionsOnSnap(List<Card> stack)
        {
            if (!base.TriggerActionsOnSnap(stack))
                return false;

            //Execute actions on Threat here
            if (stackHolder.Followers.Count == 1)
            {
                handledBy = stackHolder.Followers[0];
                return true;
            }

            return false;
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
