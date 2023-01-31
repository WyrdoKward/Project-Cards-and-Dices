using Assets.Sources.Entities;
using Assets.Sources.ScriptableObjects.Actions;
using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Cards
{
    [CreateAssetMenu(fileName = "New Threat", menuName = "Card/Threat")]
    internal class ThreatCardSO : BaseCardSO
    {
        public float ThreatTime;

        public ThreatOutcomeSO Outcomes; // Voir pour le DP Decorator ou Strategy. Et commencer par coder le spawn de la carte pour voir ce qui est accessible depuis le reste ?

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
