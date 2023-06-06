using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KitchenContainer : KitchenObjHolder, IInteractor
{
    // WARNING : Container has useless field(top point transform).
    public event EventHandler OnInteract;

    public void Interact(KitchenObject kitchenObj_)
    {
        OnInteract?.Invoke(this, EventArgs.Empty);
        // If Player has item, do nothing. else, give item to player.
        if (kitchenObj_)
        {
            Debug.LogError("Player already has an object on hand!");
        }

        else
        {
            Player.Instance.SetKitchenObj(kitchenObj);
        }
    }
}
