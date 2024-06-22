using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public SO_Item deneme;

    [SerializeField] private float _speed = 0.1f;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _headTransform;

    public Transform _handPos;

    [SerializeField] private LayerMask _interactable;

    public Item _itemInHand;
    private Rigidbody _rbPlayer;
    private Vector2 _moveInput;
    private Vector3 move;

    private InputActionMap playerMap;
    private InputActionMap uiMap;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        Application.targetFrameRate = 360;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        var actions = GetComponent<PlayerInput>().actions;
        playerMap = actions.FindActionMap("Player");
        uiMap = actions.FindActionMap("UI");
        _rbPlayer = GetComponent<Rigidbody>();
        DeactivateUIMode();
    }

    public void ActivateUIMode()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerMap.Disable();
        uiMap.Enable();
    }
    public void DeactivateUIMode()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerMap.Enable();
        uiMap.Disable();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        move = _cameraTransform.forward * _moveInput.y + _cameraTransform.right * _moveInput.x;
        move.y = 0f;
        _rbPlayer.AddForce(move.normalized * _speed, ForceMode.VelocityChange);
    }


    public void DropItem()
    {
        if (_itemInHand.isAnimCompleted)
        {
            //deattach from parent
            _itemInHand.transform.parent = null;
            //adding rigidbody back and setting its values
            Rigidbody _rb = _itemInHand.gameObject.AddComponent<Rigidbody>();
            _rb.interpolation = RigidbodyInterpolation.Interpolate;
            _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            _rb.useGravity = true;
            _rb.excludeLayers = LayerMask.GetMask("Player");

            //Vector3 deneme = Vector3.ClampMagnitude(_rbPlayer.velocity, _speed * 1.4f);
            _rb.AddForce(_rbPlayer.velocity, ForceMode.VelocityChange);
            _rb.AddForce(((_cameraTransform.transform.forward + _cameraTransform.transform.up) * 2), ForceMode.VelocityChange);

            //setting trigger false so it can interact with world
            _itemInHand.GetComponent<Collider>().isTrigger = false;
            //hand is empty now
            _itemInHand = null;
        }

    }


    public void DebugIt()
    {
        if(Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void Interact()
    {
        float interactDistance = 7.1f;

        if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit raycastHit, interactDistance, _interactable))
        {
            Transform _object = raycastHit.transform;

            //target is interactable
            if (_object.TryGetComponent(typeof(IInteractable), out Component component))
            {
                //hand is empty, interact with object.
                if (_itemInHand == null)
                {
                    _object.GetComponent<IInteractable>().Interact(transform);
                }
                //hand is not empty
                else
                {
                    //hand not empty and interacted with rack
                    if (_object.GetComponent<Rack>() != null)
                    {
                        _object.GetComponent<IInteractable>().Interact(transform);
                    }
                    //hand not empty and interacted with another item
                    if (_object.GetComponent<Item>() != null)
                    {
                        if (_itemInHand.isAnimCompleted)
                        {
                            DropItem();
                            _object.GetComponent<IInteractable>().Interact(transform);
                        }
                    }
                    if (_object.GetComponent<Label>() != null)
                    {
                        _object.GetComponent<IInteractable>().Interact(transform);
                    }
                }
            }
        }
    }


    private void Update()
    {
        ////Drop item in hand on the ground
        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    //checking if there is item in hand
        //    if(_itemInHand != null)
        //    {
        //        DropItem();
        //    }
        //}
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Cursor.lockState = CursorLockMode.Confined;
        //    Cursor.visible = false;
        //}
        //if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        //{
        //    Cursor.lockState = CursorLockMode.Locked;
        //    Cursor.visible = true;
        //}
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    PauseInput();
        //}
    }

}
