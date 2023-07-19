using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CuttingCounter : KitchenObjHolder, IInteractor
{
    public event EventHandler OnInteract;
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }

    [SerializeField] KitchenObjectSO[] cuttableArray;

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
                if (IsMyObjCutted() || CheckCuttable(kitchenObj) == false)
                {
                    // ERROR : delete all the progresses of cut.
                    Player.Instance.SetKitchenObj(kitchenObj);
                    ClearKitchenObj();
                }
                else
                {
                    CuttingAndUpdateObj();
                    if (kitchenObj.GetKitchenObjectSO().cuttingCnt > 0)
                    {
                        OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                        {
                            progressNormalized = (float)kitchenObj.GetCuttedCount() / kitchenObj.GetKitchenObjectSO().cuttingCnt
                        });
                    }
                    else
                    {
                        OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                        {
                            progressNormalized = 1f
                        });
                    }
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

        if (kitchenObj.CutAndCheckCutted())
        {
            SetKitchenObj(kitchenObj.GetCuttedObj());
            Debug.Log("Set!");
        }
    }

    bool CheckCuttable(KitchenObject objOnTable)
    {
        KitchenObjectSO inputSO = objOnTable.GetKitchenObjectSO();
        foreach (KitchenObjectSO cuttable in cuttableArray)
        {
            if (cuttable == inputSO)
            {
                return true;
            }
        }
        return false;
    }
}
