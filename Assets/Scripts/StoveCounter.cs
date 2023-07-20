using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoveCounter : KitchenObjHolder, IInteractor
{
    public event EventHandler OnInteract;

    [SerializeField] CookedKitchenObjectSO[] cookableSOArray;
    float cookTimer;

    void Awake()
    {
        KitchenObjHolderInit();
    }

    void Update()
    {
        if (HasKitchenObj() && IsCookable(kitchenObj.GetKitchenObjectSO()))
        {
            cookTimer += Time.deltaTime;
            CookedKitchenObjectSO cookedSO = kitchenObj.GetCookedSO();

            if (cookTimer >= cookedSO.cookTime)
            {
                cookTimer = 0f;
                CookAndUpdateObj();
            }
        }
        else
        {
            cookTimer = 0f;
        }
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
                // ERROR : delete all the progresses of cut.
                Player.Instance.SetKitchenObj(kitchenObj);
                ClearKitchenObj();

            }
            // if player has objOnHand; can't cook!
            else
            {
                Debug.LogError("Player couldn't cut it because he has something in hand");
            }
        }
    }

    void CookAndUpdateObj()
    {
        KitchenObjectSO outputSO = GetOutputFromInput(kitchenObj.GetKitchenObjectSO());
        if (outputSO == null) return;

        SetKitchenObj(outputSO);
    }

    bool IsCookable(KitchenObjectSO input)
    {
        foreach (CookedKitchenObjectSO cookable in cookableSOArray)
        {
            if (cookable.input == input)
            {
                return true;
            }
        }
        return false;
    }

    KitchenObjectSO GetOutputFromInput(KitchenObjectSO input)
    {
        foreach (CookedKitchenObjectSO cookable in cookableSOArray)
        {
            if (cookable.input == input)
            {
                return cookable.output;
            }
        }
        return null;
    }

}
