using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KitchenObjectHolder : MonoBehaviour
{
    public static event EventHandler OnDrop;
    public static event EventHandler OnPickup;

    [SerializeField] private Transform holdPoint;
    private KitchenObject kitchenObject;
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObject.GetKitchenObjectSO();
    }
    public bool IsHaveKitchenObject()
    {
        return kitchenObject != null;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        if (this is BaseCounter && this.kitchenObject != kitchenObject && kitchenObject != null)
        {
            OnDrop?.Invoke(this, EventArgs.Empty);
        }
        else if (this is Player && this.kitchenObject == kitchenObject && kitchenObject != null)
        {
            OnPickup?.Invoke(this, EventArgs.Empty);
        }
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
        SetKitchenObject(kitchenObject);
    }
    public void DestroyKitchenObject()
    {
        Destroy(kitchenObject.gameObject);
        ClearKitchenObject();
    }
    public void CreateKitchenObject(GameObject kitchenObjectPrefab)
    {
        KitchenObject kitchenObject = GameObject.Instantiate(kitchenObjectPrefab, GetHoldPoint()).GetComponent<KitchenObject>();
        SetKitchenObject(kitchenObject);
    }
    public static void ClearStaticData()
    {
        OnDrop = null;
        OnPickup = null;
    }
}
