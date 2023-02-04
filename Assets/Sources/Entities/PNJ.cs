using Assets.Sources.ScriptableObjects.Cards;
using Assets.Sources.ScriptableObjects.Pnjs;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sources.Entities
{
    public class PNJ : Card
    {
        public PNJCardSO cardSO;
        public override Color DefaultSliderColor { get => GlobalVariables.PNJ_DefaultSliderColor; }

        protected override List<Type> AllowedTypes => new() { typeof(Follower), typeof(Resource) };

        public float TalkingTime = 10f;
        public override Color ComputeSpecificSliderColor()
        {
            return DefaultSliderColor;
        }

        public override string GetName()
        {
            return cardSO.name;
        }

        /// <summary>
        /// Triggered when this PNJ receives an other card.
        /// </summary>
        /// <returns>True if an action has been executed</returns>
        protected override bool TriggerActionsOnSnap(List<Card> stack)
        {
            if (!base.TriggerActionsOnSnap(stack))
                return false;

            //Execute actions on PNJ here
            if (stackHolder.Followers.Count == 1 && stackHolder.Resources.Count == 0)
            {
                cardSO.Actions.Talk(stackHolder.Followers[0]);
                return true;
            }

            if (stackHolder.Followers.Count == 1 && stackHolder.Resources.Count > 0)
            {
                cardSO.Actions.BuyService(stackHolder.Followers[0], stackHolder.Resources);
                return true;
            }

            return false;
        }

        public override BaseCardSO GetCardSO()
        {
            return cardSO;
        }
    }
}
