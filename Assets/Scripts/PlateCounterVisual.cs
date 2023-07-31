using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] PlateCounter plateCounter;
    [SerializeField] Transform plateVisualPrefab;
    [SerializeField] Transform topPoint;
    List<GameObject> plateVisualGameObjectList;
    float plateOffset = 0.1f;

    void Awake()
    {
        plateVisualGameObjectList = new List<GameObject>();
    }

    void Start()
    {
        plateCounter.OnPlateSpawned += OnPlateSpawned;
        plateCounter.OnPlateRemoved += OnPlateRemoved;
    }

    void OnPlateSpawned(object s, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, topPoint);
        plateVisualTransform.localPosition = new Vector3(0, plateOffset * plateVisualGameObjectList.Count, 0);
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }

    void OnPlateRemoved(object s, System.EventArgs e)
    {
        GameObject plateObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateObject);
        Destroy(plateObject);
    }
}
