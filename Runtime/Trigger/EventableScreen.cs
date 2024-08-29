using UnityEngine;
using UnityEngine.EventSystems;

namespace FTGAMEStudio.InitialSolution.EventableObject
{
    [RequireComponent(typeof(Camera))]
    public abstract class EventableScreen : EventableTrigger
    {
        private Ray ray;
        public override Ray Ray => ray;


        protected abstract PointerEventData Data { get; }

        private new Camera camera;

        private void Awake() => camera = GetComponent<Camera>();

        private void Update()
        {
            if (Data != null)
            {
                ray = camera.ScreenPointToRay(Data.position);

                UpdateTrigger();

                HoldObject();
            }
            else ReleaseObject();
        }
    }
}
