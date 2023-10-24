using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheat : MonoBehaviour
{
    public GameObject hindrance2;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            hindrance2.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit(); //게임종료
        }
    }
}
