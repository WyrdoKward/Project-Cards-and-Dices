using Assets.Sources.Entities;
using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Cards
{
    [CreateAssetMenu(fileName = "New Threat", menuName = "Card/Threat")]
    internal class ThreatCardSO : BaseCardSO
    {
        public float ThreatTime;
        public float NegateTime;
        public bool IsLoop;

        public ScriptableObject Actions; // Ca marche pas, voir pour le DP Decorator. Et commencer par coder le spawn de la carte pour voir ce qui est accessible depuis le reste ?

        //public Dice[] dice;

        public override void InitializedCardWithScriptableObject(GameObject cardBodyGO)
        {
            cardBodyGO.AddComponent<Threat>();
            cardBodyGO.GetComponent<Threat>().cardSO = this;
        }
    }
}
