using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Kitchen : MonoBehaviour, IKitchenObjHolder, IInteractor
{
    [SerializeField] KitchenObject kitchenObj;

    [SerializeField] private Transform counterTopPoint;

    public event EventHandler OnObjChanging;

    void Awake()
    {
        OnObjChanging += DestroyObjInstance;
        if (kitchenObj)
        {
            SetKitchenObj(kitchenObj);
        }
    }


    public void Interact(KitchenObject objOnHand)
    {
        //WARNING : always write SetKitchenObj before Clear! else, make temp var to copy and swap.

        // 키친이 비면 받은 아이템 가지기
        if (!HasKitchenObj() && objOnHand != null)
        {
            SetKitchenObj(objOnHand);
            Player.Instance.ClearKitchenObj();
        }
        // if kitchen has obj, then swap with objOnHand
        else
        {
            if (objOnHand == null)
            {
                Player.Instance.SetKitchenObj(kitchenObj);
                ClearKitchenObj();
            }
            else
            {
                // WARNING : Please! Deep Copy this! but no polymorphism.
                KitchenObject kitchenObjectTemp = objOnHand;

                SetKitchenObj(objOnHand);
                Player.Instance.SetKitchenObj(kitchenObjectTemp);
            }
        }
    }

    public Transform GetKitchenObjLocation()
    {
        return counterTopPoint;
    }

    public void SetKitchenObj(KitchenObject obj)
    {
        OnObjChanging?.Invoke(this, EventArgs.Empty);

        GameObject o = Instantiate(obj.GetKitchenObjectSO().prefab, counterTopPoint);
        o.GetComponent<KitchenObject>().SetKitchenObjHolder(this);
        kitchenObj = o.GetComponent<KitchenObject>();
    }

    public void ClearKitchenObj()
    {
        OnObjChanging?.Invoke(this, EventArgs.Empty);
        kitchenObj = null;
    }

    public bool HasKitchenObj()
    {
        return kitchenObj != null;
    }

    public KitchenObject GetKitchenObj()
    {
        return kitchenObj;
    }

    public void DestroyObjInstance(object sender, EventArgs e)
    {
        for (int i = 0; i < counterTopPoint.childCount; i++)
        {
            Destroy(counterTopPoint.GetChild(i).gameObject);
        }
    }
}
