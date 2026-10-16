
using UnityEngine;

public class DroneTargeting : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera droneCamera;

    [Header("Targeting Settings")]
    [SerializeField] private float targetingRange = 100f;
    [SerializeField] private LayerMask targetLayers = ~0;

    [Header("Debug")]
    [SerializeField] private Transform currentTarget;

    public Transform CurrentTarget => currentTarget;

    private void Update()
    {
        currentTarget = null;

        if (droneCamera == null)
            return;

        Ray ray = droneCamera.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0f)
        );

        if (Physics.Raycast(
            ray,
            out RaycastHit hit,
            targetingRange,
            targetLayers,
            QueryTriggerInteraction.Ignore))
        {
            // Never target the drone or anything in its hierarchy.
            if (hit.transform == transform ||
                hit.transform.IsChildOf(transform))
            {
                return;
            }

            IDamageable damageable =
                hit.collider.GetComponentInParent<IDamageable>();

            if (damageable != null)
            {
                currentTarget =
                    ((MonoBehaviour)damageable).transform;
            }
        }

        Debug.DrawRay(
            ray.origin,
            ray.direction * targetingRange,
            currentTarget != null ? Color.green : Color.yellow
        );
    }
}
