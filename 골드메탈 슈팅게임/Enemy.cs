using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{

    public string enemyname;

    public float maxShotDelay; //발사 딜레이 변수 최대
    public float curShotDelay; //현재
    public float speed;

    public int health;
    public static int deathcnt;

    public Sprite[] sprites;


    public GameObject EnmeybulletobjA; //적 총알A 오브젝트
    public GameObject EnemybulletobjB; //적 총알B 오브젝트
    public GameObject player;


    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    void Update()
    {
        Fire();
        Reload();
    }
    void Fire()
    {
        //총알 쏘기
        if (curShotDelay < maxShotDelay) //현재 장전수가 최대보다 넘지 않았다면?
        {
            return;
        }

        if(enemyname == "S")
        {
            GameObject bullet = Instantiate(EnmeybulletobjA, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>(); //힘주기

            Vector3 dirvec = player.transform.position - transform.position;
            rigid.AddForce(dirvec.normalized * 3, ForceMode2D.Impulse);//위 방향으로 속도*10만큼 나아가게
        }
        else if (enemyname == "L")
        {
            GameObject bulletR = Instantiate(EnemybulletobjB, transform.position + Vector3.right*0.3f, transform.rotation);
            GameObject bulletL = Instantiate(EnemybulletobjB, transform.position + Vector3.left * 0.3f, transform.rotation);

            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>(); //힘주기
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>(); //힘주기

            Vector3 dirvecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            Vector3 dirvecL = player.transform.position - (transform.position + Vector3.left * 0.3f);

            rigidR.AddForce(dirvecR.normalized * 4, ForceMode2D.Impulse);//위 방향으로 속도*10만큼 나아가게
            rigidL.AddForce(dirvecL.normalized * 4, ForceMode2D.Impulse);//위 방향으로 속도*10만큼 나아가게
        }

        curShotDelay = 0; //총알을 쏘고 0으로 초기화 해주기
    }
    void Reload()
    {
        curShotDelay += Time.deltaTime;

    }



    void OnHit(int dmg) //맞다 //dmg 불러오기
    {
        health -= dmg; //체력에서 데미지 만큼 줄이기
        spriteRenderer.sprite = sprites[1]; //스프라이트 1상태로 불러오기
        Invoke("ReturnSprite", 0.1f); //딜레이

        if(health <= 0) //만약 체력이 0이라면
        {
            Destroy(gameObject); //오브젝트 파괴
            deathcnt+= 1;
        }
    }
    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0]; //맞으면 스프라이트 0으로 변경
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BorderBullet") //만약 경계선에 닿으면 사라짐
            Destroy(gameObject);
        else if (collision.gameObject.tag == "PlayerBullet") //만약 플레이어 총알에 닿으면
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>(); //bullet에서 dmg를 가져옴
            OnHit(bullet.dmg); //OnHit 실행

            Destroy(collision.gameObject); //이후 닿은 물체 없애기
        }
    }
}
