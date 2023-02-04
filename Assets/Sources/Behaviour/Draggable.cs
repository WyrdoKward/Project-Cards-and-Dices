using Assets.Sources;
using Assets.Sources.Entities;
using Assets.Sources.Providers;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IDropHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private bool isBeeingDragged;
    private GameObject lastCardThisWasSnappedOnto;

    public Vector3 Lastposition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        isBeeingDragged = false;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");

        Lastposition = transform.position;

        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        GetComponentInParent<Canvas>().sortingOrder = GlobalVariables.OnDragCardSortingLayer;
        isBeeingDragged = true;

        //on sauvegarde la dernière carte sur laquelle on était snappé
        var thisCardGuid = eventData.pointerDrag.GetComponent<Card>().Guid;
        var allCards = GameObject.Find("_GameManager").GetComponent<CardProvider>().AllCardGameObjectsInGame();
        lastCardThisWasSnappedOnto = allCards.FirstOrDefault(go => go.GetComponentInChildren<Card>().HasReceivedCardWithGuid(thisCardGuid));

        //Inutile ici car c'est le ENdDrag qui détruit le timer au SnapOut
        //Destruction du Timer slider & function
        //var draggerCardGuid = eventData.pointerDrag.GetComponent<Card>().Guid.ToString();
        //var timerToDestroy = GameObject.Find($"TimerSlider_{draggerCardGuid}");
        //if (timerToDestroy != null)
        //    Destroy(timerToDestroy);
        //TimeManager.StopTimerFromMovement(draggerCardGuid);

    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        var deltaPosition = eventData.delta / canvas.scaleFactor;
        rectTransform.anchoredPosition += deltaPosition;

        //Moving attached cards aswell
        MoveAttachedCard(eventData.pointerDrag.GetComponent<Card>(), deltaPosition);
    }

    /// <summary>
    /// Checks if card has a "ReceivedCard" an moves it along if so.
    /// </summary>
    private void MoveAttachedCard(Card card, Vector2 delta)
    {
        var receivedCard = card.NextCardInStack;
        if (receivedCard == null)
            return;

        var receivedCardTransform = receivedCard.GetComponent<RectTransform>();
        receivedCardTransform.anchoredPosition += delta;
        MoveAttachedCard(receivedCard.GetComponent<Card>(), delta);
    }

    //protected virtual void OnEndDrag(PointerEventData eventData)
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag base");

        //Si elle était ReceivedCard d'un autre carte il faut l'enlever
        if (lastCardThisWasSnappedOnto != null)
        {
            lastCardThisWasSnappedOnto.GetComponentInChildren<Card>().SnapOutOfIt(false);
        }

        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        if (isBeeingDragged)
            GetComponentInParent<Canvas>().sortingOrder = GlobalVariables.DefaultCardSortingLayer;

        isBeeingDragged = false;

    }

    /// <summary>
    /// Is called when a draggableObject is drop in here => implement this on the target
    /// </summary>
    /// <param name="eventData">The card beeing moved</param>
    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        //Appellé quand on reçoit une carte
        //Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            //Snapping UI
            var targetPosition = GetComponent<RectTransform>().anchoredPosition;
            targetPosition.y -= 70 * GetComponent<RectTransform>().localScale.y;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = targetPosition;

            //Put dragged card on top
            eventData.pointerDrag.GetComponentInParent<Canvas>().sortingOrder = GetComponentInParent<Canvas>().sortingOrder + 1;
            eventData.pointerDrag.GetComponent<Draggable>().isBeeingDragged = false;

            //Buisiness on snapping
            var targetCard = GetComponent<Card>();
            targetCard.SnapOnIt(eventData.pointerDrag);
        }
    }
}
