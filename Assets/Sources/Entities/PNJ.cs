using Assets.Sources.ScriptableObjects.Cards;
using System;
using UnityEngine;

namespace Assets.Sources.Entities
{
    internal class PNJ : Card
    {
        public PNJCardSO cardSO;
        public override Color DefaultSliderColor { get => GlobalVariables.PNJ_DefaultSliderColor; }

        public override Color ComputeSpecificSliderColor()
        {
            throw new NotImplementedException();
        }

        public override string GetName()
        {
            return cardSO.name;
        }

        protected override void TriggerActionsOnSnap(Card receivedCard)
        {
            throw new NotImplementedException();
        }

        public override BaseCardSO GetCardSO()
        {
            return cardSO;
        }
    }
}
