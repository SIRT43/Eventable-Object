using UnityEngine;

namespace InitialSolution.EventableObject
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

                if (holdingHandler == null) holdingHandler = HoldingTarget as IHoldingHandler;
                return holdingHandler;
            }
        }


        protected virtual void UpdateHold()
        {
            if (!IsHolding) return;
            HoldingHandler?.Holding(this);
        }


        public virtual void HoldObject()
        {
            if (IsHolding || !IsSelecting) return;
            IsHolding = true;

            HoldingTarget = SelectingTarget;

            DeselectObject();

            if (HoldingTarget is IBeginHoldHandler handler) handler.OnBeginHold(this);
        }

        public virtual void ReleaseObject()
        {
            if (!IsHolding) return;
            IsHolding = false;

            if (HoldingTarget is IEndHoldHandler handler) handler.OnEndHold(this);

            HoldingTarget = null;
        }

        public virtual bool TryHoldObject(EventableBehaviour target)
        {
            if (!TrySelectObject(target)) return false;
            HoldObject();

            return true;
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
