using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectedInteractorVisual : MonoBehaviour
{
    [SerializeField] private Kitchen selectedObj;
    [SerializeField] private GameObject selectedVisualObj;

    void Start()
    {
        // Why not in Awake? -> Player Instance is may not initiallized.
        Player.Instance.OnSelectedInteractorChanged += OnSelectedKitchenChanged;
    }

    void OnSelectedKitchenChanged(object sender, Player.OnSelectedInteractorChangedEventArgs e)
    {
        if (e.selectedInteractor_ == (IInteractor)selectedObj)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    void Show()
    {
        selectedVisualObj.SetActive(true);
    }

    void Hide()
    {
        selectedVisualObj.SetActive(false);
    }
}
