using UnityEngine;

namespace InitialSolution.EventableObject
{
    /// <summary>
    /// ���¼����󴥷�����
    /// </summary>
    public abstract class EventableTrigger : Holder
    {
        [Header("Ray")]
        public LayerMask targetLayer = ~4;
        public float maxDistance = 5;


        /// <summary>
        /// ���ڼ������ߣ��Ƽ�ʹ��ֻ�������ʱ������
        /// </summary>
        public abstract Ray Ray { get; }


        public virtual void UpdateTrigger()
        {
            if (IsHolding) UpdateHold();
            else UpdateSelect();
        }

        public virtual void UpdateSelect()
        {
            if (!Physics.Raycast(Ray, out RaycastHit hitInfo, maxDistance, targetLayer))
            {
                DeselectObject();
                return;
            }

            if (IsSelecting && SelectingTarget.gameObject != hitInfo.collider.gameObject)
                DeselectObject();

            if (hitInfo.collider.TryGetComponent(out EventableBehaviour component))
                SelectObject(component);
        }
    }
}
