using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //Transform del jugador, lo arrastrar� en Unity
    [SerializeField] Transform playerTransfom;


    //Distancia en Z (negativa) y en Y (en positivo) para alejar la c�mara
    [SerializeField] float distance = -10;
    [SerializeField] float verticalOffset = 5;
    //variable para suavizar el movimiento de la camara, que se puede cambiar en Unity
    [SerializeField] float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (playerTransfom != null)
        {
            Vector3 tar = playerTransfom.position + new Vector3(0f, verticalOffset, distance);
            transform.position = Vector3.SmoothDamp(transform.position, tar, ref velocity, smoothTime); 
        }

        //Desplazamiento en Vector3
        Vector3 offset = new Vector3(0f, verticalOffset, distance);
     
        transform.position = playerTransfom.position + offset;

    }
}