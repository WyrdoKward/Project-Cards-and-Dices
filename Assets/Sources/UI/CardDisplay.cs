using Assets.Sources.Entities;
using Assets.Sources.ScriptableObjects.Cards;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Sources.UI
{
    public class CardDisplay : MonoBehaviour
    {
        //public BaseCardSO cardSO;

        public Text NameText;
        public Text DescriptionText;
        public Image ArtworkImage;

        private Card _thisCard { get => transform.parent.gameObject.GetComponentInChildren<Card>(); }
        private BaseCardSO _cardSO;
        public BaseCardSO CardSO
        {
            get
            {
                if (_thisCard != null)
                    return _thisCard.GetCardSO();
                else
                    return _cardSO;
            }
            set => _cardSO = value;
        }


        void Start()
        {
            //var _cardSO = _thisCard.GetCardSO();
            //Debug.Log("Start");
            if (CardSO != null)
                LoadCardData();
            else
                Debug.Log("cardSO is null");
        }

        public void LoadCardData()
        {
            //Debug.Log($"LoadCardData for {data.name}");
            NameText.text = CardSO.name;
            DescriptionText.text = CardSO.description;
            ArtworkImage.sprite = CardSO.artwork;
        }
    }
}
