using FTGAMEStudio.InitialSolution.Inputs;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FTGAMEStudio.InitialSolution.EventableObject
{
    [AddComponentMenu("Initial Solution/Eventable Object/Eventable Screen"), RequireComponent(typeof(Camera))]
    public class EventableScreen : EventableTrigger
    {
        [Header("Pointer")]
        public PointerEventInput input;

        private Ray ray;
        public override Ray Ray => ray;

        private new Camera camera;

        private void Awake() => camera = GetComponent<Camera>();

        private void Update()
        {
            PointerEventData pointer = input.GetValue();

            if (pointer != null)
            {
                ray = camera.ScreenPointToRay(pointer.position);

                UpdateTrigger();

                HoldObject();
            }
            else ReleaseObject();
        }
    }
}
