using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeliveryCounter : KitchenObjHolder, IInteractor
{
    public event EventHandler OnInteract;

    void Awake()
    {
        KitchenObjHolderInit();
    }

    public void Interact(KitchenObject objOnHand)
    {
        OnInteract?.Invoke(this, EventArgs.Empty);
        if (objOnHand != null)
        {
            if (objOnHand.TryGetPlate(out PlateObject plateObject))
            {
                DeliveryManager.Instance.DeliveryRecipe(plateObject);
                Player.Instance.ClearKitchenObj();
            }
        }
    }
}
