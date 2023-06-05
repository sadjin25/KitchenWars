using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Kitchen : KitchenObjHolder, IInteractor
{
    void Awake()
    {
        KitchenObjHolderInit();
    }


    public void Interact(KitchenObject objOnHand)
    {
        //WARNING : always write SetKitchenObj before Clear() other side!
        // ? : Copying Component goes wrong.. maybe? 
        // 키친이 비면 받은 아이템 가지기
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
