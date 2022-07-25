using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Cards
{
    public abstract class BaseCardSO : ScriptableObject
    {
        public new string name;
        public string description;
        public Sprite artwork;

        //public Dice[] dices

        public void Print(GameObject gameObject)
        {
            Debug.Log($"{name} is at Z localposition {gameObject.transform.localPosition.z}");
            Debug.Log($"{name} is at Z worldposition {gameObject.transform.position.z}");
        }

        public abstract void InitializedCardWithScriptableObject(GameObject cardBodyGO);
    }
}
