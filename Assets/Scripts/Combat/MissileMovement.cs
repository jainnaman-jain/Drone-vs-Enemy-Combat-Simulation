
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MissileMovement : MonoBehaviour
{
    [Header("Missile Settings")]
    [SerializeField] private float speed = 25f;
    [SerializeField] private float lifetime = 10f;
    [SerializeField] private float damage = 50f;

    [Header("Explosion Settings")]
    [SerializeField] private float explosionRadius = 4f;
    [SerializeField] private float maximumBlastDamage = 50f;
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private float explosionCleanupTime = 2f;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField, Range(0f, 1f)] private float explosionVolume = 1f;

    private Rigidbody rb;
    private Transform owner;
    private bool hasExploded;

    [Header("Launch Audio")]
    [SerializeField] private AudioClip missileLaunchClip;
    [SerializeField] private float launchAudioVolume = 1f;

    private AudioSource launchAudioSource;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;

        if (missileLaunchClip != null)
        {
            launchAudioSource = gameObject.AddComponent<AudioSource>();
            launchAudioSource.clip = missileLaunchClip;
            launchAudioSource.volume = launchAudioVolume;
            launchAudioSource.playOnAwake = false;
            launchAudioSource.loop = false;
            launchAudioSource.spatialBlend = 0f;
            launchAudioSource.Play();
        }
    }

    public void Initialize(Transform missileOwner, Transform targetTransform)
    {
        owner = missileOwner;

        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate()
    {
        if (hasExploded)
            return;

        Vector3 movement = transform.forward * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasExploded)
            return;

        if (owner != null &&
            (other.transform == owner || other.transform.IsChildOf(owner)))
        {
            return;
        }

        if (other.GetComponentInParent<EnemyBullet>() != null)
            return;

        Explode();
    }

    private void Explode()
    {
        if (hasExploded)
            return;

        hasExploded = true;

        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(
                explosionSound,
                transform.position,
                explosionVolume
            );
        }

        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            explosionRadius,
            ~0,
            QueryTriggerInteraction.Collide
        );

        HashSet<IDamageable> damagedTargets = new HashSet<IDamageable>();

        foreach (Collider hit in hits)
        {
            if (owner != null &&
                (hit.transform == owner || hit.transform.IsChildOf(owner)))
            {
                continue;
            }

            if (hit.GetComponentInParent<EnemyBullet>() != null)
                continue;

            IDamageable damageable = hit.GetComponentInParent<IDamageable>();

            if (damageable == null || !damagedTargets.Add(damageable))
                continue;

            MonoBehaviour damageableBehaviour = damageable as MonoBehaviour;

            Vector3 targetPosition = damageableBehaviour != null
                ? damageableBehaviour.transform.position
                : hit.transform.position;

            float distance = Vector3.Distance(
                transform.position,
                targetPosition
            );

            if (distance > explosionRadius)
                continue;

            float damageFraction = Mathf.Clamp01(
                1f - distance / Mathf.Max(explosionRadius, 0.01f)
            );

            float appliedDamage = Mathf.Max(
                damage * 0.5f,
                maximumBlastDamage * damageFraction
            );

            damageable.TakeDamage(appliedDamage);

            Debug.Log(
                $"Missile explosion damaged {hit.name} " +
                $"for {appliedDamage:F1} damage."
            );
        }

        if (explosionEffect != null)
        {
            ParticleSystem effect = Instantiate(
                explosionEffect,
                transform.position,
                Quaternion.identity
            );

            Destroy(effect.gameObject, explosionCleanupTime);
        }

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (launchAudioSource != null)
        {
            launchAudioSource.Stop();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
