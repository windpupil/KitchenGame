using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : KitchenObjectHolder
{
    [SerializeField] private GameObject SelectedCounter;
    public virtual void Interact(Player player)
    {
        // if (GetKitchenObject() == null)
        // {
        //     KitchenObject kitchenObject = GameObject.Instantiate(kitchenObjectSO.prefab, GetHoldPoint()).GetComponent<KitchenObject>();
        //     SetKitchenObject(kitchenObject);
        // }
        // else
        // {
        //     TransferKitchenObject(this, Player.Instance);
        // }
        Debug.LogWarning("交互方法未重写");
    }
    public virtual void InteractOperate(Player player)
    {
        // Debug.LogWarning("操作方法未重写");
    }
    public void SelectCounter()
    {
        SelectedCounter.SetActive(true);
    }
    public void CancelSelect()
    {
        SelectedCounter.SetActive(false);
    }

}
