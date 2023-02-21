using Assets.Sources.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Pnjs.Concrete
{
    [CreateAssetMenu(fileName = "GerardActionsSO", menuName = "Card/PNJ/Actions/GerardActionsSO")]

    internal class GerardActionsSO : PNJActionsSO
    {
        internal override void Talk(Follower follower)
        {
            Debug.Log($"{follower.GetName()} : bla ?");
            Debug.Log($"{CardSO.name} : bla bla bla !");
        }

        internal override void BuyService(Follower follower, List<Resource> resources)
        {
            Debug.Log($"{CardSO.name} : You offer to pay me {resources.Count} resources !");
        }
    }
}
