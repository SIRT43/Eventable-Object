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
        public EventableBehaviour HoldingTarget { get; private set; }

        private IHoldingHandler HoldingHandler { get; set; }



        protected virtual bool UpdateHold()
        {
            if (!IsHolding) return false;

            if (HoldingTarget == null)
            {
                ReleaseObject();
                return false;
            }

            if (HoldingHandler == null) return false;

            HoldingHandler.Holding(this);
            return true;
        }


        public virtual bool HoldObject()
        {
            if (IsHolding || !IsSelecting || SelectingTarget == null) return false;
            IsHolding = true;

            HoldingTarget = SelectingTarget;
            HoldingHandler = SelectingTarget as IHoldingHandler;

            if (SelectingTarget is IBeginHoldHandler handler) handler.OnBeginHold(this);

            DeselectObject();

            return true;
        }

        public virtual void ReleaseObject()
        {
            if (!IsHolding) return;
            IsHolding = false;

            if (HoldingTarget != null && HoldingTarget is IEndHoldHandler handler) handler.OnEndHold(this);

            HoldingTarget = null;
        }

        public virtual bool TryHoldObject(EventableBehaviour target)
        {
            if (!TrySelectObject(target)) return false;

            return HoldObject();
        }

        public virtual void ToggleHoldObject()
        {
            if (IsSelecting) HoldObject();
            else if (IsHolding) ReleaseObject();
        }


        public override bool SelectObject(EventableBehaviour target)
        {
            if (IsHolding) return false;

            return base.SelectObject(target);
        }



#if UNITY_EDITOR
        protected override void OnValidate() => TryHoldObject(inputEventable);
#endif
    }
}
