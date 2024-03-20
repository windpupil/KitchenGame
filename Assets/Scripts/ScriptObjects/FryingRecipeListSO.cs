using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeListSO : ScriptableObject
{
    public List<FryingRecipe> list;
    public bool TryGetFryingRecipe(KitchenObjectSO input, out FryingRecipe fryingRecipe)
    {
        foreach (var recipe in list)
        {
            if (recipe.input == input)
            {
                fryingRecipe = recipe;
                return true;
            }
        }
        fryingRecipe = null;
        return false;
    }
}
[System.Serializable]
public class FryingRecipe
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float fryingTime;
}