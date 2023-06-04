using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KitchenObject : MonoBehaviour
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;

    [SerializeField] IKitchenObjHolder kitchen;

    public IKitchenObjHolder GetKitchenObjHolder()
    {
        return kitchen;
    }

    public void SetKitchenObjHolder(IKitchenObjHolder kitchenToSet)
    {
        if (kitchenToSet == null)
        {
            Debug.LogError("Kitchen is NULL!");
            return;
        }
        kitchen = kitchenToSet;
        transform.parent = kitchen.GetKitchenObjLocation();    // transform parent? : Kitchen Object Instance.
        transform.localPosition = Vector3.zero;
    }

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
}
