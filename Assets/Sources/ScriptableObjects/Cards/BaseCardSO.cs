﻿using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Cards
{
    public abstract class BaseCardSO : ScriptableObject
    {
        public new string name;
        public string description;
        public Sprite artwork;
        internal GameObject thisCardGameObject;
        public abstract Color BgColor { get; }

        public bool isUnique;

        //public Dice[] dices

        public void Print(GameObject gameObject)
        {
            //Debug.Log($"{name} is at Z localposition {gameObject.transform.localPosition.z}");
            //Debug.Log($"{name} is at Z worldposition {gameObject.transform.position.z}");
        }

        public virtual void InitializedCardWithScriptableObject(GameObject cardBodyGO)
        {
            thisCardGameObject = cardBodyGO;
        }
    }
}
