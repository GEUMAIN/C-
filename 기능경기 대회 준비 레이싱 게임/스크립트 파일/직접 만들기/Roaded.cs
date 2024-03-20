using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roaded : MonoBehaviour
{
    public static Roaded instance;

    public Renderer meshRederer;

    public float speed = 1.0f;

    public int cnt = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        Vector2 offset = meshRederer.material.mainTextureOffset;
        offset = offset + new Vector2(0, speed * Time.deltaTime);
        meshRederer.material.mainTextureOffset = offset;
        if (Input.GetMouseButtonDown(0))
        {
            cnt++;
        }
    }
}
