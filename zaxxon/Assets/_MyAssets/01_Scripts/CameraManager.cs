using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //Transform del jugador, lo arrastrar� en Unity
    [SerializeField] Transform playerTransfom;


    //Distancia en Z (negativa) y en Y (en positivo) para alejar la c�mara
    [SerializeField] float distance = -10;
    [SerializeField] float verticalOffset = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {

        //Desplazamiento en Vector3
        Vector3 offset = new Vector3(0f, verticalOffset, distance);
        //Me traslado a esa posici�n
        transform.position = playerTransfom.position + offset;
        //Tambi�n puedo rotar con el objeto
        //transform.rotation = playerTransfom.rotation;
    }
}