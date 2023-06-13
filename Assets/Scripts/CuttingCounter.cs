using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CuttingCounter : KitchenObjHolder, IInteractor
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
        // if kitchen has obj
        else
        {
            if (objOnHand == null)
            {
                if (IsMyObjCutted())
                {
                    Player.Instance.SetKitchenObj(kitchenObj);
                    ClearKitchenObj();
                }
                else
                {
                    CuttingAndUpdateObj();
                }
            }
            // if player has objOnHand; can't cut!
            else
            {
                Debug.LogError("Player couldn't cut it because he has something in hand");
            }
        }

    }


    void CuttingAndUpdateObj()
    {
        if (IsMyObjCutted()) return;

        KitchenObject cuttedObj = kitchenObj.GetCuttedObj();
        SetKitchenObj(cuttedObj);
    }
}
