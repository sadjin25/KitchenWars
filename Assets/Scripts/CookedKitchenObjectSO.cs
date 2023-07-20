using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KitchenObjects", menuName = "ScriptableObjects/CookedScriptableObject")]
public class CookedKitchenObjectSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public int cookTime;
}
