using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    readonly string ISMOVING = "IsMoving";

    [SerializeField] Player player;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        animator.SetBool(ISMOVING, player.IsMoving());
    }
}
