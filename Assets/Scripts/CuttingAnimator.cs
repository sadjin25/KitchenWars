using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingAnimator : MonoBehaviour
{
    readonly string CUT = "Cut";

    [SerializeField] CuttingCounter container;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        container = GetComponentInParent<CuttingCounter>();
        container.OnCut += AnimOnCut;
    }

    void AnimOnCut(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}
