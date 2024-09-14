using UnityEngine;

namespace InitialSolution.EventableObject
{
    public interface IBeginSelectHandler
    {
        void OnBeginSelect(Selecter sponsor);
    }

    public interface IEndSelectHandler
    {
        void OnEndSelect(Selecter sponsor);
    }

    [AddComponentMenu("Initial Solution/Eventable Object/Selecter")]
    public class Selecter : MonoBehaviour
    {
        public virtual bool IsSelecting { get; private set; }

        private EventableBehaviour selectingTarget;
        public virtual EventableBehaviour SelectingTarget
        {
            get => selectingTarget == null ? null : selectingTarget;
            private set => selectingTarget = value;
        }


        public virtual void SelectObject(EventableBehaviour target)
        {
            if (IsSelecting) return;
            IsSelecting = true;

            SelectingTarget = target;

            if (SelectingTarget is IBeginSelectHandler handler) handler.OnBeginSelect(this);
        }

        public virtual void DeselectObject()
        {
            if (!IsSelecting) return;
            IsSelecting = false;

            if (SelectingTarget is IEndSelectHandler handler) handler.OnEndSelect(this);

            SelectingTarget = null;
        }

        public virtual bool TrySelectObject(EventableBehaviour target)
        {
            if (IsSelecting) if (SelectingTarget == target) return false;

            DeselectObject();
            SelectObject(target);

            return true;
        }
    }
}
