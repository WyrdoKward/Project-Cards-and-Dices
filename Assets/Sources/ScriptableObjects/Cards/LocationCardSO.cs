using Assets.Sources.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Cards
{
    [CreateAssetMenu(fileName = "New Location", menuName = "Card/Location")]
    public class LocationCardSO : BaseCardSO
    {
        public List<BaseCardSO> Loot;

        public LocationCardSO()
        {
        }

        public override void InitializedCardWithScriptableObject(GameObject cardBodyGO)
        {
            base.InitializedCardWithScriptableObject(cardBodyGO);

            cardBodyGO.AddComponent<Location>();
            cardBodyGO.GetComponent<Location>().cardSO = this;
        }
    }
}
