using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI deliveredCountText;

    void Start()
    {
        GameManager.Instance.OnStateChanged += OnStateChanged;
        gameObject.SetActive(false);
    }

    void OnStateChanged(object s, EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            deliveredCountText.text = DeliveryManager.Instance.GetDeliveredCount().ToString();
            Show();
        }
        else
        {
            Hide();
        }
    }

    void Show()
    {
        gameObject.SetActive(true);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
