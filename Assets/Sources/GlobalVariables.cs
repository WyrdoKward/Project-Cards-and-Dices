using UnityEngine;

namespace Assets.Sources
{
    public static class GlobalVariables
    {
        public static readonly int DefaultCardSortingLayer = 0;
        public static readonly int OnDragCardSortingLayer = 99; //Allows to put the current dragged card above everything else

        private static float cardElementsScaleInt = 0.42552f;
        public static Vector3 CardElementsScale = new Vector3(cardElementsScaleInt, cardElementsScaleInt, cardElementsScaleInt);

        #region CardTypes
        public static readonly Color FOLLOWER_DefaultSliderColor = Color.cyan;
        public static readonly Color LOCATION_DefaultSliderColor = Color.green;
        public static readonly Color THREAT_DefaultSliderColor = Color.red;
        public static readonly Color RESOURCE_DefaultSliderColor = Color.white;
        public static readonly Color PNJ_DefaultSliderColor = Color.magenta;
        #endregion

        #region animationUI
        public static readonly float DefaultLerpingValue = 5f;
        #endregion
    }
}
