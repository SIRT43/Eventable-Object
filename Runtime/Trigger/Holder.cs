using UnityEngine;

namespace FTGAMEStudio.InitialSolution.EventableObject
{
    public interface IBeginHoldHandler
    {
        void OnBeginHold(Holder sponsor);
    }

    public interface IEndHoldHandler
    {
        void OnEndHold(Holder sponsor);
    }

    public interface IHoldingHandler
    {
        void Holding(Holder sponsor);
    }

    [AddComponentMenu("Initial Solution/Eventable Object/Holder")]
    public class Holder : Selecter
    {
#if UNITY_EDITOR
        [Header("Editor")]
        public EventableBehaviour inputHoldTarget;


        protected override void OnValidate()
        {
            base.OnValidate();

            if (inputHoldTarget == null && IsHolding) ReleaseObject();
            else if (inputHoldTarget != null && !IsHolding) TryHoldObject(inputHoldTarget);
        }
#endif


        public bool IsHolding { get; private set; }

        private EventableBehaviour holdingTarget;
        public EventableBehaviour HoldingTarget
        {
            get => holdingTarget == null ? null : holdingTarget;
            set => holdingTarget = value;
        }

        private IHoldingHandler holdingHandler;
        public IHoldingHandler HoldingHandler
        {
            get
            {
                if (HoldingTarget == null)
                {
                    ReleaseObject();
                    return null;
                }
                return holdingHandler;
            }
            private set => holdingHandler = value;
        }


        protected virtual void UpdateHold() => HoldingHandler?.Holding(this);


        public virtual void HoldObject()
        {
            if (IsHolding || !IsSelecting) return;

            HoldingTarget = SelectingTarget;
            HoldingHandler = HoldingTarget as IHoldingHandler;

            IsHolding = true;

            DeselectObject();

            if (HoldingTarget is IBeginHoldHandler handler) handler.OnBeginHold(this);
        }

        public virtual void ReleaseObject()
        {
            if (!IsHolding) return;

            if (HoldingTarget is IEndHoldHandler handler) handler.OnEndHold(this);

            HoldingTarget = null;
            HoldingHandler = null;

            IsHolding = false;
        }

        public virtual void TryHoldObject(EventableBehaviour target)
        {
            TrySelectObject(target);
            HoldObject();
        }

        public virtual void ToggleHoldObject()
        {
            if (IsSelecting) HoldObject();
            else if (IsHolding) ReleaseObject();
        }


        public override void SelectObject(EventableBehaviour target)
        {
            if (IsHolding) return;
            base.SelectObject(target);
        }
    }
}
