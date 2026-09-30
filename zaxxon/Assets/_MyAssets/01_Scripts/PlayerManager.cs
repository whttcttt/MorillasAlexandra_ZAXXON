using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    //Variables de vivo, velocidad y velocidad de desplazamiento lateral
    bool isAlive;
    public float moveSpeed;
    [SerializeField] float desplSpeed; //Serializada para poder cambiarla en Unity
    [SerializeField] float rotationSpeed; //velocidad a la que rotar?, en vueltas por segundo


    //Variable que obtendr? el movimiento del joystick en el eje X
    float moveX;
    float moveY;
    float moveZ;

    //Variable que obiene la rotaci?n del RS
    float rotation;
    //Rotaci?n m?xima
    float maxRotationZ = 35f;
    float maxRotationX = 15f;

    //ROTACI?N SUAVIZADA
    [SerializeField] float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    Vector3 currentRot;

    //Clase creada con el Input Asset
    InputActions inputActions;

    //Usaremos el Awake para activar los inputs y obtener los datos
    private void Awake()
    {
        //Declaro la velocidad de los enemigos en el Awake
        moveSpeed = 100f;

        //Creamos la instancia del asset de entradas IMPORTANTE: hay que activarlo en OnEnable()
        inputActions = new InputActions();

        //Cuando pulsamos el bot?n de fuego se ejecuta el m?todo correspondiente
        inputActions.Player.Fire.started += _ => Fire();

        //Cuando activamos la entrada de mover en X le damos el variable a la valor, y al dejar de tocarla la ponemos en cero
        inputActions.Player.MoveX.performed += ctx => moveX = ctx.ReadValue<float>();
        inputActions.Player.MoveX.canceled += _ => moveX = 0f;

        inputActions.Player.MoveY.performed += ctx => moveY = ctx.ReadValue<float>();
        inputActions.Player.MoveY.canceled += _ => moveY = 0f;

        inputActions.Player.MoveZ.performed += ctx => moveZ = ctx.ReadValue<float>();
        inputActions.Player.MoveZ.canceled += _ => moveZ = 0f;

        //Obtenemos la rotaci?n
        inputActions.Player.Rotate.performed += ctx => rotation = ctx.ReadValue<float>();
        inputActions.Player.Rotate.canceled += _ => rotation = 0f;

    }

    private void Update()
    {
        MovePlayer();
        RotatePlayer();

    }

    void MovePlayer()
    {
        transform.Translate(Vector3.right * desplSpeed * moveX * Time.deltaTime, Space.World);
        transform.Translate(Vector3.up * desplSpeed * moveY * Time.deltaTime, Space.World);

        //Movimiento en Z, hacia adelante, a la velocidad que diga el jugador
        transform.Translate(Vector3.forward * desplSpeed * moveZ * Time.deltaTime, Space.World);
    }

    void RotatePlayer()
    {
        //Rotaci?n loca
        //transform.Rotate(Vector3.forward * rotation * rotationSpeed * Time.deltaTime * -360);

        //Sumo el vector de rotacion en Z mas el de rotacion en X para bascular
        Vector3 vectorRotZ = Vector3.forward * -maxRotationZ * moveX;
        Vector3 vectorRotX = Vector3.right * -maxRotationX * moveY;
        Vector3 vectorRot = vectorRotX + vectorRotZ;
        currentRot = Vector3.SmoothDamp(currentRot, vectorRot, ref velocity, smoothTime);
        transform.eulerAngles = currentRot;
    }


    void Fire()
    {
        print("POOM");
    }

    //IMPORTANTE: activar el Inpu
    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }







}