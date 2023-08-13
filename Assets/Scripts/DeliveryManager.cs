using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] RecipeListSO recipeListSO;
    List<RecipeSO> waitingRecipeSOList;

    float spawnRecipeTimer;
    readonly float spawnRecipeTimerMax = 4f;
    readonly int spawnRecipesMax = 4;

    void Awake()
    {
        Instance = this;

        waitingRecipeSOList = new List<RecipeSO>();
    }

    void Update()
    {
        spawnRecipeTimer += Time.deltaTime;
        if (spawnRecipeTimer >= spawnRecipeTimerMax)
        {
            spawnRecipeTimer = 0f;

            if (waitingRecipeSOList.Count >= spawnRecipesMax)
            {
                return;
            }
            RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
            waitingRecipeSOList.Add(waitingRecipeSO);
        }
    }

    public void DeliveryRecipe(PlateObject plateObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            bool plateContentsMatchesRecipe = true;
            if (waitingRecipeSO.kitchenObjectSOList.Count == plateObject.GetKitchenObjectSOList().Count)
            {
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateObjectSO in plateObject.GetKitchenObjectSOList())
                    {
                        if (recipeKitchenObjectSO == plateObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        plateContentsMatchesRecipe = false;
                    }
                }
            }
            if (plateContentsMatchesRecipe)
            {
                waitingRecipeSOList.RemoveAt(i);
                return;
            }
        }
    }
}
