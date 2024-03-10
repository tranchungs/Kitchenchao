using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler<OnDevilerySuccessEventArgs> OnDevilerySuccess;
    public event EventHandler OnDevileryFail;
    public class OnDevilerySuccessEventArgs : EventArgs
    {
        public int IndexrecipeSO;
    }
    public event EventHandler<OnSpawnRecipeEventArgs> OnSpawnRecipe;
    public class OnSpawnRecipeEventArgs : EventArgs
    {
        public RecipeSO recipeSO;
    }
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;
    public int successfulRecipesAmount { get; private set; }


    private void Awake()
    {
        Instance = this;


        waitingRecipeSOList = new List<RecipeSO>();
        
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            if(waitingRecipeSOList.Count< waitingRecipesMax)
            {
                spawnRecipeTimer = spawnRecipeTimerMax;
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];

                waitingRecipeSOList.Add(waitingRecipeSO);
                OnSpawnRecipe?.Invoke(this, new OnSpawnRecipeEventArgs { recipeSO = waitingRecipeSO });
            }
            
        }
    }
    public void DeliverRecipe(Plate plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                // Has the same number of ingredients
                bool plateContentsMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    // Cycling through all ingredients in the Recipe
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        // Cycling through all ingredients in the Plate
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            // Ingredient matches!
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        // This Recipe ingredient was not found on the Plate
                        plateContentsMatchesRecipe = false;
                        
                    }
                }

                if (plateContentsMatchesRecipe)
                {
                    // Player delivered the correct recipe!
                    successfulRecipesAmount++;
                    waitingRecipeSOList.RemoveAt(i);
                    //eventss
                    OnDevilerySuccess?.Invoke(this, new OnDevilerySuccessEventArgs { IndexrecipeSO = i});
                    return;
                }
            }
        }
        OnDevileryFail?.Invoke(this, EventArgs.Empty);

    }
    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetSuccessfulRecipesAmount()
    {
        return successfulRecipesAmount;
    }

}
