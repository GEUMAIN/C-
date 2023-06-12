using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyObjs;
    public Transform[] spawnPoint;

    public float maxSpawnDelay;//스폰 최고 딜레이
    public float curSpawnDelay;

    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if(curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy();
            maxSpawnDelay = Random.Range(0.5f, 3f);
            curSpawnDelay = 0;
        }
    }
    void SpawnEnemy()
    {
        int ranEnemey = Random.Range(0, 3); //소환될 적
        int ranPoint = Random.Range(0, 9); //소환될 위치

        GameObject enemy =Instantiate(enemyObjs[ranEnemey],
            spawnPoint[ranPoint].position,
            spawnPoint[ranPoint].rotation);
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        if (ranPoint == 5 || ranPoint == 6) // 오른쪽에서 오는 적 속도 방향
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1);
        }
        else if (ranPoint == 7 || ranPoint == 8) //왼쪽에서 오는 적 속도 방향
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
        }
        else //중앙에서 오는 적  속도 방향
        {
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        }
    }
    void StopStage(int deathcnt)
    {

    }

}
