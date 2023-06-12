using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int health;
    public int deathcnt;
    public Sprite[] sprites;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    void OnHit(int dmg) //맞다 //dmg 불러오기
    {
        health -= dmg; //체력에서 데미지 만큼 줄이기
        spriteRenderer.sprite = sprites[1]; //스프라이트 1상태로 불러오기
        Invoke("ReturnSprite", 0.1f); //딜레이

        if(health <= 0) //만약 체력이 0이라면
        {
            Destroy(gameObject); //오브젝트 파괴
            deathcnt++;
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
