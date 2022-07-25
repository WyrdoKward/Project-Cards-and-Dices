using Assets.Sources;
using Assets.Sources.Entities;
using Assets.Sources.Systems;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IDropHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private bool isBeeingDragged;

    public Vector3 Lastposition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        isBeeingDragged = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");

        Lastposition = transform.position;

        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        GetComponentInParent<Canvas>().sortingOrder = GlobalVariables.OnDragCardSortingLayer;
        isBeeingDragged = true;

        //TODO mettre ca au endDrag
        //Destruction du Timer slider & function
        var receivedCardGuid = eventData.pointerDrag.GetComponent<Card>().Guid.ToString();
        Destroy(GameObject.Find($"TimerSlider_{receivedCardGuid}"));
        FunctionTimer.StopTimer(receivedCardGuid);

    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    /// <summary>
    /// Is called when a draggableObject is drop in here => implement this on the target
    /// </summary>
    /// <param name="eventData">The card beeing moved</param>
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            //Snapping
            Vector2 targetPosition = GetComponent<RectTransform>().anchoredPosition;
            targetPosition.y -= 70 * GetComponent<RectTransform>().localScale.y;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = targetPosition;

            //Put dragged card on top
            eventData.pointerDrag.GetComponentInParent<Canvas>().sortingOrder = GetComponentInParent<Canvas>().sortingOrder + 1;
            eventData.pointerDrag.GetComponent<Draggable>().isBeeingDragged = false;

            Card targetCard = GetComponent<Card>();
            targetCard.Receivedcard = eventData.pointerDrag;
            targetCard.TriggerActionsOnSnap(eventData.pointerDrag.GetComponent<Card>());
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        if (isBeeingDragged)
            GetComponentInParent<Canvas>().sortingOrder = GlobalVariables.DefaultCardSortingLayer;

        isBeeingDragged = false;

    }
}
