using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InGame2 : MonoBehaviour
{
    public int myNum; //클릭할때마다 늘어날 스코어 만들기
    public int moneyNum; //그림이 바뀔 스코어 만들기
    public GameObject money1; //게임 오브젝트 money1 만들기(이미지 money1를 이 게임오브젝트에 적용)
    public GameObject money2; //게임 오브젝트 money2 만들기(이미지 money2를 이 게임오브젝트에 적용)
    public Text numTxt; //현재 스코어를 알려주는 text만들기
    public int levelup; //레벨업할때 추가로 돈을 얻게 해주는것
    public GameObject shop; //상점 오브젝트
    public Text Numlevel; //현재 레벨 상태를 알려줌
    public int price; //레벨업 할때 가격
    public GameObject Level_Up; //레벨업을 하기 위한 오브젝트
    public GameObject Close; //상점을 닫기 위한 오브젝트

    void Start()
    {
        moneyNum = 100; //100일때 그림이 바뀜
        numTxt.text = "0"; //현재 스코어를 0으로 나타내기
        price = 100; //레벨업할때 필요한 돈
        levelup = 0; //현재 레벨
        myNum = 0; //내 돈
        Numlevel.text ="레벨" + "0"; //레벨과 돈 상태를 보여주기
    }
    public void Click()
    {
        Debug.Log("Click!"); //클릭하라고 출력하기
        myNum += levelup +1; //클릭할때마다 레벨 값에다가 1 더하기
        numTxt.text = myNum.ToString(); //String 문자열로 변환하기
        Debug.Log("현재 돈 :" + myNum); //현재 돈 상태를 알려주기
        if (myNum == moneyNum) //만약 현재 스코어하고 바뀔 스코어가 같다면 이미지를 바꾼다
        {
            money2.SetActive(true); //money2그림을 활성화한다
            money1.SetActive(false); //money1그림을 비활성화 한다
        }

    }
    public void ShopClick()
    {
        Level_Up.SetActive(true); //레벨업 창을 활성화한다
        Close.SetActive(true); //레벨업 창을 비활성화 한다
    }

    public void ShopLevel()
    {
        if (myNum >= price) //만약 클릭했을때 내 돈이 필요한 돈보다 많다면
        {
            myNum -= price; //돈-가격을 하여 레벨업을 한다
            levelup++; //레벨 수치 +1
            Numlevel.text = levelup.ToString(); //레벨 수치를 string으로  변환해서 띄운다
        }
        else
        {
            Debug.Log("돈이 부족합니다"); //만약 아니라면 돈이 부족하다고 디버그 출력
        }
    }
    public void shop_close()
    {
        Level_Up.SetActive(false); //닫기 오브젝트를 누르면 레벨업 창 비활성화
        Close.SetActive(false); //닫기 오브젝트를 누르면 닫기 창 비활성화
    }

}
