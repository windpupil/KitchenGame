using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//仓库类柜台
public class ContainerCounter : BaseCounter
{
    [SerializeField] private ContainerCounterVisual containerCounterVisual;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    // private void Start()
    // {
    //     containerCounterVisual = GetComponentInChildren<ContainerCounterVisual>();
    // }

    public override void Interact(Player player)
    {
        if (player.IsHaveKitchenObject())
            return;
        CreateKitchenObject(kitchenObjectSO.prefab);
        TransferKitchenObject(this, player);
        containerCounterVisual.PlayOpen();

    }
}
