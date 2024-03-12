using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : KitchenObjectHolder
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float rotateSpeed = 20;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    private bool isWalking = false;
    private ClearCounter selectedCounter;
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void Update()
    {
        HandleInteraction();
    }
    void FixedUpdate()
    {
        HandleMovement();
    }
    public bool IsWalking()
    {
        return isWalking;
    }
    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        selectedCounter?.Interact();
    }
    private void HandleMovement()
    {
        Vector3 direction = gameInput.GetMovementDirectionNormalized();
        transform.position += direction * Time.deltaTime * moveSpeed;
        isWalking = false;
        if (direction != Vector3.zero)
        {
            isWalking = true;
            Vector3.Slerp(transform.forward, direction, Time.deltaTime * rotateSpeed);//方向插值用slerp，坐标插值用lerp
            transform.forward = direction;
        }
    }
    private void HandleInteraction()//处理交互
    {
        RaycastHit hitinfo;
        bool isCollide = Physics.Raycast(transform.position, transform.forward, out hitinfo, 2, counterLayerMask);
        if (isCollide)
        {
            if (hitinfo.collider.TryGetComponent<ClearCounter>(out ClearCounter counter))
            {
                // counter.Interact();
                SetSelectedCounter(counter);
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }
    public void SetSelectedCounter(ClearCounter counter)//设置选中的counter
    {
        if (counter != selectedCounter)
        {
            selectedCounter?.CancelSelect();
            counter?.SelectCounter();
            selectedCounter = counter;
        }
    }
}
