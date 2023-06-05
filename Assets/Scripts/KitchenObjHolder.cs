using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class KitchenObjHolder : MonoBehaviour
{
    [SerializeField] protected KitchenObject kitchenObj;

    [SerializeField] protected Transform objTransform;

    public event EventHandler OnObjChanging;

    public virtual void KitchenObjHolderInit()
    {
        OnObjChanging += DestroyObjInstance;
        if (kitchenObj)
        {
            SetKitchenObj(kitchenObj);
        }
    }

    public virtual Transform GetKitchenObjLocation()
    {
        return objTransform;
    }

    public virtual void SetKitchenObj(KitchenObject obj)
    {
        OnObjChanging?.Invoke(this, EventArgs.Empty);

        GameObject o = Instantiate(obj.GetKitchenObjectSO().prefab, objTransform);
        o.GetComponent<KitchenObject>().SetKitchenObjHolder(this);
        kitchenObj = o.GetComponent<KitchenObject>();
    }

    public virtual void ClearKitchenObj()
    {
        OnObjChanging?.Invoke(this, EventArgs.Empty);
        kitchenObj = null;
    }

    public virtual bool HasKitchenObj()
    {
        return kitchenObj != null;
    }

    public virtual KitchenObject GetKitchenObj()
    {
        return kitchenObj;
    }

    public virtual void DestroyObjInstance(object sender, EventArgs e)
    {
        for (int i = 0; i < objTransform.childCount; i++)
        {
            Destroy(objTransform.GetChild(i).gameObject);
        }
    }
}
