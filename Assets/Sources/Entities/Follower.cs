using Assets.Sources.ScriptableObjects.Cards;

namespace Assets.Sources.Entities
{
    public class Follower : Card
    {
        public FollowerCardSO cardSO;

        public override void TriggerActionsOnSnap(Card receivedCard)
        {

        }


        public override string GetName()
        {
            return cardSO.name;
        }
    }
}
