using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CuttingCounter : KitchenObjHolder, IInteractor, IHasProgress
{
    public event EventHandler OnInteract;
    public event EventHandler OnCut;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    [SerializeField] CuttedKitchenObjectSO[] cuttableArray;

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
                if (IsCuttable(kitchenObj.GetKitchenObjectSO()) == false)
                {
                    // ERROR : delete all the progresses of cut.
                    Player.Instance.SetKitchenObj(kitchenObj);
                    ClearKitchenObj();
                }
                else
                {
                    CuttingAndUpdateObj();
                    OnCut?.Invoke(this, EventArgs.Empty);
                    if (kitchenObj.GetCuttedCount() > 0)
                    {
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = (float)kitchenObj.GetCuttedCount() / kitchenObj.GetCuttedSO().cuttingCnt
                        });
                    }
                    else
                    {
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 1f
                        });
                    }
                }
            }
            // if player has objOnHand; can't cut!
            // ERROR : can't swap the item
            else
            {
                Debug.LogError("Player couldn't cut it because he has something in hand");
            }
        }
    }

    void CuttingAndUpdateObj()
    {
        if (kitchenObj.CutAndCheckCutted())
        {
            KitchenObjectSO outputSO = GetOutputFromInput(kitchenObj.GetKitchenObjectSO());
            if (outputSO == null) return;

            SetKitchenObj(outputSO);
        }
    }

    bool IsCuttable(KitchenObjectSO input)
    {
        foreach (CuttedKitchenObjectSO cuttable in cuttableArray)
        {
            if (cuttable.input == input)
            {
                return true;
            }
        }
        return false;
    }

    KitchenObjectSO GetOutputFromInput(KitchenObjectSO input)
    {
        foreach (CuttedKitchenObjectSO cuttable in cuttableArray)
        {
            if (cuttable.input == input)
            {
                return cuttable.output;
            }
        }
        return null;
    }

}
