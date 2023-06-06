using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerAnimator : MonoBehaviour
{
    readonly string ISOPENED = "OpenClose";

    [SerializeField] IInteractor container;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        container = GetComponentInParent<IInteractor>();
        container.OnInteract += OnOpeningContainer;
    }

    void OnOpeningContainer(object sender, System.EventArgs e)
    {
        animator.SetTrigger(ISOPENED);
    }
}
