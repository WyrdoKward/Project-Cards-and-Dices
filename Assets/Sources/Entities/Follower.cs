using Assets.Sources.ScriptableObjects.Cards;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sources.Entities
{
    public class Follower : Card
    {
        public FollowerCardSO cardSO;

        public override Color DefaultSliderColor { get => GlobalVariables.FOLLOWER_DefaultSliderColor; }

        protected override List<Type> AllowedTypes => null;

        /// <summary>
        /// Triggered when this Follower receives an other card.
        /// </summary>
        /// <returns>True if an action has been executed</returns>
        protected override bool TriggerActionsOnSnap(List<Card> stack)
        {
            if (!base.TriggerActionsOnSnap(stack))
                return false;

            //Execute actions on Follower here

            return false;
        }

        public override string GetName()
        {
            return cardSO.name;
        }

        public override Color ComputeSpecificSliderColor()
        {
            return DefaultSliderColor;
        }

        public override BaseCardSO GetCardSO()
        {
            return cardSO;
        }
    }
}
