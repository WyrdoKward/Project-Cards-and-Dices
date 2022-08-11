using Assets.Sources.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Sources.Systems.Timers
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
        public Vector2 ReferencePosition => Cards[0].GetTransform().position;

        public TimerGroup(List<Card> cards, Action action, float duration, bool hasToStopWhenCardIsMoving)
        {
            Cards = VerifyOrder(cards);
            var guid = ComputeGuids(cards);

            //Le FunctionTimer & le slider doivent être créés ici à partir des cartes reçues
            //TimeManager.InstanciateTimerSliderOnCard
            FunctionTimer.Create(action, duration, hasToStopWhenCardIsMoving, guid);

            TimeManager.TimerGroups.Add(this);
        }

        /// <summary>
        /// Reorder cards by X axis. Index 0 is at the bottom
        /// </summary>
        private List<Card> VerifyOrder(List<Card> cards) { return cards; }

        private string ComputeGuids(List<Card> cards)
        {
            var sb = new StringBuilder();
            var last = cards.Last();
            foreach (var card in cards)
            {
                sb.Append(card.Guid);
                if (!card.Equals(last))
                    sb.Append("+");
            }
            return sb.ToString();
        }

    }
}
