using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverResultUI : MonoBehaviour
{
    private const string IS_SHOW = "IsShow";
    [SerializeField] private Animator deliverySuccessUIAnimator;
    [SerializeField] private Animator deliveryFailUIAnimator;
    private void Start() {
        OrderManager.Instance.OnRecipeSuccessed += OrderManager_OnRecipeSuccessed;
        OrderManager.Instance.OnRecipeFailed += OrderManager_OnRecipeFailed;
    }
    private void OrderManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        ShowDeliveryFailUI();
    }
    private void OrderManager_OnRecipeSuccessed(object sender, System.EventArgs e)
    {
        ShowDeliverySuccessUI();
    }
    private void ShowDeliverySuccessUI()
    {
        deliverySuccessUIAnimator.gameObject.SetActive(true);
        deliverySuccessUIAnimator.SetTrigger(IS_SHOW);
    }
    private void ShowDeliveryFailUI()
    {
        deliveryFailUIAnimator.gameObject.SetActive(true);
        deliveryFailUIAnimator.SetTrigger(IS_SHOW);
    }
}
