using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [System.Serializable]
    public class KitchenObjectSO_Model{
        public KitchenObjectSO kitchenObjectSO;
        public GameObject modol;
    }
    [SerializeField] private List<KitchenObjectSO_Model> modelMap;
    public void ShowKitchenObject(KitchenObjectSO kitchenObjectSO){
        foreach (var item in modelMap)
        {
            if(item.kitchenObjectSO == kitchenObjectSO){
                item.modol.SetActive(true);
                return;
            }
        }
    }
}
