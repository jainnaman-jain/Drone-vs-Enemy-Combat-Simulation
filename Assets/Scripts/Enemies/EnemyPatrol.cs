
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Waypoints")]
    [SerializeField] private Transform[] waypoints;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private float reachDistance = 0.3f;

    private int currentWaypointIndex;
    private EnemyDetection enemyDetection;

    private void Awake()
    {
        enemyDetection = GetComponent<EnemyDetection>();
    }

    private void Update()
    {
        if (enemyDetection != null &&
            enemyDetection.HasDetectedDrone)
        {
            return;
        }

        if (waypoints == null || waypoints.Length == 0)
            return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];

        if (targetWaypoint == null)
            return;

        Vector3 direction = targetWaypoint.position - transform.position;
        direction.y = 0f;

        if (direction.magnitude <= reachDistance)
        {
            currentWaypointIndex =
                (currentWaypointIndex + 1) % waypoints.Length;

            targetWaypoint = waypoints[currentWaypointIndex];

            if (targetWaypoint == null)
                return;

            direction = targetWaypoint.position - transform.position;
            direction.y = 0f;
        }

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(direction.normalized);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                turnSpeed * Time.deltaTime
            );

            transform.position +=
                direction.normalized * moveSpeed * Time.deltaTime;
        }
    }
}
