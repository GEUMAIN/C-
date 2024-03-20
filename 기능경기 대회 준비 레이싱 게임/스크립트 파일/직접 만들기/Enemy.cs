using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public static float i;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        i = i + 30;

        GameSystem.firstendscore = GameSystem.firstendscore + i;
        Destroy(gameObject);
        gameObject.SetActive(false);

    }
}
