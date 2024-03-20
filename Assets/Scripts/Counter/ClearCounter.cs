using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player.IsHaveKitchenObject())
        {
            //手上有kitchenObject
            if (player.GetKitchenObject().TryGetComponent<PlateKitchenObject>(out PlateKitchenObject plateKitchenObject))
            {//手上有盘子
                if (IsHaveKitchenObject() == false)
                {
                    //柜台上无食材
                    TransferKitchenObject(player, this);
                }
                else
                {
                    //柜台上有食材
                    bool isSuccessed = plateKitchenObject.AddKitchenObjectSO(GetKitchenObjectSO());
                    if (isSuccessed)
                    DestroyKitchenObject();

                }
            }
            else
            {
                if (IsHaveKitchenObject() == false)
                {
                    //柜台上无食材
                    TransferKitchenObject(player, this);
                }
                else
                {
                    //柜台上有食材
                    if(GetKitchenObject().TryGetComponent<PlateKitchenObject>(out plateKitchenObject))
                    {
                        if(plateKitchenObject.AddKitchenObjectSO(player.GetKitchenObjectSO()))
                        {
                            player.DestroyKitchenObject();
                        }
                    }
                }
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
