using UnityEngine;

namespace InitialSolution.EventableObject
{
    /// <summary>
    /// <see cref="EventableBehaviour"/> 是用于编写可事件对象的基类。
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public abstract class EventableBehaviour : MonoBehaviour 
    {
        protected Collider Collider { get; private set; }

        protected virtual void Awake() => Collider = GetComponent<Collider>();
    }
}
