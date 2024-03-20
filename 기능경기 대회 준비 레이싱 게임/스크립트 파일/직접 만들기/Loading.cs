using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public Slider slider;

    public float i;
    // Update is called once per frame
    public void Update()
    {
        slider.value = i;

    }

    public void FixedUpdate()
    {
        i +=  0.01f;

        if (i >= 1)
        {
            SceneManager.LoadScene("game1");
        }
    }
}
