using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunAim : MonoBehaviour
{
    [SerializeField] private Transform aimTransform;
    [SerializeField] private float aimSpeed = 5f;
    [SerializeField] private float zoomFOV = 40f;
    [SerializeField] private float defaultFOV = 60f;
    [SerializeField] private Vector3 aimPosition;

    private Vector3 originalPosition;
    public bool isAiming = false;
    private Camera mainCamera;

    GunAnimationController gunAnimationController;

    private void Awake()
    {
        gunAnimationController = GetComponent<GunAnimationController>();
        originalPosition = aimTransform.localPosition;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (isAiming)
        {
            aimTransform.localPosition = Vector3.Lerp(aimTransform.localPosition, aimPosition, Time.deltaTime * aimSpeed);
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, zoomFOV, Time.deltaTime * aimSpeed);
        }
        else
        {
            aimTransform.localPosition = Vector3.Lerp(aimTransform.localPosition, originalPosition, Time.deltaTime * aimSpeed);
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, defaultFOV, Time.deltaTime * aimSpeed);
        }
    }
}
