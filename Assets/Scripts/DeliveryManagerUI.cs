using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] Transform container;
    [SerializeField] Transform recipeTemplate;

    void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += OnRecipeCompleted;

        UpdateVisual();
    }

    void OnRecipeSpawned(object s, EventArgs e)
    {
        UpdateVisual();
    }

    void OnRecipeCompleted(object s, EventArgs e)
    {
        UpdateVisual();
    }

    void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            List<KitchenObjectSO> kitchenObjectSOList = recipeSO.kitchenObjectSOList;
            string recipeName = recipeSO.recipeName;
            Transform newUI = Instantiate(recipeTemplate, container);
            newUI.gameObject.SetActive(true);
            newUI.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }
    }
}
