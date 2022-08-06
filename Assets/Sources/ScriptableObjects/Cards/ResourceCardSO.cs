using Assets.Sources.Entities;
using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Cards
{
    [CreateAssetMenu(fileName = "New Resource", menuName = "Card/Resource")]
    internal class ResourceCardSO : BaseCardSO
    {
        public override Color color => new Color(0, 1, 0);
        public ResourceCardSO()
        { }

        public override void InitializedCardWithScriptableObject(GameObject cardBodyGO)
        {
            base.InitializedCardWithScriptableObject(cardBodyGO);

            cardBodyGO.AddComponent<Resource>();
            cardBodyGO.GetComponent<Resource>().cardSO = this;
        }
    }
}
