using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRecipeListSO fryingRecipeList;
    [SerializeField] private FryingRecipeListSO burningRecipeList;
    [SerializeField] private StoveCounterVisual stoveCounterVisual;
    [SerializeField] private ProgressBarUI progressBarUI;
    public enum StoveState
    {
        Idle,
        Frying,
        Buring
    }

    private float fryingTimer = 0;
    private FryingRecipe fryingRecipe;
    private StoveState state = StoveState.Idle;
    public override void Interact(Player player)
    {
        if (player.IsHaveKitchenObject())
        {
            //手上有食材
            if (IsHaveKitchenObject() == false)
            {
                if (fryingRecipeList.TryGetFryingRecipe(player.GetKitchenObject().GetKitchenObjectSO(), out FryingRecipe fryingRecipe))
                {
                    TransferKitchenObject(player, this);
                    StartFrying(fryingRecipe);
                }
                //这里out的是FryingRecipe，但其实能改成BurningRecipe最好
                else if (burningRecipeList.TryGetFryingRecipe(player.GetKitchenObject().GetKitchenObjectSO(), out FryingRecipe burningRecipe))
                {
                    TransferKitchenObject(player, this);
                    StartBuring(burningRecipe);
                }
                else
                {
                    Debug.LogWarning("无法获取食谱");
                }
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
                TurnToIdle();
                TransferKitchenObject(this, player);
            }
            else
            {
                //柜台上无食材
            }
        }
    }
    private void Update()
    {
        switch (state)
        {
            case StoveState.Idle:
                break;
            case StoveState.Frying:
                fryingTimer += Time.deltaTime;
                progressBarUI.UpdateProgress(fryingTimer / fryingRecipe.fryingTime);
                if (fryingTimer >= fryingRecipe.fryingTime)
                {
                    DestroyKitchenObject();
                    CreateKitchenObject(fryingRecipe.output.prefab);
                    state = StoveState.Buring;

                    burningRecipeList.TryGetFryingRecipe(GetKitchenObject().GetKitchenObjectSO(), out FryingRecipe nextFryingRecipe);
                    StartBuring(nextFryingRecipe);
                }
                break;
            case StoveState.Buring:
                fryingTimer += Time.deltaTime;
                progressBarUI.UpdateProgress(fryingTimer / fryingRecipe.fryingTime);
                if (fryingTimer >= fryingRecipe.fryingTime)
                {
                    DestroyKitchenObject();
                    CreateKitchenObject(fryingRecipe.output.prefab);
                    TurnToIdle();
                }
                break;
            default:
                break;
        }
    }
    private void StartFrying(FryingRecipe fryingRecipe)
    {
        fryingTimer = 0;
        this.fryingRecipe = fryingRecipe;
        state = StoveState.Frying;
        stoveCounterVisual.ShowStoveEffect();
    }
    private void StartBuring(FryingRecipe fryingRecipe)
    {
        if (fryingRecipe == null)
        {
            Debug.LogWarning("无法获取食谱");
            TurnToIdle();
            return;
        }
        fryingTimer = 0;
        this.fryingRecipe = fryingRecipe;
        state = StoveState.Buring;
        stoveCounterVisual.ShowStoveEffect();
    }
    private void TurnToIdle()
    {
        state = StoveState.Idle;
        stoveCounterVisual.HideStoveEffect();
        progressBarUI.Hide();
    }
}