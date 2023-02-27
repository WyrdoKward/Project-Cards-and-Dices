using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Sources.ScriptableObjects.Dices
{
    public class BaseDiceSO : ScriptableObject
    {
        [SerializeField]
        private int[] Sides = new int[6];

        public int Roll()
        {
            var randomIndex = Random.Range(0, Sides.Length);
            return Sides[randomIndex];
        }
    }
}
