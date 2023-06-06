using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrashCounter : MonoBehaviour, IInteractor
{
    public event EventHandler OnInteract;

    public void Interact(KitchenObject objOnHand)
    {
        if (objOnHand == null)
        {
            return;
        }

        Player.Instance.ClearKitchenObj();
    }
}
