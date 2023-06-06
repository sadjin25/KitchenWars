using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IInteractor
{
    public void Interact(KitchenObject objOnHand);

    public event EventHandler OnInteract;
}
