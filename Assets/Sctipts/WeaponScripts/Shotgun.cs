using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shotgun : MonoBehaviour
{
    [SerializeField] private float range = 30f;
    public ParticleSystem muzzleFlash;
    [SerializeField] private int pelletsPerShot = 8;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float impactForce = 10f;

    [Header("Damage")]
    [SerializeField] private int damage;


    [Header("Effects")]
    public GameObject ImpactEffect;
    public GameObject BloodEffect;

    [Header("Recoil")]
    [SerializeField] private float recoilForce = 1f;
    [SerializeField] private float recoilResetSpeed = 2f;

    [Header("Ammo")]
    [SerializeField] private int maxAmmo = 8;
    private int currentAmmo;
    [SerializeField] private float reloadTime = 2f;
    public bool isReloading = false;

    private float nextTimeToFire = 0f;
    private Camera cam;

    [SerializeField] private InputActionAsset inputPlayer;

    private InputAction fireAction;
    private InputAction reloadAction;
    private InputAction aimMode;

    private GunAim gunAim;

    private GunAnimationController gunAnimationController;

    private void Awake()
    {
        cam = Camera.main;
        currentAmmo = maxAmmo;

        gunAim = GetComponent<GunAim>();
        gunAnimationController = GetComponent<GunAnimationController>();

        fireAction = inputPlayer.FindActionMap("Player").FindAction("Fire");
        reloadAction = inputPlayer.FindActionMap("Player").FindAction("Reload");
        aimMode = inputPlayer.FindActionMap("Player").FindAction("Aim");

        fireAction.performed += OnFirePerformed;

        reloadAction.performed += OnReloadPerformed;

        aimMode.performed += context => OnAiming(true);
        aimMode.canceled += context => OnAiming(false);
    }

    private void OnEnable()
    {
        fireAction.Enable();
        reloadAction.Enable();
        aimMode.Enable();
    }

    private void OnDisable()
    {
        fireAction.Disable();
        reloadAction.Disable();
        aimMode.Disable();
    }

    private void Update()
    {
        if (isReloading)
        {
            gunAim.isAiming = false;
        }
    }

    private void OnFirePerformed(InputAction.CallbackContext context)
    {
        if (isReloading)
            return;

        if (Time.time >= nextTimeToFire && currentAmmo > 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
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

        if (currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }
    }

    void Shoot()
    {
        if (this.gameObject.activeSelf)
        {
            if (gameObject.activeSelf)
            {
                muzzleFlash.Play();
                ApplyRecoil();
            }
            gunAnimationController.ShotgunAfterFire();

            for (int i = 0; i < pelletsPerShot; i++)
            {
                Vector3 direction = cam.transform.forward + Random.insideUnitSphere * 0.1f;
                RaycastHit hit;
                if (Physics.Raycast(cam.transform.position, direction, out hit, range))
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
            }
            currentAmmo--;
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        gunAim.isAiming = false;
        gunAnimationController.ReloadingAnimation();
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void ApplyRecoil()
    {
        Vector3 recoilVector = new Vector3(Random.Range(-recoilForce, recoilForce), Random.Range(-recoilForce, recoilForce), 0f);
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
}
