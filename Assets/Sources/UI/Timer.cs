using Assets.Sources.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Sources.UI
{
    public class Timer : MonoBehaviour
    {
        public Slider timerSlider;
        public Text timerText;
        public float maxTime;

        private void Start()
        {
            timerSlider.maxValue = maxTime;
            timerSlider.value = maxTime;
        }

        private void Update()
        {
            var currentSliderTime = timerSlider.value - Time.deltaTime;

            UpdateSliderColor(currentSliderTime);

            var minutes = Mathf.FloorToInt(currentSliderTime / 60);
            var seconds = Mathf.FloorToInt(currentSliderTime % 60);

            var textTime = string.Format("{0:0}:{1:00}", minutes, seconds + 1);

            if (currentSliderTime > 0)
            {
                timerText.text = textTime;
                timerSlider.value = currentSliderTime;
            }
            else
                DestroySelf();
        }

        private void UpdateSliderColor(float currentSliderTime)
        {
            var thisCard = (Card)transform.parent.gameObject.GetComponent("Card");
            var sliderFillImage = timerSlider.transform.Find("Fill Area/Fill").GetComponent<Image>();
            var currentColor = sliderFillImage.color;
            var targetColor = thisCard.ComputeSpecificSliderColor();

            //Update the color
            if (targetColor != currentColor)
                sliderFillImage.color = Color.Lerp(currentColor, targetColor, currentSliderTime);
        }

        private void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}
