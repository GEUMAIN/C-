using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public static int heart; //ü��

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public GameObject heart5;

    public static int minusheart; //ü�±��

    private void Awake()
    {
        heart = 3;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("ü�� �����");
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
        if (heart == 4)
        {
            heart4.SetActive(true);
        }
        if (heart == 5)
        {
            heart5.SetActive(true);
        }
        if (minusheart == 1) //���� ���̳ʽ� ��Ʈ�� 1�̶��
        {
            if (heart == 5) //���� ��Ʈ�� 5���
            {
                heart5.SetActive(false); //��Ʈ 5�� ��Ȱ��ȭ �ϰ�
            }
            if (heart == 4) //���� ��Ʈ�� 4���
            {
                heart4.SetActive(false); //��Ʈ 4�� ��Ȱ��ȭ �Ѵ�
            }
        }
        if (minusheart == 2) //���� ���̳ʽ� ��Ʈ�� 2���
        {
            heart4.SetActive(false); //��Ʈ 4�� ��Ȱ��ȭ �Ѵ�
            minusheart = 0;
        }
    }
}
