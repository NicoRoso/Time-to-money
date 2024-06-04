using UnityEngine;
using UnityEngine.InputSystem;

public class CivilIntoRobber : MonoBehaviour
{
    [SerializeField] private GameObject _weaponHands;
    [SerializeField] private PlayerToCivilGround _civilAction;
    [SerializeField] private InputActionAsset _playerInput;
    [SerializeField] private float _holdTime = 2.0f;

    private float holdTimer = 0f;
    private bool isHolding = false;
    private InputAction activeRobberAction;

    void Awake()
    {
        _weaponHands.SetActive(false);
        _civilAction.enabled = false;
        activeRobberAction = _playerInput.FindActionMap("Player").FindAction("Drop");

        activeRobberAction.performed += OnHoldActionPerformed;
        activeRobberAction.canceled += OnHoldActionCanceled;
    }

    private void OnEnable()
    {
        activeRobberAction.Enable();
    }

    private void OnDisable()
    {
        activeRobberAction.Disable();
    }

    private void OnHoldActionPerformed(InputAction.CallbackContext context)
    {
        isHolding = true;
    }

    private void OnHoldActionCanceled(InputAction.CallbackContext context)
    {
        isHolding = false;
        holdTimer = 0f;
    }

    void Update()
    {
        if (isHolding)
        {
            holdTimer += Time.deltaTime;
            if (holdTimer >= _holdTime)
            {
                if (!_weaponHands.activeInHierarchy)
                {
                    _weaponHands.SetActive(true);
                    _civilAction.enabled = true;
                    this.enabled = false;
                }
            }
        }
    }
}
