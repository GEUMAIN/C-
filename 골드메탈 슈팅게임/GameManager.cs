using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public GameObject[] enemyObjs;
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    public GameObject item4;
    public GameObject player;

    public GameObject item4display1;
    public GameObject item4display2;
    public GameObject item4display3;
    public GameObject item4display4;
    public GameObject item4display5;
    public GameObject item4display6;
    public GameObject item3display1;
    public GameObject item3display2;
    public GameObject item2display1;
    public GameObject item2display2;
    public GameObject item1display1;
    public GameObject item1display2;
    public GameObject item1display3;

    public Transform[] spawnPoint;

    public float maxSpawnDelay;//���� �ְ� ������
    public float curSpawnDelay;
    public float min;
    public float max;


    public bool spawn;
    public bool stage1;
    public bool stage2;
    public bool stage3;



    public int itemrandom;

    public static int stage;

    private void Awake()
    {
        spawn = true;
        stage = 9;
        stage1 = false;
        stage2 = false;
        stage3 = false;
        min = 0.5f;
        max = 5f;
    }

    void FixedUpdate()
    {
        if (spawn)
        {
            spawnEnemy();
        }
        stopSpawn();
    }
    void spawnEnemy() {
        curSpawnDelay += Time.deltaTime;

        if (curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy();
            maxSpawnDelay = Random.Range(min, max);
            curSpawnDelay = 0;
        }
    }
    void stopSpawn()
    {
        if (Enemy.deathcnt > stage) //���� ���� ī��Ʈ�� 9���� ũ�ٸ�
        {
            spawn = false; //������ ���߰�
            stage1 = true; //��������1�� Ȱ��ȭ �Ѵ�
            if (stage1 == true) //���� ��������1�� Ȱ��ȭ �ȴٸ�
            {
                if (Enemy.deathcnt == 10 || Enemy.deathcnt == 11 || Enemy.deathcnt == 12 || Enemy.deathcnt == 13)
                {
                    item1.SetActive(true); //������1 Ȱ��ȭ
                    item2.SetActive(true); //������2 Ȱ��ȭ
                    max = 2.5f;

                    item2display1.SetActive(true);
                    item2display2.SetActive(true);
                    item1display1.SetActive(true);
                    item1display2.SetActive(true);
                    item1display3.SetActive(true);


                }
            }

            if (Enemy.deathcnt > stage) //���� ī��Ʈ�� 19 ���� ũ�ٸ�
            {
                spawn = false;
                stage2 = true; //�������� 2�� Ȱ��ȭ
                if (stage2 == true) //���� ��������2�� Ȱ��ȭ �ȴٸ�
                {
                    if(Enemy.deathcnt == 20 || Enemy.deathcnt == 21 || Enemy.deathcnt == 22 || Enemy.deathcnt == 23)
                    {
                        item3.SetActive(true); //������3 Ȱ��ȭ
                        item4.SetActive(true); //������4 Ȱ��ȭ
                        max = 2f;
                        item4display1.SetActive(true);
                        item4display2.SetActive(true);
                        item4display3.SetActive(true);
                        item4display4.SetActive(true);
                        item4display5.SetActive(true);
                        item4display6.SetActive(true);
                        item3display1.SetActive(true);
                        item3display2.SetActive(true);
                    }
                }
            }
        }
    
        else if (Enemy.deathcnt == 0) //���� ����ī��Ʈ�� 0�̶��
        {
            maxSpawnDelay = Random.Range(min, max); //�ʱ�ȭ
            spawn = true; //�� ���� �ٽ� ����
            item4display1.SetActive(false);
            item4display2.SetActive(false);
            item4display3.SetActive(false);
            item4display4.SetActive(false);
            item4display5.SetActive(false);
            item4display6.SetActive(false);
            item3display1.SetActive(false);
            item3display2.SetActive(false);
            item2display1.SetActive(false);
            item2display2.SetActive(false);
            item1display1.SetActive(false);
            item1display2.SetActive(false);
            item1display3.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            item Item = collision.gameObject.GetComponent<item>();
            switch (Item.type)
            {
                case "1":
                    Enemy.deathcnt = 0;
                    break;
            }

        }
        if (collision.gameObject.tag == "Item")
        {
            item Item = collision.gameObject.GetComponent<item>();
            switch (Item.type)
            {
                case "2":
                    Enemy.deathcnt = 0;
                    break;
            }
        }
        if (collision.gameObject.tag == "Item")
        {
            item Item = collision.gameObject.GetComponent<item>();
            switch (Item.type)
            {
                case "3":
                    Enemy.deathcnt = 0;
                    break;
            }
        }
        if (collision.gameObject.tag == "Item")
        {
            item Item = collision.gameObject.GetComponent<item>();
            switch (Item.type)
            {
                case "4":
                    Enemy.deathcnt = 0;
                    break;
            }
        }
    }
    
    void SpawnEnemy()
    {
        int ranEnemey = Random.Range(0, 3); //��ȯ�� ��
        int ranPoint = Random.Range(0, 9); //��ȯ�� ��ġ

        GameObject enemy =Instantiate(enemyObjs[ranEnemey],
            spawnPoint[ranPoint].position,
            spawnPoint[ranPoint].rotation);
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;

        if (ranPoint == 5 || ranPoint == 6) // �����ʿ��� ���� �� �ӵ� ����
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1);
        }
        else if (ranPoint == 7 || ranPoint == 8) //���ʿ��� ���� �� �ӵ� ����
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
        }
        else //�߾ӿ��� ���� ��  �ӵ� ����
        {
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        }
    }
    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe",2f); //2�� ��Ÿ��
        player.transform.position = Vector3.down * -4f;
        player.SetActive(true);

    }
    void RespawnPlayerExe()
    {
        player.transform.position = Vector3.down * 4f;
        player.SetActive(true);
    }

}
