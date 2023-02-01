using Assets.Sources.ScriptableObjects.Cards;
using UnityEngine;

namespace Assets.Sources.Entities
{
    internal class PNJ : Card
    {
        public PNJCardSO cardSO;
        public override Color DefaultSliderColor { get => GlobalVariables.PNJ_DefaultSliderColor; }
        public float TalkingTime = 10f;
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
            //Determine what kind of action according to what is snapped
            //if (receivedCard is Follower follower)
            //    TalkWith(follower);
        }

        public override BaseCardSO GetCardSO()
        {
            return cardSO;
        }

        private void TalkWith(Follower follower)
        {
            Debug.Log($"{follower.GetName()} : bla ?");
            Debug.Log($"{GetName()} : bla bla bla !");
        }
    }
}
