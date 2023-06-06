using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Kitchen : KitchenObjHolder, IInteractor
{
    public event EventHandler OnInteract;

    void Awake()
    {
        KitchenObjHolderInit();
    }


    public void Interact(KitchenObject objOnHand)
    {
        //WARNING : always write SetKitchenObj before Clear() other side!
        OnInteract?.Invoke(this, EventArgs.Empty);

        // when kitchen is empty, then get the item from objOnHand
        if (!HasKitchenObj() && objOnHand != null)
        {
            SetKitchenObj(objOnHand);
            Player.Instance.ClearKitchenObj();
        }
        // if kitchen has obj, then swap with objOnHand
        else
        {
            if (objOnHand == null)
            {
                Player.Instance.SetKitchenObj(kitchenObj);
                ClearKitchenObj();
            }
            else
            {
                // WARNING : Please! Deep Copy this!
                KitchenObject kitchenObjectTemp = kitchenObj.Clone();

                SetKitchenObj(objOnHand);
                Player.Instance.ClearKitchenObj();
                Player.Instance.SetKitchenObj(kitchenObjectTemp);
            }
        }
    }


}
