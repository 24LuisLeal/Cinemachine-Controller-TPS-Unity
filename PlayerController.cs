using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Referencia al script del movimiento
    private InputMovement inputPlayer;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private Transform cameraMain;
    private Transform child;
    private float rotationSpeed = 4f;

    private void Awake()
    {
        inputPlayer = new InputMovement();
        controller = GetComponent<CharacterController>();
    }
    private void OnEnable()
    {
        inputPlayer.Enable();
    }
    private void OnDisable()
    {
        inputPlayer.Disable();
    }
    private void Start()
    {
        //Ver donde el jugador está viendo
        cameraMain = Camera.main.transform;
        child = transform.GetChild(0).transform;
    }

    void Update()
    {
        //Esta el jugador en el suelo si o no
        groundedPlayer = controller.isGrounded;
        //Si está en el suelo y la velocidad en y es mayor a 0
        if (groundedPlayer && playerVelocity.y < 0)
        {
            //Entonces no habrá ninguna fuerza en y
            playerVelocity.y = 0f;
        }
        //Movimiento en el vector en 2d Joysticks
        Vector2 movementInput = inputPlayer.PlayerMain.Move.ReadValue<Vector2>();
        //Movimiento en el plano "x" y "y"
        Vector3 move = (cameraMain.forward * movementInput.y + cameraMain.right * movementInput.x);
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        //Si el botón de saltar está siendo oprimido y el jugador está en el suelo
        if (inputPlayer.PlayerMain.Jump.triggered && groundedPlayer)
        {
            //Entonces saltaremos
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if(movementInput != Vector2.zero)
        {
            Quaternion rotation = Quaternion.Euler(new Vector3
            (child.localEulerAngles.x, cameraMain.localEulerAngles.y, child.localEulerAngles.z));
            child.rotation = Quaternion.Lerp(child.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }

}
