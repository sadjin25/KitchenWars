using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateCounter : KitchenObjHolder, IInteractor
{
    public event EventHandler OnInteract;
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] KitchenObjectSO plateKitchenObjectSO;

    float spawnPlateTimer;
    readonly float spawnPlateTimeMax = 3f;
    int spawnedPlateCnt;
    readonly float spawnedPlateCntMax = 5f;
    void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer >= spawnPlateTimeMax)
        {
            spawnPlateTimer = 0f;
            if (spawnedPlateCnt >= spawnedPlateCntMax) return;
            spawnedPlateCnt++;
            OnPlateSpawned?.Invoke(this, EventArgs.Empty);
        }
    }

    public void Interact(KitchenObject kitchenObj_)
    {
        OnInteract?.Invoke(this, EventArgs.Empty);

        if (kitchenObj_)
        {
            return;
        }

        if (spawnedPlateCnt > 0)
        {
            spawnedPlateCnt--;
            OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            Player.Instance.SetKitchenObj(plateKitchenObjectSO);
        }
    }
}
