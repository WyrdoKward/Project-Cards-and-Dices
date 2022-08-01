using Assets.Sources.ScriptableObjects.Cards;

namespace Assets.Sources.Entities
{
    internal class Threat : Card
    {
        public ThreatCardSO cardSO;
        public override string GetName()
        {
            return cardSO.name;
        }
    }
}
