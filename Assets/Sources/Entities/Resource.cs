using Assets.Sources.ScriptableObjects.Cards;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sources.Entities
{
    internal class Resource : Card
    {
        public ResourceCardSO cardSO;
        public override Color DefaultSliderColor { get => GlobalVariables.RESOURCE_DefaultSliderColor; }

        public override Color ComputeSpecificSliderColor()
        {
            return DefaultSliderColor;
        }

        public override string GetName()
        {
            return cardSO.name;
        }

        protected override void TriggerActionsOnSnap(Card receivedCard)
        {

        }

        protected override void TriggerActionsOnSnap(List<Card> stack)
        {
            throw new System.NotImplementedException();
        }

        public override BaseCardSO GetCardSO()
        {
            return cardSO;
        }
    }
}
