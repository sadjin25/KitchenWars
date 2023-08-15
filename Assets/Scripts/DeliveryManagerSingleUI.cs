using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI recipeName;
    [SerializeField] Transform iconContainer;
    [SerializeField] Transform iconTemplate;

    void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeSO(RecipeSO recipeSO)
    {
        recipeName.text = recipeSO.recipeName;
        foreach (Transform child in iconContainer)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Transform newIcon = Instantiate(iconTemplate, iconContainer);
            newIcon.gameObject.SetActive(true);
            newIcon.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }
}
