using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    public Text death;
    public Text attack;
    void Update()
    {
        death.text = Enemy.deathcnt.ToString();
        attack.text = Bullet.attack.ToString();
    }
}
