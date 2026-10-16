
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DroneController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float verticalSpeed = 5f;
    [SerializeField] private float rotationSpeed = 8f;

    [Header("Mouse Facing")]
    [SerializeField] private Transform cameraTransform;

    private Rigidbody rb;
    private Vector3 moveInput;

    public Vector3 MoveInput => moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float forward = Input.GetAxisRaw("Vertical");

        float vertical = 0f;

        if (Input.GetKey(KeyCode.E)) vertical = 1f;
        if (Input.GetKey(KeyCode.Q)) vertical = -1f;

        // Use the camera's horizontal direction for drone movement.
        Vector3 forwardDirection = cameraTransform != null
            ? cameraTransform.forward
            : transform.forward;

        forwardDirection.y = 0f;

        if (forwardDirection.sqrMagnitude > 0.001f)
            forwardDirection.Normalize();
        else
            forwardDirection = transform.forward;

        Vector3 rightDirection = Vector3.Cross(
            Vector3.up,
            forwardDirection
        ).normalized;

        Vector3 horizontalMovement =
            forwardDirection * forward +
            rightDirection * horizontal;

        horizontalMovement = Vector3.ClampMagnitude(
            horizontalMovement,
            1f
        );

        moveInput =
            horizontalMovement * moveSpeed +
            Vector3.up * vertical * verticalSpeed;

        // Rotate the drone toward the camera's horizontal direction.
        if (forwardDirection.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(forwardDirection);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput;
    }
}
