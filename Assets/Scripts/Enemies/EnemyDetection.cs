
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private Transform droneTarget;
    [SerializeField] private float detectionRange = 15f;

    public bool HasDetectedDrone { get; private set; }

    private void Update()
    {
        if (droneTarget == null)
            return;

        float distance = Vector3.Distance(
            transform.position,
            droneTarget.position
        );

        HasDetectedDrone = distance <= detectionRange;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
