using Assets.Sources.ScriptableObjects.Cards;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Sources.Display
{
    public class CardDisplay : MonoBehaviour
    {
        public BaseCardSO cardSO;

        public Text NameText;
        public Text DescriptionText;
        public Image ArtworkImage;

        void Start()
        {
            Debug.Log("Start");
            if (cardSO != null)
                LoadCardData(cardSO);
            else
                Debug.Log("cardSO is null");
        }

        public void LoadCardData(BaseCardSO data)
        {
            Debug.Log($"LoadCardData for {data.name}");
            NameText.text = data.name;
            DescriptionText.text = data.description;
            ArtworkImage.sprite = data.artwork;
        }
    }
}
