using System;
using UnityEngine;

namespace Assets.Sources.Entities
{
    /// <summary>
    /// Remove this component form the instanciated prefab to replace it with a children if this entity
    /// </summary>
    public class Card : MonoBehaviour
    {
        internal GameObject Receivedcard;
        internal Vector3 scale;
        [SerializeField]
        internal Guid Guid { get; private set; }

        private void Start()
        {
            scale = GetComponent<RectTransform>().localScale;
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


    }
}
