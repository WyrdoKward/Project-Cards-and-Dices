using System.Linq;
using UnityEngine;

namespace Assets.Sources.Behaviour
{
    class Draggable3D : MonoBehaviour
    {
        Plane movePlane;
        float fixedDistance = 2f;
        float hitDist, t;
        Ray camRay;
        Vector3 startPos, point, corPoint;

        //private bool isDragged = false;

        public float snapRange = 0.1f;
        public float snapYOffset = 0.02f;
        public float snapZOffset = 10;

        void OnMouseDown()
        {
            startPos = transform.position; // save position in case draged to invalid place
            movePlane = new Plane(-Camera.main.transform.forward, transform.position); // find a parallel plane to the camera based on obj start pos;
        }

        void OnMouseDrag()
        {
            camRay = Camera.main.ScreenPointToRay(Input.mousePosition); // shoot a ray at the obj from mouse screen point

            if (movePlane.Raycast(camRay, out hitDist))
            {

                // find the collision on movePlane
                point = camRay.GetPoint(hitDist); // define the point on movePlane
                t = -(fixedDistance - camRay.origin.z) / (camRay.origin.z - point.z); // the x,y or z plane you want to be fixed to
                corPoint.x = camRay.origin.x + (point.x - camRay.origin.x) * t; // calculate the new point t futher along the ray
                corPoint.y = camRay.origin.y + (point.y - camRay.origin.y) * t;

                //TODO voir pour surélever (z--) la cazrte quand on la soulève
                corPoint.z = camRay.origin.z + (point.z - camRay.origin.z) * t;

                if (InvalidPosition())
                    transform.position = startPos;
                else
                    transform.position = corPoint;
            }
        }

        private void OnMouseUp()
        {
            //Récupérer la liste des cartes pour snaper dessus
            var cards = GameObject.FindGameObjectsWithTag("Card").ToList();
            cards.Remove(this.gameObject); //Il faudra aussi ajouter une propriété dans BaseCardSO qui determine si on peut combineer lla carte courante ou pas.

            //Snap tuto : https://www.youtube.com/watch?v=axW46wCJxZ0
            //https://www.youtube.com/watch?v=BGr-7GZJNXg&t=140s 

            float closestDistanceFromACard = -1;
            Transform closestCard = null;

            foreach (GameObject card in cards)
            {
                var currentDistance = Vector2.Distance(this.transform.localPosition, card.transform.localPosition);
                if (closestCard == null || currentDistance < closestDistanceFromACard)
                {
                    closestCard = card.transform;
                    closestDistanceFromACard = currentDistance;
                }
                Debug.Log($"{gameObject.name} is at Z localposition {gameObject.transform.localPosition.z}");
                Debug.Log($"{gameObject.name} is at Z worldposition {gameObject.transform.position.z}");
            }

            //Si on a une cible pour le snap
            if (closestCard != null && closestDistanceFromACard <= snapRange)
            {
                Vector3 tmpLocalPosition = closestCard.transform.localPosition;
                tmpLocalPosition.y -= snapYOffset;
                tmpLocalPosition.z = closestCard.transform.localPosition.z - snapZOffset;
                this.transform.localPosition = tmpLocalPosition;

                //On spawn une ressourfce
                /*GameObject GameManager = GameObject.Find("_GameManager");
                CardSpawner cardSpawenr = GameManager.GetComponent<CardSpawner>();
                cardSpawenr.SpawnCard();*/
            }


        }

        /// <summary>
        /// Return true si on se retrouve hors du GameBoard
        /// </summary>
        /// <returns></returns>
        private bool InvalidPosition()
        {
            return false;
        }


    }

}
