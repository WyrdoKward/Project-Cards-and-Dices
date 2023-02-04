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

        /// <summary>
        /// Triggered when this Resource receives an other card.
        /// </summary>
        /// <returns>True if an action has been executed</returns>
        protected override bool TriggerActionsOnSnap(List<Card> stack)
        {
            if (!base.TriggerActionsOnSnap(stack))
                return false;

            //Execute actions on Resources here

            return false;
        }

        public override BaseCardSO GetCardSO()
        {
            return cardSO;
        }
    }
}
