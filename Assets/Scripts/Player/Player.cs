using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour,IKitchenObjectParten
{
    public static Player Instance { get; private set; }
    public event EventHandler<OnSelectedCounterChangeEventArgs> OnSelectedCounterChange;

    public class OnSelectedCounterChangeEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    // Start is called before the first frame update
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask couterLayerMask;
    [SerializeField] private Transform clearCounterTopPoint;
    private BaseCounter selectedCounter;

    private Vector3 lastInteractDir;
    private bool isWalking;
    private KitchenObject kitchenObject;

    // Update is called once per frame
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Another Player Instance");
        }
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteract;
        gameInput.OnInteractAlternate += GameInput_OnInteractAlternate;
    }

    private void GameInput_OnInteractAlternate(object sender, EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }


    }

    private void GameInput_OnInteract(object sender, System.EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    void Update()
    {
        HanldeMovement();
        HanldeInteractions();
    }
    public bool IsWalking()
    {
        return isWalking;
    }
   
    private void HanldeMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        /* bắn ra raycast nếu gặp vật cản thì không thể đi
         * Raycast Capsule từ vị trí Player, và phía trên đầu, theo hướng di chuyển và bắn ra bao nhiêu distance
         */
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        if (canMove)
        {
            transform.position += (Vector3)moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed); // quay  theo thời gian 
    }
    private void HanldeInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }
        float maxDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, maxDistance, couterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent<BaseCounter>(out BaseCounter selectedCounterHit))
            {
        
                SetSelectedCounter(selectedCounterHit);
             
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
    private void SetSelectedCounter(BaseCounter clearCounter)
    {
        this.selectedCounter = clearCounter;
        OnSelectedCounterChange?.Invoke(this, new OnSelectedCounterChangeEventArgs { selectedCounter = selectedCounter });
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return clearCounterTopPoint;
    }
    public KitchenObject GetKitchenObject()
    {
        return this.kitchenObject;
    }
    public void SetKitchenObject(KitchenObject kitchenObj)
    {
        this.kitchenObject = kitchenObj;
    }
    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return this.kitchenObject != null;
    }

}
