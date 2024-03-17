using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    // [SerializeField] private StoveVisual stoveVisual;
    public override void Interact(Player player)
    {
        // if (player.IsHaveKitchenObject())
        // {
        //     KitchenObjectSO kitchenObject = player.GetKitchenObject();
        //     if (kitchenObject is FoodSO)
        //     {
        //         FoodSO food = kitchenObject as FoodSO;
        //         stoveVisual.SetFood(food);
        //         player.DestroyKitchenObject();
        //     }
        // }
    }
}