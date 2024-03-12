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
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        kitchenObject.transform.localPosition = Vector3.zero;
    }
    public Transform GetHoldPoint()
    {
        return holdPoint;
    }
    public void TransferKitchenObject(ClearCounter sourceCounter, ClearCounter targetCounter)
    {
        if (sourceCounter.kitchenObject != null)
        {
            if (targetCounter.GetKitchenObject() == null)
            {
                targetCounter.AddKitchenObject(sourceCounter.GetKitchenObject());
                sourceCounter.ClearKitchenObject();
            }
            else
            {
                Debug.LogWarning("目标位置已经有物品了");
            }
        }
        else
        {
            Debug.LogWarning("源位置没有物品");
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
