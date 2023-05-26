using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KitchenObject : MonoBehaviour
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;

    [SerializeField] Kitchen kitchen;

    public Kitchen GetKitchen()
    {
        return kitchen;
    }

    public void SetKitchen(Kitchen kitchenToSet)
    {
        if (kitchenToSet == null)
        {
            Debug.LogError("Kitchen is NULL!");
            return;
        }

        kitchenToSet.ClearKitchenObj();

        kitchen = kitchenToSet;
        kitchen.SelectKitchenObj(this);
        transform.parent = kitchen.GetKitchenTopTransform();    // transform parent? : Kitchen Object Instance.
        transform.localPosition = Vector3.zero;
    }

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
}
