using Assets.Sources.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Cards
{
    [CreateAssetMenu(fileName = "New Location", menuName = "Card/Location")]
    public class LocationCardSO : BaseCardSO
    {
        public override Color color => new Color(0, 0, 1);
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
