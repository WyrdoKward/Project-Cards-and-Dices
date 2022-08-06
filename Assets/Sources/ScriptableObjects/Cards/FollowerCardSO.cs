using Assets.Sources.Entities;
using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Cards
{
    [CreateAssetMenu(fileName = "New Follower", menuName = "Card/Follower")]
    public class FollowerCardSO : BaseCardSO
    {
        public override Color color => new Color(0, 1, 1);
        public FollowerCardSO()
        {
        }


        public override void InitializedCardWithScriptableObject(GameObject cardBodyGO)
        {
            base.InitializedCardWithScriptableObject(cardBodyGO);

            cardBodyGO.AddComponent<Follower>();
            cardBodyGO.GetComponent<Follower>().cardSO = this;
        }
    }
}
