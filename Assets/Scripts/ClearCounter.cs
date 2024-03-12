using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : KitchenObjectHolder
{
    [SerializeField] private GameObject SelectedCounter;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private bool testing = false;
    [SerializeField] private ClearCounter tranferTargetCounter;




    private void Update()
    {
        if (testing && Input.GetMouseButtonDown(0))
        {
            TransferKitchenObject(this, tranferTargetCounter);
        }
    }



    public void Interact()
    {
        if (GetComponent<KitchenObject>() == null)
        {
            KitchenObject kitchenObject = GameObject.Instantiate(kitchenObjectSO.prefab, GetHoldPoint()).GetComponent<KitchenObject>();
            SetKitchenObject(kitchenObject);
        }
        else
        {
            Debug.LogWarning(" 已经有东西了" );
        }
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
