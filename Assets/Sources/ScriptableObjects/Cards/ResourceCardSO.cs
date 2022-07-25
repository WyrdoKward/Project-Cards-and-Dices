using Assets.Sources.Misc;
using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Cards
{
    [CreateAssetMenu(fileName = "New Resource", menuName = "Card/Resource")]
    internal class ResourceCardSO : BaseCardSO
    {
        new ECardType cardType = ECardType.Ressource;

        public ResourceCardSO()
        {
            //cardType = ECardType.Ressource;//Initile pour le moment car spécifié dans l'inspector unity cu cardSO mais c'est redondant...
        }
    }
}
