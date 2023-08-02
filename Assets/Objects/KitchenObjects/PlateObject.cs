using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateObject : KitchenObject
{
    [SerializeField] List<KitchenObjectSO> validKitchenObjectSOList;
    List<KitchenObjectSO> kitchenObjectSOList;

    void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO inputSO)
    {
        if (kitchenObjectSOList.Contains(inputSO))
        {
            return false;
        }
        if (validKitchenObjectSOList.Contains(inputSO))
        {
            kitchenObjectSOList.Add(inputSO);
            return true;
        }
        return false;
    }
}
