using Assets.Sources.ScriptableObjects.Cards;
using Assets.Sources.Systems;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sources.Entities
{
    /// <summary>
    /// Remove this component form the instanciated prefab to replace it with a children if this entity
    /// </summary>
    public abstract class Card : MonoBehaviour
    {
        /// <summary>
        /// ATTENTION : Implique qu'on ne gère pas les stacks
        /// </summary>
        internal GameObject NextCardInStack;
        internal GameObject PreviousCardInStack;
        internal Vector3 scale;
        internal GameObject gameManager;
        internal TimeManager timeManager;
        public string attachedTimerGuid;

        protected Color defaultSliderColor;

        [SerializeField]
        internal Guid Guid { get; private set; }
        public abstract Color DefaultSliderColor { get; }

        public abstract string GetName();
        public abstract BaseCardSO GetCardSO();
        public abstract Color ComputeSpecificSliderColor(); //Passer en virtual si besoin d'une base() commune

        internal virtual void Start()
        {
            scale = GetComponent<RectTransform>().localScale;
            gameManager = GameObject.Find("_GameManager");
            timeManager = gameManager.GetComponent<TimeManager>();
            Guid = Guid.NewGuid();
            GetComponent<Draggable>().Lastposition = transform.position;
        }

        /// <summary>
        /// Triggered when this card receives an other card.
        /// </summary>
        protected abstract void TriggerActionsOnSnap(Card receivedCard);

        public List<Card> GetStackStartingOnThis()
        {
            var stack = new List<Card> { this };
            if (NextCardInStack == null)
                return stack;

            stack.AddRange(NextCardInStack.GetComponent<Card>().GetStackStartingOnThis());

            return stack;
        }

        public void ReturnToLastPosition()
        {
            transform.position = GetComponent<Draggable>().Lastposition;
        }

        /// <summary>
        /// Créer un timer sur cette carte et lance une action à la fin du temps imparti
        /// </summary>
        /// <param name="action">The method to trigfger at the end of the timer</param>
        /// <param name="duration">In seconds</param>
        /// <param name="receivedCard">Should be null if the timer is on a card alone</param>
        internal void LaunchDelayedActionWithTimer(Action action, float duration, Card receivedCard, bool hasToStopWhenCardIsMoving)
        {
            //On affiche le timer
            var attachedTimerGuid = timeManager.CreateTimerGroup(action, DefaultSliderColor, duration, this, receivedCard, hasToStopWhenCardIsMoving);
            this.attachedTimerGuid = attachedTimerGuid;
            if (receivedCard != null)
                receivedCard.attachedTimerGuid = attachedTimerGuid;
        }

        internal RectTransform GetTransform()
        {
            return this.transform.parent.GetComponent<RectTransform>();
        }

        /// <summary>
        /// True if this card's ReceivedCard match the Guid in parameter
        /// </summary>
        internal bool HasReceivedCardWithGuid(Guid guid)
        {
            if (NextCardInStack == null)
                return false;

            return NextCardInStack.GetComponentInChildren<Card>().Guid == guid;
        }

        /// <summary>
        /// Call this when this card receives "receivedCard"
        /// </summary>
        public virtual void SnapOnIt(GameObject receivedCard)
        {
            // Chain the cards
            NextCardInStack = receivedCard;
            receivedCard.GetComponent<Card>().PreviousCardInStack = this.gameObject;

            // Ici il faut déterminer tout le stack
            // var stack = GetStackBeforeThis()
            // stack.AddRange(GetStackStartingOnThis())
            // var firstCardOfStack
            // firstCardOfStack.TriggerActionsOnSnap(stack)
            TriggerActionsOnSnap(receivedCard.GetComponent<Card>());
        }

        /// <summary>
        /// Call this to remove the snapped card
        /// </summary>
        public virtual void SnapOutOfIt(bool expulseCardOnUI = false)
        {
            if (expulseCardOnUI)
                NextCardInStack.GetComponent<Card>().ReturnToLastPosition();

            NextCardInStack = null;
        }
    }
}
