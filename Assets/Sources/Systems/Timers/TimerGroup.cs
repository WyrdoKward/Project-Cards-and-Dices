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
        public string Guid;

        private readonly GameObject timerHookGO;
        private bool isFinished;

        /// <summary>
        /// Bottom card position for the slider GO to follow at update
        /// </summary>
        public Vector2 ReferencePosition => Cards[0].GetTransform().position;

        public TimerGroup(List<Card> cards, Action action, float duration, bool hasToStopWhenCardIsMoving)
        {
            Cards = VerifyOrder(cards);
            ComputeGuids(cards);

            timerHookGO = new GameObject($"TimerHook-{Guid}", typeof(MonoBehaviourHook));
            timerHookGO.GetComponent<MonoBehaviourHook>().onUpdate = Update;

            //On attache l'action à un timer
            //https://www.youtube.com/watch?v=1hsppNzx7_0
            FunctionTimer = FunctionTimer.Create(action, duration, hasToStopWhenCardIsMoving, Guid);
        }

        private void Update()
        {
            if (isFinished)
                return;

            isFinished = FunctionTimer.Update();

            if (isFinished)
                Stop();
        }

        public void Stop()
        {
            Cards.ForEach(card => card.attachedTimerGuid = "");
            FunctionTimer = null;

            isFinished = true;
            UnityEngine.Object.Destroy(timerHookGO);
            DisperseCards();
        }


        /// <summary>
        /// On renvoie toutes les cartes superposées à leur dernière position
        /// </summary>
        private void DisperseCards()
        {
            for (var i = 0; i < Cards.Count; i++)
            {
                if (i != 0)
                    Cards[i].ReturnToLastPosition();
            }
        }

        /// <summary>
        /// Reorder cards by SortingLayer. Index 0 is at the bottom
        /// </summary>
        private List<Card> VerifyOrder(List<Card> cards)
        {
            //TODO 
            return cards;
        }

        private void ComputeGuids(List<Card> cards)
        {
            var sb = new StringBuilder();

            cards.RemoveAll(c => c == null);

            var last = cards.Last();
            foreach (var card in cards)
            {
                sb.Append(card.Guid);
                if (!card.Equals(last))
                    sb.Append("+");
            }
            Guid = sb.ToString();
        }


        /// <summary>
        /// Dummy class to access MonoBehaviour functions
        /// </summary>
        private class MonoBehaviourHook : MonoBehaviour
        {
            public Action onUpdate;
            private void Update()
            {
                if (onUpdate != null)
                    onUpdate();
            }
        }

    }
}
