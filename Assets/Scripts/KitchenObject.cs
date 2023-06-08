using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;
    [SerializeField] KitchenObjectSO cuttedKitchenObjectSO;

    [SerializeField] KitchenObjHolder kitchen;

    public KitchenObject Clone()
    {
        // For Deep Copying
        KitchenObject toReturn = new KitchenObject();
        toReturn.kitchenObjectSO = this.kitchenObjectSO;
        toReturn.kitchen = this.kitchen;
        return toReturn;
    }

    public KitchenObjHolder GetKitchenObjHolder()
    {
        return kitchen;
    }

    public void SetKitchenObjHolder(KitchenObjHolder kitchenToSet)
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

    public KitchenObject GetCuttedObj()
    {
        KitchenObject cutted = new KitchenObject();
        cutted.kitchenObjectSO = cuttedKitchenObjectSO;
        cutted.kitchen = this.kitchen;
        return cutted;
    }
}
