using Assets.Sources.Entities;
using Assets.Sources.Systems.Timers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sources.Systems
{
    public class TimeManager : MonoBehaviour
    {
        public GameObject timerPrefab;
        public static List<TimerGroup> TimerGroups = new List<TimerGroup>();

        internal string CreateTimerGroup(Action action, float duration, Card targetCard, Card receivedCard,/*RectTransform targetCard, string receivedCardGuid,*/ bool hasToStopWhenCardIsMoving)
        {
            var cards = new List<Card>() { targetCard, receivedCard };
            var timerGroup = new TimerGroup(cards, action, duration, hasToStopWhenCardIsMoving);

            TimerGroups.Add(timerGroup);

            DisplayTimerSlider(duration, targetCard.GetTransform(), timerGroup.Guid);

            return timerGroup.Guid;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timerGroupGuid"></param>
        /// <param name="fromMovement">True si StopTimer apellé depuis un mouvement</param>
        internal static void StopTimer(string timerGroupGuid, bool fromMovement = false)
        {
            //Stoper le FunctionTimer
            var hasBeenStopped = false;
            for (var i = 0; i < TimerGroups.Count; i++)
            {
                if (TimerGroups[i].Guid == timerGroupGuid)
                {
                    if (fromMovement && !TimerGroups[i].FunctionTimer.HasToStopWhenCardIsMoving) //Pour ne pas stopper un timer qui doit persister après un mouvement
                        continue;

                    TimerGroups[i].Stop();
                    TimerGroups.RemoveAt(i);
                    i--; // On décrémente pour ne pas en skipper un, puisque qu'on en a viré un de la liste
                    Debug.Log("Stopping " + timerGroupGuid + " OK");
                    hasBeenStopped = true;
                }
            }

            if (!hasBeenStopped)
                Debug.Log("Stopping " + timerGroupGuid + " FAILED - no such timer exists");
        }

        /// <summary>
        /// A apeller si on veut vérifier & stopper un timer suite à un mouvement
        /// </summary>
        internal static void StopTimerFromMovement(string receivedCardGuid)
        {
            StopTimer(receivedCardGuid, true);
        }

        private void DisplayTimerSlider(float duration, RectTransform targetCard, string timerGroupGuid)
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
            spawedTimer.name = timerGroupGuid;
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
        }

    }
}
