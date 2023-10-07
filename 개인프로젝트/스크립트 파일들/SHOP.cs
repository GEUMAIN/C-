using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SHOP : MonoBehaviour
{
    public GameObject shop;
    public AudioClip hoverSound; // 마우스를 가져다 대면 재생할 소리 파일
    private AudioSource audioSource; // 오디오 소스

    public Text moneyText; // 돈을 표시할 UI Text 요소
    public static int currentMoney = 1000; // 현재 돈
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
        // 오디오 소스 컴포넌트 가져오기
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // 오디오 소스가 없다면 새로 추가
            audioSource = gameObject.AddComponent<AudioSource>();
            // 초기 돈 설정
        }
        // 오디오 클립 설정
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
        // 오디오 소스 재생
        if (audioSource != null && hoverSound != null)
        {
            audioSource.Play();
        }
    }
    private void UpdateMoneyText()
    {
        if (moneyText != null)
        {
            moneyText.text = "보유 금액: $" + currentMoney.ToString(); // 텍스트 업데이트
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
            Debug.Log("돈이 부족합니다.");
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
            Debug.Log("돈이 부족합니다.");
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
            Debug.Log("돈이 부족합니다.");
        }
    }
}
