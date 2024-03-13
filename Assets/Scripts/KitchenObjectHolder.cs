using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObjectHolder : MonoBehaviour
{
    [SerializeField] private Transform holdPoint;
    private KitchenObject kitchenObject;
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public bool IsHaveKitchenObject()
    {
        return kitchenObject != null;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        kitchenObject.transform.localPosition = Vector3.zero;
    }
    public Transform GetHoldPoint()
    {
        return holdPoint;
    }
    public void TransferKitchenObject(KitchenObjectHolder sourceHolder, KitchenObjectHolder targetHolder)
    {
        if (sourceHolder.kitchenObject != null)
        {
            if (targetHolder.GetKitchenObject() == null)
            {
                targetHolder.AddKitchenObject(sourceHolder.GetKitchenObject());
                sourceHolder.ClearKitchenObject();
            }
            else
            {
                Debug.LogWarning("目标持有者已经有物品了");
            }
        }
        else
        {
            Debug.LogWarning("源持有者没有物品");
        }
    }
    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
    }
    public void AddKitchenObject(KitchenObject kitchenObject)
    {
        kitchenObject.transform.SetParent(holdPoint);
        kitchenObject.transform.localPosition = Vector3.zero;
        this.kitchenObject = kitchenObject;
    }
}
