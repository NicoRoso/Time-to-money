using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CivilIntoRobber : MonoBehaviour
{
    [SerializeField] private GameObject _weaponHands;
    [SerializeField] private GameObject _events;
    [SerializeField] private GameObject _spawner;
    [SerializeField] private GameObject _hud;
    [SerializeField] private GameObject _hudCivl;
    [SerializeField] private PlayerToCivilGround _civilAction;
    [SerializeField] private PlayerInteract _interactAction;
    [SerializeField] private InputActionAsset _playerInput;
    [SerializeField] private float _holdTime = 2.0f;

    [SerializeField] private Image _fillAmount;

    private float holdTimer = 0f;
    private bool isHolding = false;
    private InputAction activeRobberAction;

    [SerializeField] private AudioClip[] begginingVoice;
    public static Action<AudioClip[]> isBegun;

    void Awake()
    {
        _weaponHands.SetActive(false);
        _civilAction.enabled = false;
        _interactAction.enabled = false;
        _fillAmount.fillAmount = 0f;

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
        _fillAmount.fillAmount = 0f;
    }

    void Update()
    {
        if (isHolding)
        {
            holdTimer += Time.deltaTime;
            _fillAmount.fillAmount = holdTimer / _holdTime; // Update fill amount

            if (holdTimer >= _holdTime)
            {
                if (!_weaponHands.activeInHierarchy)
                {
                    _hudCivl.SetActive(false);
                    isBegun?.Invoke(begginingVoice);
                    _weaponHands.SetActive(true);
                    _events.SetActive(true);
                    _hud.SetActive(true);
                    _civilAction.enabled = true;
                    _interactAction.enabled = true;
                    this.enabled = false;
                }
            }
        }
    }
}
