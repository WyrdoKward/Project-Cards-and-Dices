using Assets.Sources.Entities;
using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Cards
{
    [CreateAssetMenu(fileName = "New PNJ", menuName = "Card/PNJ")]
    internal class PNJCardSO : BaseCardSO
    {
        public override Color color => Color.magenta;

        public override void InitializedCardWithScriptableObject(GameObject cardBodyGO)
        {
            base.InitializedCardWithScriptableObject(cardBodyGO);

            cardBodyGO.AddComponent<PNJ>();
            cardBodyGO.GetComponent<PNJ>().cardSO = this;
        }
    }
}
