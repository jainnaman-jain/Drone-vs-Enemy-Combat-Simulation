
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MissileMovement missilePrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Camera droneCamera;

    [Header("Firing Settings")]
    [SerializeField] private float fireCooldown = 0.7f;
    [SerializeField] private float aimRange = 200f;
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip missileLaunchClip;

    private float nextFireTime;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) ||
            Input.GetMouseButtonDown(0))
        {
            FireMissile();
        }
    }

    private void FireMissile()
    {
        if (Time.time < nextFireTime)
            return;

        if (missilePrefab == null || spawnPoint == null)
        {
            Debug.LogWarning(
                "MissileLauncher: Assign the missile prefab and spawn point.",
                this
            );
            return;
        }

        Camera cam = droneCamera != null
            ? droneCamera
            : Camera.main;

        if (cam == null)
        {
            Debug.LogWarning("MissileLauncher: No camera assigned.", this);
            return;
        }

        nextFireTime = Time.time + fireCooldown;

        Ray aimRay = cam.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0f)
        );

        Vector3 aimPoint;

        if (Physics.Raycast(
            aimRay,
            out RaycastHit hit,
            aimRange,
            Physics.DefaultRaycastLayers,
            QueryTriggerInteraction.Ignore))
        {
            aimPoint = hit.point;
        }
        else
        {
            aimPoint = aimRay.origin + aimRay.direction * aimRange;
        }

        Vector3 launchDirection = aimPoint - spawnPoint.position;

        if (launchDirection.sqrMagnitude < 0.001f)
            launchDirection = cam.transform.forward;

        Quaternion launchRotation =
            Quaternion.LookRotation(launchDirection.normalized);

        MissileMovement missile = Instantiate(
            missilePrefab,
            spawnPoint.position,
            launchRotation
        );

        missile.Initialize(transform, null);

        

        Debug.Log("Missile launched along the crosshair aim direction.");
    }
}
