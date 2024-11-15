using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public float interactDistance = 3f;
    public InputActionAsset playerInput;

    InputAction interact;
    bool isInteract;
    bool interactProcessed;

    private void Awake()
    {
        interact = playerInput.FindActionMap("Player").FindAction("Use");

        interact.performed += context => isInteract = true;
        interact.canceled += context => isInteract = false;
    }

    private void OnEnable()
    {
        interact.Enable();
    }

    private void OnDisable()
    {
        interact.Disable();
    }

    void Update()
    {
        if (isInteract && !interactProcessed)
        {
            interactProcessed = true;

            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                if (hit.collider.CompareTag("Interactable"))
                {
                    Destroy(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("TimeInteract"))
                {
                    TimeInteractScript timeInteractScript = hit.collider.GetComponent<TimeInteractScript>();
                    if (timeInteractScript != null)
                    {
                        timeInteractScript.StartCountdown();
                    }
                }
            }
        }

        if (!isInteract)
        {
            interactProcessed = false;
        }
    }
}
