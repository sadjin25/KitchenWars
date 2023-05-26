using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KitchenObjects", menuName = "ScriptableObjects/KitchenScriptableObject")]
public class KitchenObjectSO : ScriptableObject
{
    public GameObject obj;
    public Sprite sprite;
}
