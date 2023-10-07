using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SHOP : MonoBehaviour
{
    public GameObject shop;
    public AudioClip hoverSound; // ���콺�� ������ ��� ����� �Ҹ� ����
    private AudioSource audioSource; // ����� �ҽ�

    public Text moneyText; // ���� ǥ���� UI Text ���
    public static int currentMoney = 1000; // ���� ��
    public int price1;
    public int price2;
    public int price3;

    public static bool hind1out;
    public static bool hind2out;
    public static bool hind3out;

    public void shopopen()
    {
        shop.SetActive(true);
    }
    public void shopclose()
    {
        shop.SetActive(false);
    }
    private void Start()
    {
        // ����� �ҽ� ������Ʈ ��������
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // ����� �ҽ��� ���ٸ� ���� �߰�
            audioSource = gameObject.AddComponent<AudioSource>();
            // �ʱ� �� ����
        }
        // ����� Ŭ�� ����
        audioSource.clip = hoverSound;
        hind1out = false;
        price1 = 1100;
        price2 = 1050;
        price3 = 2000;
    }
    private void Update()
    {
        UpdateMoneyText();
    }
    public void OnMouseEnter()
    {
        // ����� �ҽ� ���
        if (audioSource != null && hoverSound != null)
        {
            audioSource.Play();
        }
    }
    private void UpdateMoneyText()
    {
        if (moneyText != null)
        {
            moneyText.text = "���� �ݾ�: $" + currentMoney.ToString(); // �ؽ�Ʈ ������Ʈ
        }
    }
    public void hindout()
    {
        if(currentMoney >= price1)
        {
            hind1out = true;
            currentMoney -= price1;
        }
        else
        {
            Debug.Log("���� �����մϴ�.");
        }
    }
    public void hindout2()
    {
        if (currentMoney >= price2)
        {
            hind2out = true;
            currentMoney -= price2;
        }
        else
        {
            Debug.Log("���� �����մϴ�.");
        }
    }
    public void hindout3()
    {
        if (currentMoney >= price3)
        {
            hind3out = true;
            currentMoney -= price3;
        }
        else
        {
            Debug.Log("���� �����մϴ�.");
        }
    }
}
