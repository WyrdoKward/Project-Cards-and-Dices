using Assets.Sources.ScriptableObjects.Cards;
using UnityEngine;

namespace Assets.Sources.Entities
{
    internal class Threat : Card
    {
        public ThreatCardSO cardSO;
        public override string GetName()
        {
            return cardSO.name;
        }

        public new void Start()
        {
            base.Start();

            timeManager.InstanciateTimerSliderOnCard(DetermineOutcome, cardSO.ThreatTime, this.transform.parent.GetComponent<RectTransform>(), this.Guid.ToString());
        }

        private void DetermineOutcome()
        {
            if (true)
                cardSO.Actions.Success();
            else
                cardSO.Actions.Failure();
        }
    }
}
