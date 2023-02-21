using Assets.Sources.Entities;
using Assets.Sources.ScriptableObjects.Cards;
using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Pnjs
{
    [CreateAssetMenu(fileName = "PnjName", menuName = "Card/PNJ/NewPnj")]
    public class PNJCardSO : BaseCardSO
    {
        [Tooltip("True if it should diseapear after a fixed time")]
        public bool IsTemporaray;
        public float Duration;
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
