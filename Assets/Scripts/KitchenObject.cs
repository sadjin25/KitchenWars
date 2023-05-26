using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;

    Kitchen kitchen;

    public Kitchen GetKitchen()
    {
        return kitchen;
    }

    public void SetKitchen(Kitchen kitchenToSet)
    {
        if (kitchenToSet != null)
        {
            kitchen.ClearKitchenObj();
            kitchen = kitchenToSet;
            kitchen.SelectKitchenObj(this);
            transform.parent = kitchen.GetKitchenTopTransform();
            transform.localPosition = Vector3.zero;
        }
    }

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
}
