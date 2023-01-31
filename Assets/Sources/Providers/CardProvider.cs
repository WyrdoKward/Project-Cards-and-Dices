using Assets.Sources.Entities;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Sources.Providers
{
    public class CardProvider : MonoBehaviour
    {
        public GameObject CardContainerGO;
        internal GameObject GetRandomCard<T>() where T : Card
        {
            var typedCards = new List<T>();
            foreach (var cardGO in AllCardGameObjectsInGame())
            {
                var card = cardGO.GetComponentInChildren<Card>();
                if (card is T tCard)
                    typedCards.Add(tCard);
            }

            if (typedCards.Count == 0)
                return null;

            var rnd = Random.Range(0, typedCards.Count);
            var res = typedCards[rnd];

            return res.transform.parent.gameObject;
        }

        /// <summary>
        /// Returs a list of all the card names currently in game
        /// </summary>
        public List<string> ComputeInGameCardsList()
        {
            var res = new List<string>();
            var childCount = CardContainerGO.transform.GetComponentsInChildren<Card>().Length;
            for (var i = 0; i < childCount; i++)
            {
                var card = CardContainerGO.transform.GetComponentsInChildren<Card>()[i];
                if (card != null)
                    res.Add(card.GetName());
            }

            return res;
        }

        public List<GameObject> AllCardGameObjectsInGame()
        {
            var res = new List<GameObject>();
            foreach (Transform child in CardContainerGO.transform)
            {
                res.Add(child.gameObject);
            }

            return res;
        }
    }
}
