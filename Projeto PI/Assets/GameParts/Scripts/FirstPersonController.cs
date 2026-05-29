using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("References")]
    public Transform cameraHolder;
    public Transform weaponHolder;

    private CharacterController controller;
    private PlayerInputActions inputActions;

    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool runInput;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 2f;
    private float xRotation = 0f;

    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float gravity = -9.81f;
    private Vector3 velocity;

    [Header("Headbob Settings")]
    public float bobFrequency = 1.5f;
    public float bobAmplitude = 0.1f;
    private float bobTimer;
    private Vector3 initialCamPos;

    [Header("Sway Settings")]
    public float swayAmount = 2f;
    public float swaySmooth = 5f;

    private Vector3 initialWeaponPos;

    [Header("States")]
    public bool isRunning;
    public bool isGrounded;
    public bool isCutscene;
    public bool canMove = true;
    public bool canLook = true;

    void Awake()
    {
        controller = GetComponent<CharacterController>();

        inputActions = new PlayerInputActions();

        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        inputActions.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Look.canceled += ctx => lookInput = Vector2.zero;

        inputActions.Player.Run.performed += ctx => runInput = true;
        inputActions.Player.Run.canceled += ctx => runInput = false;
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        initialCamPos = cameraHolder.localPosition;

        if (weaponHolder != null)
            initialWeaponPos = weaponHolder.localPosition;
    }

    void Update()
    {
        if (isCutscene)
            return;

        HandleMouseLook();
        HandleMovement();
        HandleHeadbob();
        HandleSway();
    }

    void HandleMouseLook()
    {
        if (!canLook) return;

        float mouseX = lookInput.x * mouseSensitivity * 100f * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * 100f * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleMovement()
    {
        if (!canMove) return;

        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float x = moveInput.x;
        float z = moveInput.y;

        isRunning = runInput;

        float speed = isRunning ? runSpeed : walkSpeed;

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public interface IInteractable
    {
        string GetInteractionText();

        void Interact();

        void ShowUI(string text);

        void HideUI();
    }



    void HandleHeadbob()
    {
        float moveAmount = moveInput.magnitude;

        if (!isGrounded || moveAmount < 0.1f)
        {
            bobTimer = 0;
            cameraHolder.localPosition = Vector3.Lerp(
                cameraHolder.localPosition,
                initialCamPos,
                Time.deltaTime * 5f
            );
            return;
        }

        bobTimer += Time.deltaTime * (isRunning ? bobFrequency * 1.5f : bobFrequency);

        float bobOffset = Mathf.Sin(bobTimer) * bobAmplitude;

        Vector3 targetPos = initialCamPos + new Vector3(0, bobOffset, 0);

        cameraHolder.localPosition = Vector3.Lerp(
            cameraHolder.localPosition,
            targetPos,
            Time.deltaTime * 10f
        );
    }

    void HandleSway()
    {
        if (weaponHolder == null) return;

        float mouseX = lookInput.x;
        float mouseY = lookInput.y;

        Vector3 targetPos = initialWeaponPos + new Vector3(-mouseX, -mouseY, 0) * swayAmount;

        weaponHolder.localPosition = Vector3.Lerp(
            weaponHolder.localPosition,
            targetPos,
            Time.deltaTime * swaySmooth
        );
    }
    public void SetCutscene(bool state)
    {
        isCutscene = state;
        canMove = !state;
        canLook = !state;
    }

    public void EnableMovement(bool state)
    {
        canMove = state;
    }

    public void EnableLook(bool state)
    {
        canLook = state;
    }
}