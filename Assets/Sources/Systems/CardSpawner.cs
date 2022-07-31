using Assets.Sources.Display;
using Assets.Sources.Entities;
using Assets.Sources.ScriptableObjects.Cards;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Sources.Systems
{
    public class CardSpawner : MonoBehaviour
    {
        public GameObject cardPrefab;
        //public List<BaseCardSO> AllCardsData;
        public GameObject CardContainerGO;
        public BaseCardSO defaultLoot;

        //Prefabs with SO and spawn
        //https://www.youtube.com/watch?v=i9oVPmgjw-U

        //https://answers.unity.com/questions/1821765/how-to-access-and-change-scriptable-objects-on-ins.html

        private void Start()
        {
            // generate all cards
            //GenerateCards((card) => true);

            // generate cards with the word attack in the description
            //GenerateCards(card => card.name.ToLower().Contains("wood"));
        }

        public void GenerateRandomCardFromList(IEnumerable<BaseCardSO> cards)
        {
            var filteredLoot = FilterExistingUniqueCards(cards);

            if (filteredLoot.Count == 0)
                SpawnCard(defaultLoot);
            else
            {
                var rnd = Random.Range(0, filteredLoot.Count);
                var chosenCard = filteredLoot[rnd];
                SpawnCard(chosenCard);
            }
        }

        private List<BaseCardSO> FilterExistingUniqueCards(IEnumerable<BaseCardSO> cards)
        {
            var res = new List<BaseCardSO>(cards);

            //var inGameCardNames = CardContainerGO.transform.GetComponentsInChildren<Card>().Select(c => c.GetName());
            var inGameCardNames = new List<string>();
            var childCount = CardContainerGO.transform.GetComponentsInChildren<Card>().Length;
            for (var i = 0; i < childCount; i++)
            {
                var card = CardContainerGO.transform.GetComponentsInChildren<Card>()[i];
                if (card != null)
                    inGameCardNames.Add(card.GetName());
            }

            foreach (var cardSO in cards)
            {
                if (!cardSO.isUnique)
                    continue;

                if (inGameCardNames.Contains(cardSO.name))
                    res.Remove(cardSO);
            }
            return res;
        }

        //private void GenerateCards(Func<BaseCardSO, bool> doWeWantToInstantiateCard)
        //{
        //    foreach (BaseCardSO card in AllCardsData)
        //    {
        //        if (doWeWantToInstantiateCard(card))
        //        {
        //            SpawnCard(card);
        //        }
        //    }
        //}


        public void SpawnCard(BaseCardSO cardData)
        {
            Debug.Log($"SpawnCard {cardData.name}");
            var cd = cardPrefab.GetComponentInChildren<CardDisplay>();
            cd.LoadCardData(cardData);
            cd.cardSO = cardData;


            var spawedCardGO = Instantiate(cardPrefab, new Vector3(10f, 10f, -10f), Quaternion.identity);

            var cardBodyGO = spawedCardGO.GetComponentInChildren<RectTransform>().Find("Card Body").gameObject;
            Destroy(spawedCardGO.GetComponentInChildren<Card>());
            cardData.InitializedCardWithScriptableObject(cardBodyGO);


            spawedCardGO.name = cardData.name;
            spawedCardGO.transform.SetParent(CardContainerGO.transform, false);
            spawedCardGO.GetComponentInChildren<RectTransform>().localScale = GlobalVariables.CardElementsScale;
            spawedCardGO.GetComponent<Canvas>().sortingOrder = 10; // temporaire, le temps de coder un truc pour capter les cartes autour et juste se poser au dessus
        }
    }
}
