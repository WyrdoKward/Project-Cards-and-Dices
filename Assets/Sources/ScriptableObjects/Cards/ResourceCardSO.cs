using Assets.Sources.Entities;
using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Cards
{
    [CreateAssetMenu(fileName = "New Resource", menuName = "Card/Resource")]
    internal class ResourceCardSO : BaseCardSO
    {
        public ResourceCardSO()
        { }

        public override void InitializedCardWithScriptableObject(GameObject cardBodyGO)
        {
            cardBodyGO.AddComponent<Resource>();
            cardBodyGO.GetComponent<Resource>().cardSO = this;
        }
    }
}
