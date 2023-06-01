using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : MonoBehaviour, ITable, IInteractor
{
    [SerializeField] KitchenObject kitchenObj;

    [SerializeField] private Transform counterTopPoint;

    public void Interact(KitchenObject objOnHand)
    {
        // 키친이 비면 받은 아이템 가지기
        if (!HasKitchenObj())
        {
            GameObject o = Instantiate(objOnHand.GetKitchenObjectSO().prefab, counterTopPoint);
            o.GetComponent<KitchenObject>().SetKitchen(this);
            kitchenObj = o.GetComponent<KitchenObject>();
            Player.Instance.RemoveObjOnHand();
        }

        else
        {
            Debug.Log(kitchenObj.GetKitchen());
        }
    }

    public Transform GetKitchenTopTransform()
    {
        return counterTopPoint;
    }

    public void SelectKitchenObj(KitchenObject obj)
    {
        kitchenObj = obj;
    }

    public void ClearKitchenObj()
    {
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

    public bool HasKitchenObject()
    {
        return kitchenObj != null;
    }
}
