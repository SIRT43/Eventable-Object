using UnityEngine;

namespace InitialSolution.EventableObject
{
    [RequireComponent(typeof(Camera))]
    public abstract class EventableScreen : EventableTrigger
    {
        private Ray ray;
        public override Ray Ray => ray;


        protected abstract Vector2? ScreenPos { get; }

        private new Camera camera;

        private void Awake() => camera = GetComponent<Camera>();

        private void Update()
        {
            if (ScreenPos != null)
            {
                ray = camera.ScreenPointToRay((Vector3)ScreenPos);

                UpdateTrigger();
                HoldObject();
            }
            else ReleaseObject();
        }
    }
}
