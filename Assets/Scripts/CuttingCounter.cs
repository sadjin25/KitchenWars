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
            objOnHand.SetKitchenObjHolder(this);
            Player.Instance.ClearKitchenObj();
        }
        // if kitchen has obj
        else
        {
            if (objOnHand == null)
            {
                if (IsCuttable(kitchenObj.GetKitchenObjectSO()) == false)
                {
                    GetKitchenObj().SetKitchenObjHolder(Player.Instance);
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
            else
            {
                if (objOnHand.TryGetPlate(out PlateObject plateObject))
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
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = 1f
                    });
                }
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
