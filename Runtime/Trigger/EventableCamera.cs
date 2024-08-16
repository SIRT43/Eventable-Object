using UnityEngine;

namespace FTGAMEStudio.InitialSolution.EventableObject
{
    [AddComponentMenu("Initial Solution/Eventable Object/Eventable Camera"), RequireComponent(typeof(Camera))]
    public class EventableCamera : EventableTrigger
    {
        public override Ray Ray => new(transform.position, transform.forward);

        private void Update() => UpdateTrigger();
    }
}
