using InitialFramework;
using UnityEngine;

namespace InitialSolution.EventableObject
{
    [AddComponentMenu("Initial Solution/Eventable Object/Moveable Object"), RequireComponent(typeof(Rigidbody))]
    public class MoveableObject : EventableBehaviour, IHoldingHandler, IBeginHoldHandler, IEndHoldHandler
    {
        [Header("Detection")]
        [Min(0)] public float density = 1;
        [Range(0, 1)] public float inference = 0.5f;

        [Header("Holding")]
        public ForceMode forceMode = ForceMode.Acceleration;
        [Range(0, 200)] public float forceScale = 50;

        protected new Rigidbody rigidbody;
        protected int layerBackup;

        protected override void Awake()
        {
            base.Awake();
            rigidbody = GetComponent<Rigidbody>();
        }

        public void OnBeginHold(Holder sponsor)
        {
            rigidbody.useGravity = false;

            layerBackup = gameObject.layer;
            gameObject.layer = 2;
        }

        public void OnEndHold(Holder sponsor)
        {
            gameObject.layer = layerBackup;
            rigidbody.useGravity = true;
        }

        public void Holding(Holder sponsor)
        {
            if (sponsor is not EventableTrigger eventable) return;

            rigidbody.velocity = Vector3.zero;

            float length = collider.bounds.size.magnitude / 2;
            Probe probe = new(eventable.Ray.origin, eventable.Ray.direction, length);

            DetectionResult result = IFPhysics.Detection(probe, eventable.maxDistance, density, inference, eventable.targetLayer);

            Vector3 force = (result.position - rigidbody.position) * forceScale;

            rigidbody.AddForce(force, forceMode);
        }
    }
}
