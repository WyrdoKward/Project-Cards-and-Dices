using Assets.Sources.Systems;
using System;
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
        internal GameObject Receivedcard;
        internal Vector3 scale;
        internal GameObject gameManager;
        internal TimeManager timeManager;
        public string attachedTimerGuid;

        protected Color defaultSliderColor;

        [SerializeField]
        internal Guid Guid { get; private set; }
        public abstract Color DefaultSliderColor { get; }

        public abstract string GetName();
        public abstract Color ComputeSpecificSliderColor(); //Passer en virtual si besoin d'une base() commune

        internal virtual void Start()
        {
            scale = GetComponent<RectTransform>().localScale;
            gameManager = GameObject.Find("_GameManager");
            timeManager = gameManager.GetComponent<TimeManager>();
            Guid = Guid.NewGuid();
            GetComponent<Draggable>().Lastposition = transform.position;
            Debug.Log($"Card.Start : {GetName()} at {transform.position}");
        }

        /// <summary>
        /// Triggered when this card receives an other card.
        /// </summary>
        protected abstract void TriggerActionsOnSnap(Card receivedCard);

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
            if (Receivedcard == null)
                return false;

            return Receivedcard.GetComponentInChildren<Card>().Guid == guid;
        }

        /// <summary>
        /// Call this when this card receives "receivedCard"
        /// </summary>
        public virtual void SnapOnIt(GameObject receivedCard)
        {
            Receivedcard = receivedCard;
            TriggerActionsOnSnap(receivedCard.GetComponent<Card>());
        }

        /// <summary>
        /// Call this to remove the snapped card
        /// </summary>
        public virtual void SnapOutOfIt(bool expulseCardOnUI = false)
        {
            if (expulseCardOnUI)
                Receivedcard.GetComponent<Card>().ReturnToLastPosition();

            Receivedcard = null;
        }
    }
}
