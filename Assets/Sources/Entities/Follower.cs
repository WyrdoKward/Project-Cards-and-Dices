using Assets.Sources.ScriptableObjects.Cards;
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
