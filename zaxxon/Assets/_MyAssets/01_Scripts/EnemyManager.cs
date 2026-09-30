using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //La velocidad de movimiento, que la obtendré del jugador
    float speed;

    //El componente playerManager que tendrá el jugador
    [SerializeField] PlayerManager playerManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Necesito acceder al objeto (jugador) que tiene el componente PlayerManager
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //Una vez accedido a ese objeto, accedo a su componente
        playerManager = player.GetComponent<PlayerManager>();

    }

    // Update is called once per frame
    void Update()
    {
        //Me muevo a la velocidad que diga el jugador
        speed = playerManager.moveSpeed;
        transform.Translate(Vector3.back * speed * Time.deltaTime);

    }
}