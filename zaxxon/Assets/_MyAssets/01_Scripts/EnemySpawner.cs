using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Prefab que voy a spawnear
    [SerializeField] GameObject[] enemies;
    //Intervalo
    [SerializeField] float interval = 0.5f;

    //Valores aleatorios en X y en Y
    [SerializeField] float limitX;
    [SerializeField] float limitUp;
    [SerializeField] float limitDown;

    //Enemigos intermedios
    [SerializeField] float firstEnemyDistance;
    [SerializeField] float distanceBetweenEnemies;

    //Oleadas
    [SerializeField] int waves;

    //Jugador para saber su velocidad
    [SerializeField] PlayerManager playerManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine("SpawnEnemy");
        EnemigosIntermedios();

    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            for (int n = 0; n < waves; n++)
            {
                SacarNave(0);

            }
            interval = distanceBetweenEnemies / playerManager.moveSpeed;
            yield return new WaitForSeconds(interval);
        }
    }

    void EnemigosIntermedios()
    {
        float distanceToFill = transform.position.z - firstEnemyDistance;
        float numberOfEnemiesf = distanceToFill / distanceBetweenEnemies;
        int ciclos = Mathf.FloorToInt(numberOfEnemiesf);
        for (int i = 0; i < ciclos; i++)
        {
            SacarNave(distanceToFill);
            distanceToFill -= distanceBetweenEnemies;

        }

    }

    void SacarNave(float distanceZ)
    {
        float randomX = Random.Range(-limitX, limitX);
        float randomY = Random.Range(limitDown, limitUp);
        //Instancio en posici�n aleatoria en X e Y pero Z donde est� el spawner
        //Vector3 despl = new Vector3(randomX, randomY, distanceZ);
        //Vector3 instPos = transform.position + despl;
        Vector3 instPos = new Vector3(randomX, randomY, transform.position.z - distanceZ);
        int r = Random.Range(0, enemies.Length);
        Instantiate(enemies[r], instPos, Quaternion.identity);
    }


}
