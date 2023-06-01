using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITable
{
    public void SelectKitchenObj(KitchenObject obj);
    public void ClearKitchenObj();
    public bool HasKitchenObj();

    public KitchenObject GetKitchenObj();
    public Transform GetKitchenTopTransform();
    public bool HasKitchenObject();
}
