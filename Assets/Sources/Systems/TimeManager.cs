using UnityEngine;

namespace Assets.Sources.Systems
{
    public class TimeManager : MonoBehaviour
    {
        public GameObject timerPrefab;

        internal void InstanciateTimerSliderOnCard(float duration, Vector3 timerPosition, RectTransform parentTransform, string receivedCardGuid)
        {
            var targetCardRectTransform = (RectTransform)parentTransform.Find("Card Body").transform;

            Vector3 newPos = targetCardRectTransform.position;
            //newPos.y = targetCardRectTransform.rect.height / 2 + 50;
            newPos.y = 50;
            newPos.x = 180;

            GameObject spawedTimer = Instantiate(timerPrefab, timerPosition, Quaternion.identity);
            spawedTimer.name = $"TimerSlider_{receivedCardGuid}";
            spawedTimer.GetComponent<Timer>().maxTime = duration;
            spawedTimer.GetComponent<Timer>().timerSlider.value = duration;

            spawedTimer.transform.SetParent(parentTransform.Find("Card Body"));
            spawedTimer.transform.localScale = new Vector3(1, 1, 1);
            spawedTimer.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            spawedTimer.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1);
            spawedTimer.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1);
            spawedTimer.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
            //spawedTimer.transform.position = newPos;
            //TODO trrouver une solution avec les anchors

            //spawedTimer.transform.position = 20;
            Debug.Log("Timer is at " + spawedTimer.GetComponent<RectTransform>().position.ToString());
            //spawedTimer.transform.localScale = GlobalVariables.CardElementsScale;
        }
    }
}
