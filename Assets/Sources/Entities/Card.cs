using Assets.Sources.Misc;
using Assets.Sources.ScriptableObjects.Cards;
using Assets.Sources.ScriptableObjects.Dices;
using Assets.Sources.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Sources.Entities
{
    /// <summary>
    /// Remove this component form the instanciated prefab to replace it with a children if this entity
    /// </summary>
    public abstract class Card : MonoBehaviour, IPointerClickHandler
    {
        /// <summary>
        /// The card above
        /// </summary>
        internal GameObject NextCardInStack;
        /// <summary>
        /// The card under
        /// </summary>
        internal GameObject PreviousCardInStack;
        internal Vector3 scale;
        internal GameObject gameManager;
        internal TimeManager timeManager;
        public string attachedTimerGuid;

        protected Color defaultSliderColor;
        protected StackHolder stackHolder;

        /// <summary>
        /// Les types de cartes autorisées à stacker sur cette carte
        /// </summary>
        protected abstract List<Type> AllowedTypes { get; }

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
        /// <returns>False if no action is to be executed</returns>
        protected virtual bool TriggerActionsOnSnap(List<Card> stack)
        {
            if (stack.Count == 0)
            {
                Debug.LogWarning($"Stack vide sur {GetName()}");
                return false;
            }

            stackHolder = new StackHolder(stack, AllowedTypes);

            return !stackHolder.HasUselessCards();
        }

        public List<Card> GetFullStack()
        {
            var stack = GetPreviousCardsInStack();
            stack.RemoveAt(0); // "This" sera ajouté par GetNextCardsInStack
            stack.Reverse();
            stack.AddRange(GetNextCardsInStack());

            return stack;
        }

        /// <summary>
        /// La carte actuelle sera la dernière carte de la liste retournée
        /// </summary>
        private List<Card> GetPreviousCardsInStack()
        {
            var stack = new List<Card>() { this };
            if (PreviousCardInStack == null)
                return stack;

            stack.AddRange(PreviousCardInStack.GetComponent<Card>().GetPreviousCardsInStack());

            return stack;
        }

        /// <summary>
        /// La première carte de la liste retournée sera la carte actuelle
        /// </summary>
        public List<Card> GetNextCardsInStack()
        {
            var stack = new List<Card> { this };
            if (NextCardInStack == null)
                return stack;

            stack.AddRange(NextCardInStack.GetComponent<Card>().GetNextCardsInStack());

            return stack;
        }

        /// <summary>
        /// Wrapper for GetNextCardsInStack that returns the GO of the cards
        /// </summary>
        public List<GameObject> GetNextGameObjectsInStack()
        {
            return GetNextCardsInStack().Select(c => c.gameObject).ToList();
        }

        public bool IsInSameStack(Card card)
        {
            foreach (var c in GetFullStack())
            {
                if (c.Guid == card.Guid)
                    return true;
            }
            return false;
        }
        //TODO déplacer dans un script dédié à l'UI
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
            Debug.Log($"{receivedCard.GetComponent<Card>().GetName()} snaps on {GetName()}");
            // Chain the cards
            NextCardInStack = receivedCard;
            receivedCard.GetComponent<Card>().PreviousCardInStack = this.gameObject;

            // Ici il faut déterminer tout le stack
            var stack = GetFullStack();
            //DebugPrintStack(stack);
            var firstCardOfStack = stack.FirstOrDefault();
            stack.RemoveAt(0); //Remove first card for convenience later
            var hasActionBeenExecuted = firstCardOfStack.TriggerActionsOnSnap(stack);
        }

        /// <summary>
        /// Call this to remove the snapped card
        /// </summary>
        public virtual void SnapOutOfIt(bool expulseCardOnUI = false)
        {
            if (NextCardInStack == null)
                return;

            Debug.Log($"{NextCardInStack.GetComponent<Card>().GetName()} snaps out of {GetName()}");
            if (expulseCardOnUI)
                NextCardInStack.GetComponent<Card>().ReturnToLastPosition();

            NextCardInStack.GetComponent<Card>().PreviousCardInStack = null;
            NextCardInStack = null;
        }

        public void DestroySelf()
        {
            //On expulse proprement les cartes attachées
            SnapOutOfIt(true);
            if (PreviousCardInStack != null)
                PreviousCardInStack.GetComponent<Card>().SnapOutOfIt(false);
            Destroy(transform.parent.gameObject);
        }

        //TODO A déplacer ans un script DiceLogic à poser sur le GO ?
        protected int RollDice<T>()
        {
            //T[] dices = cardSO.Dices.Where(d => d is T);//cardSO ?? sera peut être + accessible depuis DiceLogic avec une ref...
            //Ou alors implémenter directement RollCombatDice() sur Follower.cs
            return 1;
        }

        public int RollCombatDices()
        {
            BaseCardSO cardSO = null;
            if (this is Follower f)
                cardSO = f.cardSO;
            else if (this is Threat t)
                cardSO = t.cardSO;
            else
                throw new InvalidCastException($"Type of {GetName()} not handled for combat dices.");

            var dicesToRoll = cardSO.Dices.Where(d => d is CombatDiceSO);
            var res = 0;
            foreach (var d in dicesToRoll)
            {
                res += d.Roll();
            }
            Debug.Log($"{GetName()} rolling {dicesToRoll.Count()} dices... => result = {res}");
            return res;
        }

        #region DEBUG

        public void DebugPrintStack(List<Card> stack)
        {
            var msg = $"{stack.Count} elements in stack : ";
            foreach (var c in stack)
            {
                msg += c.GetCardSO().name + ", ";
            }
            msg = msg.Substring(0, msg.Length - 2);
            Debug.Log(msg);
        }

        /// <summary>
        /// Affiche des infos de débug au clic droit
        /// </summary>
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                Debug.Log($"Card Chained : {PreviousCardInStack?.GetComponent<Card>().GetName()} < {GetName()} < {NextCardInStack?.GetComponent<Card>().GetName()}");
            }
        }
        #endregion
    }
}
