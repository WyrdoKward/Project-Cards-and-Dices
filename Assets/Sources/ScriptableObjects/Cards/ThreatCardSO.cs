using Assets.Sources.Entities;
using Assets.Sources.ScriptableObjects.Actions;
using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Cards
{
    [CreateAssetMenu(fileName = "New Threat", menuName = "Card/Threat")]
    public class ThreatCardSO : BaseCardSO
    {
        public float ThreatTime;

        public ThreatOutcomeSO Outcomes; // Implémenté via un DP Strategy

        public override Color BgColor => new Color(1, 0, 0);

        //public Dice[] dice;

        public override void InitializedCardWithScriptableObject(GameObject cardBodyGO)
        {
            base.InitializedCardWithScriptableObject(cardBodyGO);
            Outcomes.SetCardBodyGameObject(cardBodyGO);
            Outcomes.CardSO = this;
            Outcomes.GameManager = GameObject.Find("_GameManager");

            cardBodyGO.AddComponent<Threat>();
            cardBodyGO.GetComponent<Threat>().cardSO = this;
        }
    }
}
