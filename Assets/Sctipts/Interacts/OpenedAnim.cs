using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenedAnim : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        TimeInteractScript.isStartOpen += Open;
    }

    private void OnDisable()
    {
        TimeInteractScript.isStartOpen -= Open;
    }

    public void Open()
    {
        animator.SetTrigger("Open");
    }
}
