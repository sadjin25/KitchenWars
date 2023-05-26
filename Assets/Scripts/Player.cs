using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance
    {
        get;
        private set;
    }

    [SerializeField] private GameInputs gameInputs;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private LayerMask interactionMask;

    private readonly float speed = 5f;
    private Vector3 lastMoveDir;

    private IInteractor selectedInteractor;
    readonly float interactionDistance = 3f;
    public event EventHandler<OnSelectedInteractorChangedEventArgs> OnSelectedInteractorChanged;

    public class OnSelectedInteractorChangedEventArgs : EventArgs
    {
        public IInteractor selectedInteractor_;
    }

    void Awake()
    {
        gameInputs.OnInteractAction += OnInputInteraction;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        Move();
        SelectFacedInteractor();
    }

    void Move()
    {
        Vector2 XY = gameInputs.GetMoveInputValue();
        Vector3 moveDir = new Vector3(XY.x, 0f, XY.y);
        if (moveDir != Vector3.zero)
        {
            lastMoveDir = moveDir;
        }
        rb.velocity = new Vector3(XY.x * speed, 0f, XY.y * speed);
    }


    void OnInputInteraction(object sender, EventArgs e)
    {
        if (selectedInteractor != null)
        {
            selectedInteractor.Interact();
        }
    }

    void SelectFacedInteractor()
    {
        if (Physics.Raycast(transform.position, lastMoveDir, out RaycastHit raycastHit, interactionDistance, interactionMask))
        {
            if (raycastHit.transform.TryGetComponent(out IInteractor interactor))
            {
                if (interactor != selectedInteractor)
                {
                    SetSelectedInteractor(interactor);
                }
            }
            else
            {
                SetSelectedInteractor(null);
            }
        }
        else
        {
            SetSelectedInteractor(null);
        }
    }

    void SetSelectedInteractor(IInteractor interactor)
    {
        selectedInteractor = interactor;
        OnSelectedInteractorChanged?.Invoke(this, new OnSelectedInteractorChangedEventArgs
        {
            selectedInteractor_ = selectedInteractor
        });
    }
}
