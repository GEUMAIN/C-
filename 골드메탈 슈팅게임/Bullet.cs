using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int dmg;
    public static int attack;
    private void Awake()
    {
        dmg = dmg + Player.plusdmg;
    }
    private void Update()
    {
        attack = dmg;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag== "BorderBullet") //��輱�� ������ �Ѿ��� ������� ��
        {
            Destroy(gameObject);
        }
    }
}
