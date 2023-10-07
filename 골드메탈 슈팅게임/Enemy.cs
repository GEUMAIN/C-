using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{

    public string enemyname;

    public float maxShotDelay; //�߻� ������ ���� �ִ�
    public float curShotDelay; //����
    public float speed;

    public int health;
    public static int deathcnt;

    public Sprite[] sprites;


    public GameObject EnmeybulletobjA; //�� �Ѿ�A ������Ʈ
    public GameObject EnemybulletobjB; //�� �Ѿ�B ������Ʈ
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
        //�Ѿ� ���
        if (curShotDelay < maxShotDelay) //���� �������� �ִ뺸�� ���� �ʾҴٸ�?
        {
            return;
        }

        if(enemyname == "S")
        {
            GameObject bullet = Instantiate(EnmeybulletobjA, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>(); //���ֱ�

            Vector3 dirvec = player.transform.position - transform.position;
            rigid.AddForce(dirvec.normalized * 3, ForceMode2D.Impulse);//�� �������� �ӵ�*10��ŭ ���ư���
        }
        else if (enemyname == "L")
        {
            GameObject bulletR = Instantiate(EnemybulletobjB, transform.position + Vector3.right*0.3f, transform.rotation);
            GameObject bulletL = Instantiate(EnemybulletobjB, transform.position + Vector3.left * 0.3f, transform.rotation);

            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>(); //���ֱ�
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>(); //���ֱ�

            Vector3 dirvecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            Vector3 dirvecL = player.transform.position - (transform.position + Vector3.left * 0.3f);

            rigidR.AddForce(dirvecR.normalized * 4, ForceMode2D.Impulse);//�� �������� �ӵ�*10��ŭ ���ư���
            rigidL.AddForce(dirvecL.normalized * 4, ForceMode2D.Impulse);//�� �������� �ӵ�*10��ŭ ���ư���
        }

        curShotDelay = 0; //�Ѿ��� ��� 0���� �ʱ�ȭ ���ֱ�
    }
    void Reload()
    {
        curShotDelay += Time.deltaTime;

    }



    void OnHit(int dmg) //�´� //dmg �ҷ�����
    {
        health -= dmg; //ü�¿��� ������ ��ŭ ���̱�
        spriteRenderer.sprite = sprites[1]; //��������Ʈ 1���·� �ҷ�����
        Invoke("ReturnSprite", 0.1f); //������

        if(health <= 0) //���� ü���� 0�̶��
        {
            Destroy(gameObject); //������Ʈ �ı�
            deathcnt+= 1;
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
