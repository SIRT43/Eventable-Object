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
        public virtual EventableBehaviour SelectingTarget { get; private set; }


        public virtual bool SelectObject(EventableBehaviour target)
        {
            if (IsSelecting || target == null) return false;
            IsSelecting = true;

            SelectingTarget = target;

            if (SelectingTarget is IBeginSelectHandler handler) handler.OnBeginSelect(this);

            return true;
        }

        public virtual void DeselectObject()
        {
            if (!IsSelecting) return;
            IsSelecting = false;

            if (SelectingTarget != null && SelectingTarget is IEndSelectHandler handler) handler.OnEndSelect(this);

            SelectingTarget = null;
        }

        public virtual bool TrySelectObject(EventableBehaviour target)
        {
            if (IsSelecting && SelectingTarget == target) return false;

            DeselectObject();
            return SelectObject(target);
        }



#if UNITY_EDITOR
        [Header("Debug")]
        public EventableBehaviour inputEventable;

        protected virtual void OnValidate() => TrySelectObject(inputEventable);
#endif
    }
}
