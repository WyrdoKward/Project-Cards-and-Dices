using Assets.Sources.Entities;
using Assets.Sources.Providers;
using Assets.Sources.ScriptableObjects.Cards;
using Assets.Sources.Tools;
using Assets.Sources.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Sources.Systems
{
    public class CardSpawner : MonoBehaviour
    {
        public GameObject cardPrefab;
        //public List<BaseCardSO> AllCardsData;
        public GameObject CardContainerGO;
        public BaseCardSO defaultLoot;
        private CardProvider cardProvider;

        //Prefabs with SO and spawn
        //https://www.youtube.com/watch?v=i9oVPmgjw-U

        //https://answers.unity.com/questions/1821765/how-to-access-and-change-scriptable-objects-on-ins.html

        private void Start()
        {
            cardProvider = GameObject.Find("_GameManager").GetComponent<CardProvider>();
            // generate all cards
            //GenerateCards((card) => true);

            // generate cards with the word attack in the description
            //GenerateCards(card => card.name.ToLower().Contains("wood"));
        }

        /// <summary>
        /// Utiliser les poids dans le random plutot que de multiplier les objets ?
        /// </summary>
        /// <param name="weightedDict"></param>
        public void GenerateRandomCardFromWeightedList(DictionaryForInspector<BaseCardSO, int> weightedDict)
        {
            var res = new List<BaseCardSO>();
            for (var i = 0; i < weightedDict.Keys.Count; i++)
            {
                for (var j = 0; j < weightedDict.Values[i]; j++)
                {
                    res.Add(weightedDict.Keys[i]);
                }
            }
            GenerateRandomCardFromList(res);
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
            var inGameCardNames = cardProvider.ComputeInGameCardsList();

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
            //Debug.Log($"SpawnCard {cardData.name}");
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
            spawedCardGO.GetComponentInChildren<Graphic>().color = cardData.BgColor;
        }

    }
}
