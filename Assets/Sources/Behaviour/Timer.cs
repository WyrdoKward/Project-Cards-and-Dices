using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Slider timerSlider;
    public Text timerText;
    public float maxTime;

    private bool isRunning;

    private void Start()
    {
        isRunning = true;
        timerSlider.maxValue = maxTime;
        timerSlider.value = maxTime;
    }

    private void Update()
    {
        var currentSliderTime = timerSlider.value - Time.deltaTime;

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

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
