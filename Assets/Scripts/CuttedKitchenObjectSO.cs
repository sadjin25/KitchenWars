using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KitchenObjects", menuName = "ScriptableObjects/CuttedScriptableObject")]
public class CuttedKitchenObjectSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public int cuttingCnt;
}
