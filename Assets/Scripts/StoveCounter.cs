using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoveCounter : KitchenObjHolder, IInteractor
{
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    public event EventHandler OnInteract;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    [SerializeField] CookedKitchenObjectSO[] cookableSOArray;
    float cookTimer;
    State state;
    CookedKitchenObjectSO cookedSO;

    void Awake()
    {
        KitchenObjHolderInit();
        state = State.Idle;
    }

    void Update()
    {
        if (HasKitchenObj() && IsCookable(kitchenObj.GetKitchenObjectSO()))
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    cookTimer += Time.deltaTime;

                    if (cookTimer >= cookedSO.cookTime)
                    {
                        cookTimer = 0f;
                        CookAndUpdateObj();
                        state = State.Fried;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                    }
                    break;

                case State.Fried:
                    cookTimer += Time.deltaTime;

                    if (cookTimer >= cookedSO.cookTime)
                    {
                        cookTimer = 0f;
                        CookAndUpdateObj();
                        state = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                    }
                    break;

                case State.Burned:
                    break;
            }

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
            state = State.Frying;
            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
            cookTimer = 0f;
            cookedSO = kitchenObj.GetCookedSO();
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
