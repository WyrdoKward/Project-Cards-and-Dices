using UnityEngine;

namespace Assets.Sources.ScriptableObjects.Actions
{
    public abstract class ThreatOutcomeSO : ScriptableObject
    {
        public abstract void Success();
        public abstract void Failure();
    }
}
