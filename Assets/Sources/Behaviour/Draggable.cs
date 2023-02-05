using Assets.Sources;
using Assets.Sources.Entities;
using Assets.Sources.Providers;
using Assets.Sources.Tools;
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
        //Debug.Log($"Moving {card.GetName()} ( + {card.NextCardInStack?.GetComponent<Card>().GetName()})");
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
        //Si elle était ReceivedCard d'un autre carte il faut l'enlever
        if (lastCardThisWasSnappedOnto != null)
        {
            lastCardThisWasSnappedOnto.GetComponentInChildren<Card>().SnapOutOfIt(false);
        }

        //Appliquer les effets d'UI pour cette carte et toutes celles qui lui sont stackées au dessus
        var sortingOrder = GlobalVariables.DefaultCardSortingLayer;
        if (eventData.pointerDrag.GetComponent<Card>().PreviousCardInStack != null)
            sortingOrder = eventData.pointerDrag.GetComponentInParent<Canvas>().sortingOrder;

        var stack = GetComponent<Card>().GetNextGameObjectsInStack();
        CardHelper.ApplyToGameObjects(stack, (go, args) =>
        {
            Debug.Log($"Apply to {go.GetComponent<Card>().GetName()}, sorting order = {sortingOrder}");
            go.GetComponent<Draggable>().canvasGroup.alpha = 1;
            go.GetComponent<Draggable>().canvasGroup.blocksRaycasts = true;
            //if (go.GetComponent<Draggable>().isBeeingDragged) TODO : A décommenter une fois qu'on a fait CardHelper.ApplyToGameObjects sur OnBeginDrag
            go.GetComponentInParent<Canvas>().sortingOrder = sortingOrder;
            go.GetComponent<Draggable>().isBeeingDragged = false;
            sortingOrder++;
        },
        new object[] { sortingOrder, isBeeingDragged })
    }

    /// <summary>
    /// Is called when a draggableObject is drop in here => implement this on the target
    /// </summary>
    /// <param name="eventData">The card beeing moved</param>
    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        //Appellé quand on reçoit une carte
        Debug.Log("OnDrop");
        if (eventData.pointerDrag == null)
            return;

        var targetCard = GetComponent<Card>();
        var droppedCard = eventData.pointerDrag.GetComponent<Card>();
        //var stack = targetCard.GetFullStack();
        //targetCard.DebugPrintStack(stack);

        //var stack2 = droppedCard.GetFullStack();
        //droppedCard.DebugPrintStack(stack2);

        //Si les 2 cartes sont dcans le meme stack, on ne les resnap pas
        if (targetCard.IsInSameStack(droppedCard))
            return;


        Debug.Log($"{GetComponent<Card>().GetName()} receiving : {droppedCard.GetName()}");

        var targetPosition = GetComponent<RectTransform>().anchoredPosition;
        var baseSortingOrder = GetComponentInParent<Canvas>().sortingOrder;

        foreach (var cardGO in droppedCard.GetNextGameObjectsInStack())
        {
            ////Snapping UI
            targetPosition.y -= 70 * GetComponent<RectTransform>().localScale.y;
            cardGO.GetComponent<RectTransform>().anchoredPosition = targetPosition;
            //Put dragged card on top
            baseSortingOrder += 1;
            cardGO.GetComponentInParent<Canvas>().sortingOrder = baseSortingOrder;
            cardGO.GetComponent<Draggable>().isBeeingDragged = false;
        }

        //Buisiness on snapping
        targetCard.SnapOnIt(eventData.pointerDrag);
    }
}
