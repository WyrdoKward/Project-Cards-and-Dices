using Assets.Sources.Misc;
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
            cardType = ECardType.Location;
        }
    }
}
