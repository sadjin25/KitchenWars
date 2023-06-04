using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjHolder
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

    [SerializeField] private KitchenObject objOnHand;
    [SerializeField] private Transform onHandTransform;

    public event EventHandler OnObjChanging;

    void Awake()
    {
        gameInputs.OnInteractAction += OnInputInteraction;
        OnObjChanging += DestroyObjInstance;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        if (objOnHand)
        {
            SetKitchenObj(objOnHand);
        }
    }

    void Update()
    {
        Move();
        SelectFacedInteractor();
    }

    #region PlayerControl


    void Move()
    {
        Vector2 XY = gameInputs.GetMoveInputValue();
        Vector3 moveDir = new Vector3(XY.x, 0f, XY.y);
        if (moveDir != Vector3.zero)
        {
            lastMoveDir = moveDir;
        }
        RotateModel(lastMoveDir);
        rb.velocity = new Vector3(XY.x * speed, 0f, XY.y * speed);
    }

    void RotateModel(Vector3 moveDir)
    {
        transform.forward = moveDir;
    }

    void OnInputInteraction(object sender, EventArgs e)
    {
        if (selectedInteractor != null)
        {
            selectedInteractor.Interact(objOnHand);
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


    #endregion

    #region IKitchenObjHolder

    public Transform GetKitchenObjLocation()
    {
        return onHandTransform;
    }

    public void SetKitchenObj(KitchenObject obj)
    {
        OnObjChanging?.Invoke(this, EventArgs.Empty);

        GameObject o = Instantiate(obj.GetKitchenObjectSO().prefab, onHandTransform);
        o.GetComponent<KitchenObject>().SetKitchenObjHolder(this);
        objOnHand = o.GetComponent<KitchenObject>();
    }

    public void ClearKitchenObj()
    {
        OnObjChanging?.Invoke(this, EventArgs.Empty);
        objOnHand = null;
    }

    public bool HasKitchenObj()
    {
        return objOnHand != null;
    }

    public KitchenObject GetKitchenObj()
    {
        return objOnHand;
    }

    public void DestroyObjInstance(object sender, EventArgs e)
    {
        for (int i = 0; i < onHandTransform.childCount; i++)
        {
            Destroy(onHandTransform.GetChild(i).gameObject);
        }
    }

    #endregion
}
