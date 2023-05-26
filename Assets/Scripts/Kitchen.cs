using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : MonoBehaviour, IInteractor
{
    [SerializeField] KitchenObject kitchenObj;
    [SerializeField] KitchenObjectSO koso;

    [SerializeField] private Transform counterTopPoint;

    public void Interact()
    {
        if (kitchenObj == null)
        {
            // TODO : koso를 Player에서 받아서 할 것.
            GameObject kitchenObjNew = Instantiate(koso.obj, counterTopPoint);
            kitchenObj = kitchenObjNew.GetComponent<KitchenObject>();

            kitchenObj.SetKitchen(this);
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
}
