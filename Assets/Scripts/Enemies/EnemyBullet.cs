using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private float speed = 25f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float lifetime = 3f;

    private Vector3 travelDirection;
    private Transform owner;
    private bool hasHit;

    public void Initialize(Transform targetTransform, Transform ownerTransform)
    {
        owner = ownerTransform;

        // Lock the direction toward the target's position at firing time.
        if (targetTransform != null)
        {
            travelDirection =
                (targetTransform.position - transform.position).normalized;
        }
        else
        {
            travelDirection = transform.forward;
        }

        if (travelDirection.sqrMagnitude > 0.001f)
            transform.rotation = Quaternion.LookRotation(travelDirection);

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if (hasHit)
            return;

        // Continue along the original direction; never track the target.
        transform.position += travelDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHit)
            return;

        // Ignore the soldier that fired this bullet.
        if (owner != null &&
            (other.transform == owner || other.transform.IsChildOf(owner)))
        {
            return;
        }

        // Ignore other enemy bullets.
        if (other.GetComponentInParent<EnemyBullet>() != null)
            return;

        hasHit = true;

        IDamageable damageable = other.GetComponentInParent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}