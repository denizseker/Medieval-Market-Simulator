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
    public Rigidbody _rbPlayer;
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
    public void DebugIt(InputAction.CallbackContext context)
    {
        //if(Cursor.lockState == CursorLockMode.None)
        //{
        //    Cursor.lockState = CursorLockMode.Locked;
        //    Cursor.visible = true;
        //}
        //else
        //{
        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;
        //}

        if (context.started) //KEY PRESSED
        {
            
        }

        
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {

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
