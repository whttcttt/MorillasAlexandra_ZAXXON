using UnityEngine;

public class VayViene : MonoBehaviour
{
    //Animar el objeto de un lado a otro
    //Velocidad de suavizado, entre 0 y 1

    [Range(0, 1)]
    public float smoothSpeed;
    // Puntos de inicio y de final
    [Header("Punto de inicio (Transform)")]
    [SerializeField] Transform initReference;
    [Header("Punto de fin (Transform)")]
    [SerializeField] Transform endReference;
    //Los vectores de posici�n que crearemos
    Vector3 initPosition;
    Vector3 endPosition;

    //Tiempo inicial
    float t = 0f;

    private void Start()
    {
        //Indicamos los Vectores para el movimiento (podemos crearlos manualemente)
        initPosition = initReference.position;
        endPosition = endReference.position;
    }

    void Update()
    {
        TranslateLerp();
    }


    void TranslateLerp()
    {
        // Cambiamos la posici�n del objeto entre los dos extremos
        transform.position = Vector3.Lerp(initPosition, endPosition, t);

        // .. incrementamos el interpolador, seg�n el tiempo transcurrido
        t += smoothSpeed * Time.deltaTime;

        // Si el interpolador alcanza el punto de destino
        // intercambiamos el inicio con el final para que cambie de orientaci�n
        // y se mueva en la direcci�n contraria
        if (transform.position == endPosition)
        {
            Vector3 temp = endPosition;
            endPosition = initPosition;
            initPosition = temp;
            t = 0.0f; //Reseteamos el tiempo
        }
    }
}