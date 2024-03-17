using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CuttingRecipe
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public int cuttingCountMax;
}
[CreateAssetMenu()]
public class CuttingRecipeListSO : ScriptableObject
{
    public List<CuttingRecipe> list;
    public KitchenObjectSO GetOutput(KitchenObjectSO input)
    {
        foreach (var recipe in list)
        {
            if (recipe.input == input)
            {
                return recipe.output;
            }
        }
        return null;
    }
    public bool TryGetCuttingRecipe(KitchenObjectSO input, out CuttingRecipe cuttingRecipe)
    {
        foreach (var recipe in list)
        {
            if (recipe.input == input)
            {
                cuttingRecipe = recipe;
                return true;
            }
        }
        cuttingRecipe = null;
        return false;
    }
}