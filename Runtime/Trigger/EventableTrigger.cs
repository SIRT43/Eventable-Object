using UnityEngine;

namespace FTGAMEStudio.InitialSolution.EventableObject
{
    /// <summary>
    /// ���¼����󴥷�����
    /// </summary>
    [AddComponentMenu("Initial Solution/Eventable Object/Eventable Trigger")]
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


#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();

            if ((targetLayer & 4) == 4)
            {
                targetLayer &= ~4;
                Debug.LogWarning("Layer 3 cannot be included because it is the default exclusion layer.");
            }
        }
#endif
    }
}
