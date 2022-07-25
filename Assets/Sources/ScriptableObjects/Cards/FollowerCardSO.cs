using Assets.Sources.Misc;
using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Cards
{
    [CreateAssetMenu(fileName = "New Follower", menuName = "Card/Follower")]
    public class FollowerCardSO : BaseCardSO
    {
        public FollowerCardSO()
        {
            cardType = ECardType.Follower;
        }
    }
}
