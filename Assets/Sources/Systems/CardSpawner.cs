using Assets.Sources.Display;
using Assets.Sources.Entities;
using Assets.Sources.Misc;
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
        public GameObject GameBoard;

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

        public void GenerateRandomCardFromList(List<BaseCardSO> cards)
        {
            var rnd = Random.Range(0, cards.Count);
            BaseCardSO chosenCard = cards[rnd];
            SpawnCard(chosenCard);
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
            CardDisplay cd = cardPrefab.GetComponentInChildren<CardDisplay>();
            cd.LoadCardData(cardData);
            cd.cardSO = cardData;


            GameObject spawedCardGO = Instantiate(cardPrefab, new Vector3(10f, 10f, -10f), Quaternion.identity);

            //TODO A rendre générique...
            GameObject cardBodyGO = spawedCardGO.GetComponentInChildren<RectTransform>().Find("Card Body").gameObject;



            Destroy(spawedCardGO.GetComponentInChildren<Card>());
            switch (cd.cardSO.cardType)
            {
                case Misc.ECardType.Ressource:
                    cardBodyGO.AddComponent<Resource>();
                    cardBodyGO.GetComponent<Resource>().cardSO = (ResourceCardSO)cardData;
                    break;
                case ECardType.Follower:
                    cardBodyGO.AddComponent<Follower>();
                    cardBodyGO.GetComponent<Follower>().cardSO = (FollowerCardSO)cardData;
                    break;
                case Misc.ECardType.Location:
                    cardBodyGO.AddComponent<Location>();
                    cardBodyGO.GetComponent<Location>().cardSO = (LocationCardSO)cardData;
                    break;
                default:
                    break;
            }


            spawedCardGO.name = cardData.name;
            spawedCardGO.transform.SetParent(GameBoard.transform, false);
            spawedCardGO.GetComponentInChildren<RectTransform>().localScale = GlobalVariables.CardElementsScale;
            spawedCardGO.GetComponent<Canvas>().sortingOrder = 10; // temporaire, le temps de coder un truc pour capter les cartes autour et juste se poser au dessus
        }
    }
}
