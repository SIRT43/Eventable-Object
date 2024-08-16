using UnityEngine;

namespace FTGAMEStudio.InitialSolution.EventableObject
{
    public interface IBeginSelectHandler
    {
        public void OnBeginSelect(Selecter sponsor);
    }

    public interface IEndSelectHandler
    {
        public void OnEndSelect(Selecter sponsor);
    }

    [AddComponentMenu("Initial Solution/Eventable Object/Selecter")]
    public class Selecter : MonoBehaviour
    {
#if UNITY_EDITOR
        [Header("Editor")]
        public EventableBehaviour inputSelectTarget;


        protected virtual void OnValidate()
        {
            if (inputSelectTarget == null && IsSelecting) DeselectObject();
            else if (inputSelectTarget != null && !IsSelecting) TrySelectObject(inputSelectTarget);
        }
#endif


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

            SelectingTarget = target;
            IsSelecting = true;

            if (SelectingTarget is IBeginSelectHandler handler) handler.OnBeginSelect(this);
        }

        public virtual void DeselectObject()
        {
            if (!IsSelecting) return;

            if (SelectingTarget is IEndSelectHandler handler) handler.OnEndSelect(this);

            SelectingTarget = null;
            IsSelecting = false;
        }

        public virtual void TrySelectObject(EventableBehaviour target)
        {
            if (IsSelecting) if (SelectingTarget == target) return;

            DeselectObject();
            SelectObject(target);
        }
    }
}
