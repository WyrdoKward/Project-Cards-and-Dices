using System;
using UnityEngine;

namespace Assets.Sources.Systems
{
    public class TimeManager : MonoBehaviour
    {
        public GameObject timerPrefab;

        internal void InstanciateTimerSliderOnCard(Action action, float duration, RectTransform targetCard, string receivedCardGuid)
        {
            //Calcul de la position du slider
            var timerPosition = targetCard.position;
            timerPosition.y += 20;

            var targetCardRectTransform = (RectTransform)targetCard.Find("Card Body").transform;

            var newPos = targetCardRectTransform.position;
            //newPos.y = targetCardRectTransform.rect.height / 2 + 50;
            newPos.y = 50;
            newPos.x = 180;

            var spawedTimer = Instantiate(timerPrefab, timerPosition, Quaternion.identity);
            spawedTimer.name = $"TimerSlider_{receivedCardGuid}";
            spawedTimer.GetComponent<Timer>().maxTime = duration;
            spawedTimer.GetComponent<Timer>().timerSlider.value = duration;

            spawedTimer.transform.SetParent(targetCard.Find("Card Body"));
            spawedTimer.transform.localScale = new Vector3(1, 1, 1);
            spawedTimer.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            spawedTimer.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1);
            spawedTimer.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1);
            spawedTimer.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
            //spawedTimer.transform.position = newPos;
            //TODO trrouver une solution avec les anchors

            //spawedTimer.transform.position = 20;
            //Debug.Log("Timer is at " + spawedTimer.GetComponent<RectTransform>().position.ToString());
            //spawedTimer.transform.localScale = GlobalVariables.CardElementsScale;

            //On attache l'action à un timer
            //https://www.youtube.com/watch?v=1hsppNzx7_0
            FunctionTimer.Create(action, duration, receivedCardGuid);
        }
    }
}
