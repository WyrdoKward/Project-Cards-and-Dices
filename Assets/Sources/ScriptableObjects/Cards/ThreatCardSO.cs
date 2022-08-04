using Assets.Sources.Entities;
using Assets.Sources.ScriptableObjects.Actions;
using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Cards
{
    [CreateAssetMenu(fileName = "New Threat", menuName = "Card/Threat")]
    internal class ThreatCardSO : BaseCardSO
    {
        public float ThreatTime;
        public float NegateTime;

        public ThreatOutcomeSO Actions; // Voir pour le DP Decorator ou Strategy. Et commencer par coder le spawn de la carte pour voir ce qui est accessible depuis le reste ?

        //public Dice[] dice;

        public override void InitializedCardWithScriptableObject(GameObject cardBodyGO)
        {
            base.InitializedCardWithScriptableObject(cardBodyGO);
            Actions.thisCardGameObject = cardBodyGO;

            cardBodyGO.AddComponent<Threat>();
            cardBodyGO.GetComponent<Threat>().cardSO = this;
        }
    }
}
