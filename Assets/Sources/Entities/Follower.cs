﻿using Assets.Sources.ScriptableObjects.Cards;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sources.Entities
{
    public class Follower : Card
    {
        public FollowerCardSO cardSO;

        public override Color DefaultSliderColor { get => GlobalVariables.FOLLOWER_DefaultSliderColor; }

        protected override void TriggerActionsOnSnap(Card receivedCard)
        {

        }


        protected override void TriggerActionsOnSnap(List<Card> stack)
        {
            throw new System.NotImplementedException();
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
