using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private float range = 100f;
    public ParticleSystem muzzleFlash;
    [SerializeField] private float fireRate = 10f;
    [SerializeField] private float impactForce = 30f;

    [Header("Damage")]
    [SerializeField] private int damage;

    [Header("Effects")]
    public GameObject ImpactEffect;
    public GameObject BloodEffect;

    [Header("Recoil")]
    [SerializeField] private float recoilForce = 0.5f;
    [SerializeField] private float recoilResetSpeed = 2f;

    [Header("Ammo")]
    [SerializeField] private int maxAmmo;
    [SerializeField] private int allAmmo;
    private int currentAmmo;
    [SerializeField] private float reloadTime = 1.5f;
    public bool isReloading = false;

    private float nextTimeToFire = 0f;
    private Camera cam;

    public enum FireMode
    {
        SemiAuto,
        FullAuto,
        Burst
    }

    [SerializeField] private bool isPistol = false;

    [SerializeField] private FireMode fireMode = FireMode.SemiAuto;
    [SerializeField] private int burstCount = 3;
    [SerializeField] private float timeBetweenBursts = 0.1f;

    private int currentBurstCount = 0;
    private bool isBursting = false;

    [SerializeField] private InputActionAsset inputPlayer;

    private InputAction fireAction;
    private InputAction reloadAction;
    private InputAction changeFireMode;
    private InputAction aimMode;

    private GunAim gunAim;

    [Header("Animation")]
    private GunAnimationController animationController;

    public static Action gunFired;

    [SerializeField] private AudioClip emptyAmmo;
    public static Action<AudioClip> emptyFired;

    private void Awake()
    {
        cam = Camera.main;
        currentAmmo = maxAmmo;
        allAmmo = maxAmmo * 3;

        gunAim = GetComponent<GunAim>();
        animationController = GetComponent<GunAnimationController>();

        fireAction = inputPlayer.FindActionMap("Player").FindAction("Fire");
        reloadAction = inputPlayer.FindActionMap("Player").FindAction("Reload");
        changeFireMode = inputPlayer.FindActionMap("Player").FindAction("Change FireMode");
        aimMode = inputPlayer.FindActionMap("Player").FindAction("Aim");

        fireAction.performed += OnFirePerformed;
        reloadAction.performed += OnReloadPerformed;
        changeFireMode.performed += OnChangeFireModePerformed;

        aimMode.performed += context => OnAiming(true);
        aimMode.canceled += context => OnAiming(false);
    }

    private void OnEnable()
    {
        fireAction.Enable();
        reloadAction.Enable();
        changeFireMode.Enable();
        aimMode.Enable();
    }

    private void OnDisable()
    {
        fireAction.Disable();
        reloadAction.Disable();
        changeFireMode.Disable();
        aimMode.Disable();
    }

    private void OnFirePerformed(InputAction.CallbackContext context)
    {
        if (!gameObject.activeSelf || isReloading)
            return;

        switch (fireMode)
        {
            case FireMode.SemiAuto:
                if (currentAmmo > 0 && Time.time >= nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1f / fireRate;
                    Shoot();
                }
                break;
            case FireMode.FullAuto:
                if (currentAmmo > 0 && Time.time >= nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1f / fireRate;
                    Shoot();
                }
                break;
            case FireMode.Burst:
                if (currentAmmo > 0 && Time.time >= nextTimeToFire && !isBursting)
                {
                    StartCoroutine(BurstFire());
                }
                break;
        }

        if (currentAmmo <= 0)
        {
            emptyFired?.Invoke(emptyAmmo);
        }
    }

    private void OnAiming(bool aim)
    {
        gunAim.isAiming = aim;

        if (gunAim.isAiming)
        {
            recoilForce = recoilForce - 0.2f;
        }
        else
        {
            recoilForce = recoilForce + 0.2f;
        }
    }

    private void OnReloadPerformed(InputAction.CallbackContext context)
    {
        if (!gameObject.activeSelf || isReloading)
            return;

        if (currentAmmo < maxAmmo && allAmmo > 0)
        {
            StartCoroutine(Reload());
        }
    }

    private void OnChangeFireModePerformed(InputAction.CallbackContext context)
    {
        switch (fireMode)
        {
            case FireMode.SemiAuto:
                if (!isPistol)
                    fireMode = FireMode.FullAuto;
                break;
            case FireMode.FullAuto:
                if (!isPistol)
                    fireMode = FireMode.Burst;
                break;
            case FireMode.Burst:
                fireMode = FireMode.SemiAuto;
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (fireMode == FireMode.FullAuto && fireAction.ReadValue<float>() > 0f && Time.time >= nextTimeToFire && !isReloading && currentAmmo > 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        if (isReloading)
        {
            gunAim.isAiming = false;
        }
    }

    IEnumerator BurstFire()
    {
        isBursting = true;
        currentBurstCount = 0;

        while (currentBurstCount < burstCount && currentAmmo > 0)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenBursts);
            currentBurstCount++;
        }

        isBursting = false;
    }

    void Shoot()
    {
        if (gameObject.activeSelf)
        {
            muzzleFlash.Play();
            ApplyRecoil();
        }

        gunFired?.Invoke();

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            if (hit.collider != null)
            {
                if (hit.rigidbody != null && !hit.collider.gameObject.CompareTag("Enemy"))
                {
                    hit.rigidbody.AddForce(hit.normal * -impactForce);
                }

                if (hit.collider.gameObject.GetComponent<TakeDamage>() != null)
                {
                    hit.collider.gameObject.GetComponent<TakeDamage>().DecreaseHP(damage);
                    if (BloodEffect != null)
                    {
                        if (hit.normal != Vector3.zero)
                        {
                            GameObject impactGo = Instantiate(BloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                            Destroy(impactGo, 2f);
                        }
                        else
                        {
                            GameObject impactGo = Instantiate(BloodEffect, hit.point, Quaternion.LookRotation(hit.point - cam.transform.position));
                            Destroy(impactGo, 2f);
                        }
                    }
                }
                else if (hit.collider.gameObject.GetComponent<TakeDamageCivil>() != null)
                {
                    hit.collider.gameObject.GetComponent<TakeDamageCivil>().DecreaseCivilHP(damage);
                    if (BloodEffect != null)
                    {
                        if (hit.normal != Vector3.zero)
                        {
                            GameObject impactGo = Instantiate(BloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                            Destroy(impactGo, 2f);
                        }
                        else
                        {
                            GameObject impactGo = Instantiate(BloodEffect, hit.point, Quaternion.LookRotation(hit.point - cam.transform.position));
                            Destroy(impactGo, 2f);
                        }
                    }
                }
                else if (ImpactEffect != null)
                {
                    if (hit.normal != Vector3.zero)
                    {
                        GameObject impactGo = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                        Destroy(impactGo, 2f);
                    }
                    else
                    {
                        GameObject impactGo = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.point - cam.transform.position));
                        Destroy(impactGo, 2f);
                    }
                }
            }
        }
        currentAmmo--;
    }

    IEnumerator Reload()
    {
        isReloading = true;
        gunAim.isAiming = false;
        animationController.ReloadingAnimation();

        int ammoToReload = maxAmmo - currentAmmo;
        int ammoAvailable = Mathf.Min(ammoToReload, allAmmo);

        yield return new WaitForSeconds(reloadTime);
        currentAmmo += ammoAvailable;
        allAmmo -= ammoAvailable;

        isReloading = false;
    }

    void ApplyRecoil()
    {
        Vector3 recoilVector = new Vector3(UnityEngine.Random.Range(-recoilForce, recoilForce), UnityEngine.Random.Range(-recoilForce, recoilForce), 0f);
        cam.transform.localPosition += recoilVector;
        StartCoroutine(ResetRecoil());
    }

    IEnumerator ResetRecoil()
    {
        while (cam.transform.localPosition != Vector3.zero)
        {
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, Vector3.zero, recoilResetSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void PlusAmmo(int amount)
    {
        allAmmo += amount;
        allAmmo = Mathf.Min(allAmmo, maxAmmo * 3);
    }
}
