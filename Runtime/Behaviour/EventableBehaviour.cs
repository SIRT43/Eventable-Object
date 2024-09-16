using UnityEngine;

namespace InitialSolution.EventableObject
{
    /// <summary>
    /// <see cref="EventableBehaviour"/> �����ڱ�д���¼�����Ļ��ࡣ
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public abstract class EventableBehaviour : MonoBehaviour 
    {
        protected Collider Collider { get; private set; }

        protected virtual void Awake() => Collider = GetComponent<Collider>();
    }
}
