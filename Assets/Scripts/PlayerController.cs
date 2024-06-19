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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rbPlayer = GetComponent<Rigidbody>();
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

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float interactDistance = 7.1f;

            if(Physics.Raycast(_cameraTransform.position, _cameraTransform.forward,out RaycastHit raycastHit, interactDistance, _interactable))
            {
                Transform _object = raycastHit.transform;

                //target is interactable
                if (_object.TryGetComponent(typeof(IInteractable), out Component component))
                {
                    //hand is empty, interact with object.
                    if (_itemInHand == null)
                    {
                        Debug.Log("Item yok");
                        _object.GetComponent<IInteractable>().Interact(transform);
                    }
                    //hand is not empty
                    else
                    {
                        //hand not empty and interacted with rack
                        if(_object.GetComponent<Rack>() != null) 
                        {
                            _object.GetComponent<IInteractable>().Interact(transform);
                        }
                        //hand not empty and interacted with another item
                        if(_object.GetComponent<Item>() != null)
                        {
                            DropItem();
                            _object.GetComponent<IInteractable>().Interact(transform);
                        }
                    }
                }
            }
        }
        //Drop item in hand on the ground
        if (Input.GetKeyDown(KeyCode.G))
        {
            //checking if there is item in hand
            if(_itemInHand != null)
            {
                DropItem();
            }
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Instantiate(deneme._itemPrefab, transform.position, Quaternion.identity);
        }
    }

}
