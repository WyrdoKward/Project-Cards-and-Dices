using Assets.Sources.ScriptableObjects.Cards;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sources.Entities
{
    public class Resource : Card
    {
        public ResourceCardSO cardSO;
        public override Color DefaultSliderColor { get => GlobalVariables.RESOURCE_DefaultSliderColor; }

        protected override List<Type> AllowedTypes => null;

        public override Color ComputeSpecificSliderColor()
        {
            return DefaultSliderColor;
        }

        public override string GetName()
        {
            return cardSO.name;
        }

        protected override void TriggerActionsOnSnap(List<Card> stack)
        {
        }

        public override BaseCardSO GetCardSO()
        {
            return cardSO;
        }
    }
}
