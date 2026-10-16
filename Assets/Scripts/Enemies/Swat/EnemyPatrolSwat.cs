using UnityEngine;

public class EnemyPatrolSwat : MonoBehaviour
{
    [Header("Patrol Waypoints")]
    [SerializeField] private Transform[] waypoints;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private float reachDistance = 0.3f;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private string walkingParameter = "isWalking";

    private int currentWaypointIndex;
    private EnemyDetection enemyDetection;
    private bool isWalking;

    private void Awake()
    {
        enemyDetection = GetComponent<EnemyDetection>();

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (enemyDetection != null &&
            enemyDetection.HasDetectedDrone)
        {
            SetWalking(false);
            return;
        }

        if (waypoints == null || waypoints.Length == 0)
        {
            SetWalking(false);
            return;
        }

        Transform targetWaypoint = waypoints[currentWaypointIndex];

        if (targetWaypoint == null)
        {
            SetWalking(false);
            return;
        }

        Vector3 direction = targetWaypoint.position - transform.position;
        direction.y = 0f;

        if (direction.magnitude <= reachDistance)
        {
            currentWaypointIndex =
                (currentWaypointIndex + 1) % waypoints.Length;

            targetWaypoint = waypoints[currentWaypointIndex];

            if (targetWaypoint == null)
            {
                SetWalking(false);
                return;
            }

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

            SetWalking(true);
        }
        else
        {
            SetWalking(false);
        }
    }

    private void SetWalking(bool walking)
    {
        if (isWalking == walking)
            return;

        isWalking = walking;

        if (animator != null)
            animator.SetBool(walkingParameter, isWalking);
    }
}
