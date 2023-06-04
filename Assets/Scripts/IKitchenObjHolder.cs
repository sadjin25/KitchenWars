using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IKitchenObjHolder
{
    public void SetKitchenObj(KitchenObject obj);
    public void ClearKitchenObj();
    public bool HasKitchenObj();
    public KitchenObject GetKitchenObj();
    public Transform GetKitchenObjLocation();

    public event EventHandler OnObjChanging;
    public void DestroyObjInstance(object sender, EventArgs e);
}
