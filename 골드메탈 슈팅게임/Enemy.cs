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
    void OnHit(int dmg) //�´� //dmg �ҷ�����
    {
        health -= dmg; //ü�¿��� ������ ��ŭ ���̱�
        spriteRenderer.sprite = sprites[1]; //��������Ʈ 1���·� �ҷ�����
        Invoke("ReturnSprite", 0.1f); //������

        if(health <= 0) //���� ü���� 0�̶��
        {
            Destroy(gameObject); //������Ʈ �ı�
            deathcnt++;
        }
    }
    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0]; //������ ��������Ʈ 0���� ����
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BorderBullet") //���� ��輱�� ������ �����
            Destroy(gameObject);
        else if (collision.gameObject.tag == "PlayerBullet") //���� �÷��̾� �Ѿ˿� ������
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>(); //bullet���� dmg�� ������
            OnHit(bullet.dmg); //OnHit ����

            Destroy(collision.gameObject); //���� ���� ��ü ���ֱ�
        }
    }
}
