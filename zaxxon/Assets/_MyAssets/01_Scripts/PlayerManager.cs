using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    // Variables de estado y velocidad
    bool isAlive;
    public float moveSpeed;
    [SerializeField] float desplSpeed; // Velocidad de movimiento

    // Variables que obtienen el movimiento de los ejes
    float moveX;
    float moveY;

    // Rotación máxima en Z (inclinación lateral) y X (inclinación vertical)
    float maxRotationZ = 35f;
    float maxRotationX = 15f;

    // ROTACIÓN SUAVIZADA
    [SerializeField] float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    Vector3 currentRot;

    // Clase creada con el Input Asset
    InputActions inputActions;

    private void Awake()
    {
        moveSpeed = 100f;

        // Instanciamos el Input Actions
        inputActions = new InputActions();

        // Botón de fuego
        inputActions.Player.Fire.started += _ => Fire();

        // Entradas de movimiento (A/D y W/S o Joystick izquierdo)
        inputActions.Player.MoveX.performed += ctx => moveX = ctx.ReadValue<float>();
        inputActions.Player.MoveX.canceled += _ => moveX = 0f;

        inputActions.Player.MoveY.performed += ctx => moveY = ctx.ReadValue<float>();
        inputActions.Player.MoveY.canceled += _ => moveY = 0f;

        // (Nota: La acción 'Rotate' de la Q/E ya no se usa aquí porque el movimiento lo hace todo)
    }

    private void Update()
    {
        MovePlayer();
        RotatePlayer();
        CheckLimits();
    }

    void CheckLimits()
    {
        // Restricción de área fija para evitar que el jugador se salga de la pantalla
        float clampedX = Mathf.Clamp(transform.position.x, -150f, 150f);
        float clampedY = Mathf.Clamp(transform.position.y, 4f, 200f);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    void MovePlayer()
    {
        transform.Translate(Vector3.right * desplSpeed * moveX * Time.deltaTime, Space.World);
        transform.Translate(Vector3.up * desplSpeed * moveY * Time.deltaTime, Space.World);
    }

    void RotatePlayer()
    {
        // La rotación en Z y X se calcula automáticamente según las teclas de movimiento (A/D y W/S)
        Vector3 vectorRotZ = Vector3.forward * -maxRotationZ * moveX;
        Vector3 vectorRotX = Vector3.right * -maxRotationX * moveY;
        Vector3 vectorRot = vectorRotX + vectorRotZ;

        currentRot = Vector3.SmoothDamp(currentRot, vectorRot, ref velocity, smoothTime);
        transform.eulerAngles = currentRot; // Aplicamos los ángulos de Euler suavizados
    }

    void Fire()
    {
        print("POOM");
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
