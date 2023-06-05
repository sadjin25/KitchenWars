using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectedInteractorVisual : MonoBehaviour
{
    [SerializeField] private IInteractor selectedObj;
    [SerializeField] private GameObject[] selectedVisualObjArr;

    void Start()
    {
        // Why not in Awake? -> Player Instance is may not initiallized.
        Player.Instance.OnSelectedInteractorChanged += OnSelectedKitchenChanged;
        selectedObj = this.GetComponentInParent<IInteractor>();
    }

    void OnSelectedKitchenChanged(object sender, Player.OnSelectedInteractorChangedEventArgs e)
    {
        if (e.selectedInteractor_ == selectedObj)
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
        for (int i = 0; i < selectedVisualObjArr.Length; i++)
        {
            selectedVisualObjArr[i].SetActive(true);
        }
    }

    void Hide()
    {
        for (int i = 0; i < selectedVisualObjArr.Length; i++)
        {
            selectedVisualObjArr[i].SetActive(false);
        }
    }
}
