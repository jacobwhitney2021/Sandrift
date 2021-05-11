using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner_Script : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject enemy1;
    public GameObject enemy2;

    public GameObject enemy3;

    public float height;

    public const int enemyCount = 1;
    public const float averageDistance = 80f;
    public const float startingHeight = 50f;
    public const float drawDistanceWidth = 500f;
    public const float drawDistanceHeight = 500f;

    private GameObject[] enemies;

    Vector3 playerPosition;
    Vector3 forward;
    void Start()
    {
        enemies = new GameObject[enemyCount];
        enemies[0] = enemy1;
        //enemies[1] = enemy2;
        //enemies[2] = enemy3;

        Vector3 position = transform.position;
        
        float distFromPlayer = 0;
        while (drawDistanceHeight > distFromPlayer) {
            
            createLine(distFromPlayer, position);
            distFromPlayer += averageDistance;
        }
        
        playerPosition = position;
        forward = transform.forward;


        Debug.Log(forward);
        
    }

    public void createLine(float distFromPlayer, Vector3 position)
    {
        Vector3 extra = new Vector3(-1 * drawDistanceWidth, height , distFromPlayer);
        while (drawDistanceWidth > extra.x) {
            float actualX = Random.Range(averageDistance / -2 , averageDistance / 2);
            float actualZ = Random.Range(averageDistance / -2 , averageDistance / 2);
            Vector3 actual = new Vector3(actualX, 0, actualZ);
            int rand = Random.Range(0, enemyCount);
            Instantiate(enemies[rand], position + extra + actual,  Quaternion.identity);
            
            extra += new Vector3(averageDistance, 0, 0);
        }   
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z - averageDistance > playerPosition.z) {
            playerPosition = transform.position;
            createLine(drawDistanceHeight, playerPosition);
        }
    }
}
