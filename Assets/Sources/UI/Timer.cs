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

        private float _lerpStartTime;
        private float _currentLerpTime = 0f;
        private bool _isLerping = false;

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
            {
                var lerp = RunSliderLerp(currentSliderTime);
                sliderFillImage.color = Color.Lerp(currentColor, targetColor, lerp);
            }
        }

        /// <summary>
        /// Handles the slider color lerping and interpolate from _lerpStartTime for GlobalVariables.DefaultLerpingValue duration.
        /// Call this BEFORE the actual xxx.Lerp function to allow it to reset _currentLerpTime
        /// </summary>
        private float RunSliderLerp(float currentSliderTime)
        {
            if (!_isLerping && _currentLerpTime == 0) // Start of the lerp
            {
                _isLerping = true;
                _lerpStartTime = currentSliderTime;
            }
            else if (_currentLerpTime > GlobalVariables.DefaultLerpingValue) //End of the lerp
            {
                _isLerping = false;
                _currentLerpTime = 0f;
            }

            //between 0 and 1
            _currentLerpTime = _lerpStartTime - currentSliderTime;
            return _currentLerpTime / GlobalVariables.DefaultLerpingValue;
        }

        private void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}
