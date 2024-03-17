using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player.IsHaveKitchenObject())
        {
            //手上有食材
            if (IsHaveKitchenObject() == false)
            {
                //柜台上无食材
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
            }
            else
            {
                //柜台上无食材
            }
        }
    }

}
