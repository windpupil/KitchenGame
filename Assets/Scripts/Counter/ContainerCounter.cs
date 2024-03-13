using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//仓库类柜台
public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (player.IsHaveKitchenObject())
            return;
        CreateKitchenObject(kitchenObjectSO.prefab);
        TransferKitchenObject(this, player);

    }
    public void CreateKitchenObject(GameObject kitchenObjectPrefab)
    {
        KitchenObject kitchenObject = GameObject.Instantiate(kitchenObjectSO.prefab, GetHoldPoint()).GetComponent<KitchenObject>();
        SetKitchenObject(kitchenObject);
    }
}
