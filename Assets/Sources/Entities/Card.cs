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
        public virtual void TriggerActionsOnSnap(Card receivedCard)
        {

        }

        public void ReturnToLastPosition()
        {
            transform.position = GetComponent<Draggable>().Lastposition;
        }

        /// <summary>
        /// Créer un timer sur cette carte et lance une action à la fin du temps imparti
        /// </summary>
        internal void LaunchDelayedActionWithTimer(Action action, float duration, Card receivedCard, bool hasToStopWhenCardIsMoving)
        {
            //On affiche le timer
            var attachedTimerGuid = timeManager.CreateTimerGroup(action, duration, this, receivedCard, hasToStopWhenCardIsMoving);
            this.attachedTimerGuid = attachedTimerGuid;
            if (receivedCard != null)
                receivedCard.attachedTimerGuid = attachedTimerGuid;

            //timeManager.InstanciateTimerSliderOnCard(action, duration, GetTransform(), receivedCardGuid, hasToStopWhenCardIsMoving);
        }

        internal RectTransform GetTransform()
        {
            return this.transform.parent.GetComponent<RectTransform>();
        }
    }
}
