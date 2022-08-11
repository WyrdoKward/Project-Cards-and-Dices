using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sources.Systems.Timer
{
    /// <summary>
    /// Regroup cards and timers
    /// </summary>
    public class TimerGroup
    {
        public List<Card> Cards;
        public FunctionTimer FunctionTimer;
        public GameObject TimerSlider;

        /// <summary>
        /// Bottom card position for the slider GO to follow at update
        /// </summary>
        public Vector2 ReferencePosition{
            get {
                return Cards[0].GetTransform().position;  //GetTransform renvoie card.transform.parent.GetComponent<RectTransform>()     
            };
        }

        public Timergroup(List<Card> cards, Action action, float duration){
            Cards = VerifyOrder(cards); //Vérifier l'ordre des cartes dans la liste selon l'axes X ?
            string guid = ComputeGuids(cards)//Concaténer les guid des cards

            //Le FunctionTimer & le slider doivent être créés ici à partir des cartes reçues
            //TimeManager.InstanciateTimerSliderOnCard
            FunctionTimer.Create(action, duration, guid);
        }

        private List<Cards> VerifyOrder(List<Cards> cards){}
        private string ComputeGuids(List<Cards> cards){}

    }
}
