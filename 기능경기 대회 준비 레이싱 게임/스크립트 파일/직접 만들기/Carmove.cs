using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carmove : MonoBehaviour
{
    public float speed = 3f;

    public float leftrotate = 70;
    public float rightrotate = -70;

    public GameObject Trail;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.Translate(Vector2.up * speed * Time.fixedDeltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.Translate(Vector2.down * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            leftrotation();
        }

        if (gameObject.transform.position.z != rightrotate)
        {
            if (Input.GetKey(KeyCode.D))
            {
                rightrotation();
            }
        }

        if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            Trail.SetActive(true);
        }
        if(Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
        {
            Trail.SetActive(true);
        }
    }

    void leftrotation()
    {
        if (gameObject.transform.rotation.z >= 0.65) return;
        else gameObject.transform.Rotate(new Vector3(0, 0, 30) * Time.fixedDeltaTime);
    }

    void rightrotation()
    {
        if (gameObject.transform.rotation.z <= -0.65) return;
        else gameObject.transform.Rotate(new Vector3(0, 0, -30) * Time.fixedDeltaTime);
    }
}
