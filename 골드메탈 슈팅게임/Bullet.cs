using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag== "BorderBullet") //경계선에 닿으면 총알이 사라지는 것
        {
            Destroy(gameObject);
        }
    }
}
