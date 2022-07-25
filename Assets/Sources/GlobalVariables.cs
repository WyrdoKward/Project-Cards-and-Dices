using UnityEngine;

namespace Assets.Sources
{
    public static class GlobalVariables
    {
        public static readonly int DefaultCardSortingLayer = 0;
        public static readonly int OnDragCardSortingLayer = 99; //Allows to put the current dragged card above everything else

        private static float cardElementsScaleInt = 0.42552f;
        public static Vector3 CardElementsScale = new Vector3(cardElementsScaleInt, cardElementsScaleInt, cardElementsScaleInt);
    }
}
