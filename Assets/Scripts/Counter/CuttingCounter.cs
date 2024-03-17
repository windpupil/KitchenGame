using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeListSO cuttingRecipeList;
    [SerializeField] private ProgressBarUI progressBarUI;
    [SerializeField]private CuttingCounterVisual cuttingCounterVisual;
    private int cuttingCount = 0;
    public override void Interact(Player player)
    {
        if (player.IsHaveKitchenObject())
        {
            //手上有食材
            if (IsHaveKitchenObject() == false)
            {
                //柜台上无食材
                cuttingCount = 0;
                TransferKitchenObject(player, this);
            }
            else
            {
                //柜台上有食材
            }
        }
        else
        {
            //手上无食材
            if (IsHaveKitchenObject())
            {
                //柜台上有食材
                TransferKitchenObject(this, player);
                progressBarUI.Hide();
            }
            else
            {
                //柜台上无食材
            }
        }
    }
    public override void InteractOperate(Player player)
    {
        if (IsHaveKitchenObject())
        {
            if (cuttingRecipeList.TryGetCuttingRecipe(GetKitchenObject().GetKitchenObjectSO(), out CuttingRecipe cuttingRecipe))
            {
                Cut();
                progressBarUI.UpdateProgress((float)cuttingCount / cuttingRecipe.cuttingCountMax);
                if (cuttingCount == cuttingRecipe.cuttingCountMax)
                {
                    DestroyKitchenObject();
                    CreateKitchenObject(cuttingRecipe.output.prefab);
                }
            }
        }
    }
    private void Cut()
    {
        cuttingCount++;
        cuttingCounterVisual.PlayCut();
    }
}