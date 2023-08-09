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
            Player.Instance.GetKitchenObj().SetKitchenObjHolder(this);
            Player.Instance.ClearKitchenObj();
        }
        // if kitchen has obj, then swap with objOnHand
        else
        {
            if (objOnHand == null)
            {
                GetKitchenObj().SetKitchenObjHolder(Player.Instance);
                ClearKitchenObj();
            }
            else
            {
                PlateObject plateObject;
                if (objOnHand.TryGetPlate(out plateObject))
                {
                    if (!plateObject.TryAddIngredient(GetKitchenObj().GetKitchenObjectSO()))
                    {
                        return;
                    }
                    ClearKitchenObj();
                }
                else if (GetKitchenObj().TryGetPlate(out plateObject))
                {
                    if (!plateObject.TryAddIngredient(Player.Instance.GetKitchenObj().GetKitchenObjectSO()))
                    {
                        return;
                    }
                    Player.Instance.ClearKitchenObj();
                }
                else
                {
                    SwapObject(objOnHand);
                }
            }
        }
    }


}
