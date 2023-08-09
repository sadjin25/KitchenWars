using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateObject : KitchenObject
{
    [SerializeField] List<KitchenObjectSO> validKitchenObjectSOList;
    List<KitchenObjectSO> kitchenObjectSOList;

    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

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
        if (!validKitchenObjectSOList.Contains(inputSO))
        {
            return false;
        }
        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs { kitchenObjectSO = inputSO });
        kitchenObjectSOList.Add(inputSO);
        return true;
    }
}
