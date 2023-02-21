using Assets.Sources.Entities;
using Assets.Sources.ScriptableObjects.Cards;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Pnjs
{
    public abstract class PNJActionsSO : ScriptableObject
    {
        public BaseCardSO CardSO; //La Base class évite la référence circulaire

        internal abstract void Talk(Follower follower);
        internal abstract void BuyService(Follower follower, List<Resource> resources);
    }
}
