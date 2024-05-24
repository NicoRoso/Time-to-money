using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerToCivilGround : MonoBehaviour
{
    [SerializeField] private InputActionAsset playerInput;

    private InputAction screamAction;

    [SerializeField] private float radius = 10f;

    private void Awake()
    {
        screamAction = playerInput.FindActionMap("Player").FindAction("Scream");

        screamAction.performed += context => CheckForCivilCollision();
    }

    private void OnEnable()
    {
        screamAction.Enable();
    }

    private void OnDisable()
    {
        screamAction.Disable();
    }

    void CheckForCivilCollision()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in colliders)
        {
            if (col.gameObject.GetComponent<TakeDamage>())
            {
                CivilAnimations civilAnim = col.GetComponentInParent<CivilAnimations>();

                if (civilAnim != null)
                {
                    civilAnim.HostageAnimation(true);
                }
            }
        }
    }
}
