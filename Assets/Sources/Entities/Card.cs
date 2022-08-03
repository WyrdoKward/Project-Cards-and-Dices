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

        [SerializeField]
        internal Guid Guid { get; private set; }

        public abstract string GetName();

        internal virtual void Start()
        {
            scale = GetComponent<RectTransform>().localScale;
            gameManager = GameObject.Find("_GameManager");
            timeManager = gameManager.GetComponent<TimeManager>();
            Guid = Guid.NewGuid();
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
        internal void LaunchTimer(Action action, float duration, string receivedCardGuid)
        {
            //Calcul de la position du slider
            /*var timerPosition = GetComponent<RectTransform>().position;
            timerPosition.y += 20;*/

            //On affiche le timer
            timeManager.InstanciateTimerSliderOnCard(action, duration, this.transform.parent.GetComponent<RectTransform>(), receivedCardGuid);

        }
    }
}
