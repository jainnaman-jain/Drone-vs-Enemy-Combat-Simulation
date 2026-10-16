
using UnityEngine;

public class DroneCameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Camera Position")]
    [SerializeField] private Vector3 offset = new Vector3(2f, 3f, -7f);
    [SerializeField] private float followSpeed = 6f;
    [SerializeField] private float lookHeight = 0.5f;
    [SerializeField] private float lookAheadDistance = 10f;

    [Header("Mouse Look")]
    [SerializeField] private float mouseSensitivity = 3f;
    [SerializeField] private float minPitch = -20f;
    [SerializeField] private float maxPitch = 60f;

    private float yaw;
    private float pitch = 15f;

    private void Start()
    {
        if (target == null)
        {
            Debug.LogWarning(
                "DroneCameraFollow: Assign the drone target.", this);
            return;
        }

        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = NormalizeAngle(angles.x);
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        Quaternion cameraRotation = Quaternion.Euler(pitch, yaw, 0f);

        Vector3 cameraOffset = cameraRotation * offset;
        Vector3 desiredPosition = target.position + cameraOffset;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            followSpeed * Time.deltaTime
        );

        Vector3 lookDirection = cameraRotation * Vector3.forward;
        Vector3 lookPoint =
            target.position +
            Vector3.up * lookHeight +
            lookDirection * lookAheadDistance;

        transform.rotation = Quaternion.LookRotation(
            lookPoint - transform.position
        );
    }

    private float NormalizeAngle(float angle)
    {
        if (angle > 180f)
            angle -= 360f;

        return angle;
    }
}
