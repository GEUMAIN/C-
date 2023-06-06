using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public int heart; //체력


    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    private void Awake()
    {
        heart = 3;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("체력 깎는중");
            heart -= 1;
        
        }
        if (heart == 2)
        {
            heart3.SetActive(false);
        }
        if (heart == 1)
        {
            heart2.SetActive(false);
        }
        if (heart == 0)
        {
            heart1.SetActive(false);
        }
    }
}
