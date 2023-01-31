using Assets.Sources.ScriptableObjects.Cards;
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
    }
}
