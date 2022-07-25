using Assets.Sources.Entities;
using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Cards
{
    [CreateAssetMenu(fileName = "New Follower", menuName = "Card/Follower")]
    public class FollowerCardSO : BaseCardSO
    {
        public FollowerCardSO()
        {
        }


        public override void InitializedCardWithScriptableObject(GameObject cardBodyGO)
        {
            cardBodyGO.AddComponent<Follower>();
            cardBodyGO.GetComponent<Follower>().cardSO = this;
        }
    }
}
