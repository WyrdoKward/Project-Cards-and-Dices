using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Sources.Behaviour
{
    /// <summary>
    /// A utiuliser si a l'avenir on a besoin de comportements spécifiques pour les drags
    /// </summary>
    internal class DraggableFollower : Draggable, IEndDragHandler
    {
        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag Follower");
            //base.OnEndDrag(eventData);

        }
    }
}
