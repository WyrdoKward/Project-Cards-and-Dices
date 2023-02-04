using Assets.Sources.Entities;
using Assets.Sources.ScriptableObjects.Cards;
using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Pnjs
{
    [CreateAssetMenu(fileName = "PnjName", menuName = "Card/PNJ/NewPnj")]
    public class PNJCardSO : BaseCardSO
    {
        public PNJActionsSO Actions;
        public override Color BgColor => Color.magenta;

        public override void InitializedCardWithScriptableObject(GameObject cardBodyGO)
        {
            base.InitializedCardWithScriptableObject(cardBodyGO);

            cardBodyGO.AddComponent<PNJ>();
            cardBodyGO.GetComponent<PNJ>().cardSO = this;
        }
    }
}
