using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeliveryCounter : KitchenObjHolder, IInteractor
{
    public static DeliveryCounter Instance { get; private set; }
    public event EventHandler OnInteract;

    void Awake()
    {
        Instance = this;
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
