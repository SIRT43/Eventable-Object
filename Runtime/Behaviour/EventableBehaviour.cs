using UnityEngine;

namespace InitialSolution.EventableObject
{
    /// <summary>
    /// <see cref="EventableBehaviour"/> �����ڱ�д���¼�����Ļ��ࡣ
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public abstract class EventableBehaviour : MonoBehaviour
    {
        protected new Collider collider;

        protected virtual void Awake() => collider = GetComponent<Collider>();
    }
}
