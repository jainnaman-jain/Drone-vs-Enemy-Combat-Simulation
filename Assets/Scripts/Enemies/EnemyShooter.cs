using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemyDetection enemyDetection;
    [SerializeField] private Transform droneTarget;
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private EnemyBullet bulletPrefab;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gunshotClip;

    [Header("Shooting Settings")]
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private float shootingRange = 15f;

    private float nextFireTime;
    private bool isShooting;

    private void Update()
    {
        bool canShoot =
            enemyDetection != null &&
            enemyDetection.HasDetectedDrone &&
            droneTarget != null &&
            muzzlePoint != null &&
            bulletPrefab != null &&
            Vector3.Distance(transform.position, droneTarget.position)
                <= shootingRange;

        SetShootingAnimation(canShoot);

        if (!canShoot)
            return;

        // Aim the soldier toward the drone on the horizontal plane.
        Vector3 direction = droneTarget.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                180f * Time.deltaTime
            );
        }

        if (Time.time >= nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void SetShootingAnimation(bool shooting)
    {
        if (isShooting == shooting)
            return;

        isShooting = shooting;

        if (animator != null)
        {
            animator.SetBool("isShooting", isShooting);
        }
    }

    private void FireBullet()
    {
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        if (audioSource != null && gunshotClip != null)
        {
            audioSource.PlayOneShot(gunshotClip);
        }

        Vector3 direction =
            (droneTarget.position - muzzlePoint.position).normalized;

        GameObject bulletObject = Instantiate(
            bulletPrefab.gameObject,
            muzzlePoint.position,
            Quaternion.LookRotation(direction)
        );

        EnemyBullet bullet = bulletObject.GetComponent<EnemyBullet>();

        if (bullet != null)
        {
            bullet.Initialize(droneTarget, transform.root);
        }
    }

    private void OnDisable()
    {
        if (animator != null)
        {
            animator.SetBool("isShooting", false);
        }
    }
}