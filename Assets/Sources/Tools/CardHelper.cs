using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sources.Tools
{
    public static class CardHelper
    {
        // Apply a set of actions to a list of Cards
        //
        // Ex of use :
        // ApplyToCards(
        //  cards,
        //  (card, args) =>
        //      {
        //          card.sortingOrder = (int)args[0];
        //          args[0] = (int)args[0] + 1;
        //      },
        //      new object[] { 0 });

        public static List<GameObject> ApplyToGameObjects(List<GameObject> cards, Action<GameObject, object[]> action, object[] args)
        {
            var res = new List<GameObject>(cards);
            foreach (var card in res)
            {
                action(card, args);
            }
            return res;
        }
    }
}
