using System;
using UnityEngine;

public class PoliceShooting : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float rotationSpeed = 10f;
    public Transform raycastOriginOffset;

    [SerializeField] private int damage = 5;

    private float fireRate = 7f;
    private float nextFireTime = 0f;

    public ParticleSystem muzzleFlash;

    [SerializeField] private PoliceHP hp;
    [SerializeField] private AnimationEnemy animatorEnemy;

    private PoliceAI policeAI;

    [SerializeField] private AudioClip[] _enemySpotedLines;

    public Action<AudioClip[]> _isSpoted;

    private bool isSpoting;

    private void Awake()
    {
        isSpoting = false;
        policeAI = GetComponent<PoliceAI>();
        hp = GetComponent<PoliceHP>();
        animatorEnemy = GetComponent<AnimationEnemy>();
    }

    public void Update()
    {
        target = policeAI.target;

        if (target != null)
        {
            Vector3 targetDir = target.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDir);
            targetRotation *= Quaternion.Euler(0f, 31f, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        Vector3 raycastOrigin = raycastOriginOffset != null ? raycastOriginOffset.position : transform.position;

        if (Time.time >= nextFireTime)
        {

            RaycastHit hit;
            if (Physics.Raycast(raycastOrigin, raycastOriginOffset.forward, out hit, Mathf.Infinity))
            {

                if (!isSpoting)
                {
                    _isSpoted?.Invoke(_enemySpotedLines);
                    isSpoting = true;
                }

                Debug.DrawRay(raycastOrigin, raycastOriginOffset.forward * hit.distance, Color.red);

                if (hit.collider.transform == target)
                {
                    Shoot();
                    nextFireTime = Time.time + 1f/fireRate;
                }
            }
        }

        if (hp.isDead)
        {
            this.enabled = false;
        }
    }

    private void Shoot()
    {
        muzzleFlash.Play();

        if (target.gameObject.GetComponent<TakePlayerDamage>() != null)
        {
            target.gameObject.GetComponent<TakePlayerDamage>().DecreaseHp(damage);
        }

        animatorEnemy.Fire();
    }
}
