using Assets.Sources.ScriptableObjects.Cards;

namespace Assets.Sources.Entities
{
    internal class Resource : Card
    {
        public ResourceCardSO cardSO;

        public override string GetName()
        {
            return cardSO.name;
        }
    }
}
