using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform recipeTemplate;
    [SerializeField] private Transform container;
    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }
    void Start()
    {
        DeliveryManager.Instance.OnSpawnRecipe += Instance_OnSpawnRecipe;
        DeliveryManager.Instance.OnDevilerySuccess += Instance_OnDevilerySuccess;
        UpdateVisual();
    }

    private void Instance_OnDevilerySuccess(object sender, DeliveryManager.OnDevilerySuccessEventArgs e)
    {
        UpdateVisual();
    }

    private void Instance_OnSpawnRecipe(object sender, DeliveryManager.OnSpawnRecipeEventArgs e)
    {

        UpdateVisual();

    }
    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<RecipeTemplateUI>().SetUI(recipeSO);
        }
    }

}
