using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
public class InGame2 : MonoBehaviour
{
    public int myNum; //클릭할때마다 늘어날 스코어 만들기
    public int hpNum; //그림이 바뀔 스코어 만들기
    public GameObject picha1; //게임 오브젝트 money1 만들기(이미지 money1를 이 게임오브젝트에 적용)
    public GameObject picha2; //게임 오브젝트 money2 만들기(이미지 money2를 이 게임오브젝트에 적용)
    public Text numTxt; //현재 스코어를 알려주는 text만들기
    public int levelup; //레벨업할때 추가로 돈을 얻게 해주는것
    public GameObject shop; //상점 오브젝트
    public Text Numlevel; //현재 레벨 상태를 알려줌
    public int price; //레벨업 할때 가격
    public GameObject Level_Up; //레벨업을 하기 위한 오브젝트
    public GameObject Close; //상점을 닫기 위한 오브젝트
    public Text hptxt; //현재 피카츄 채력을 나타내는 것
    public int price2; //스킬1를 사는데 필요한 돈
    public GameObject skill1; //스킬 오브젝트1
    public GameObject skill2; //스킬 오브젝트2
    public int price3; //스킬2를 사는데 필요한 돈
    public GameObject verystrongskill; //스킬 오브젝트3
    public int price4; //필살기를 사는데 필요한 돈
    public GameObject Finish;
    void Start()
    {
        hptxt.text = "체력 : " +"1000000";
        hpNum = 1000000; //100일때 그림이 바뀜
        numTxt.text ="현재 돈 : "+"0"; //현재 스코어를 0으로 나타내기
        price = 100; //레벨업할때 필요한 돈
        price2 = 200; //스킬1 가격
        price3 = 500; //스킬2 가격
        price4 = 4000; //필살기 가격
        levelup = 0; //현재 레벨
        myNum = 0; //내 돈
        Numlevel.text ="레벨" + "0"; //레벨과 돈 상태를 보여주기
    }
    public void Click()
    {
        Debug.Log("Click!"); //클릭하라고 출력하기
        myNum += levelup +1; //클릭할때마다 레벨 값에다가 1 더하기
        hpNum -= 1; //피카츄의 체력 -1
        numTxt.text = myNum.ToString(); //String 문자열로 변환하기
        hptxt.text = hpNum.ToString(); //체력을 String 문자열로 변환
        Debug.Log("현재 돈 :" + myNum); //현재 돈 상태를 알려주기
        if (hpNum == 0) //만약 현재 스코어하고 바뀔 스코어가 같다면 이미지를 바꾼다
        {
            picha2.SetActive(true); // picha2그림을 활성화한다
            picha1.SetActive(false); //picha1그림을 비활성화 한다
            Finish.SetActive(true); //게임 끝 메세지를 나타내는 것
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
            Debug.Log("돈이 부족합니다 필요 돈 :"+(price -= myNum)); //만약 아니라면 돈이 부족하다고 디버그 출력
        }
    }
    public void shop_close()
    {
        Level_Up.SetActive(false); //닫기 오브젝트를 누르면 레벨업 창 비활성화
        Close.SetActive(false); //닫기 오브젝트를 누르면 닫기 창 비활성화
    }
    public void shop_skill1()
    {
        if (myNum >= price2)
        {
            myNum -= price2;
            hpNum -= 3000;
        }
        else
        {
            Debug.Log("돈이 부족합니다 필요 돈 :" + (price2 -= myNum));
        }
    }
    public void shop_skill2()
    {
        if (myNum >= price3)
        {
            myNum -= price3;
            hpNum -= 5000;
        }
        else
        {
            Debug.Log("돈이 부족합니다 필요 돈 :" + (price3 -= myNum));
        }
    }
    public void shop_skill3()
    {
        if (myNum >= price4)
        {
            myNum -= price4;
            hpNum -= 40000;
        }
        else
        {
            Debug.Log("돈이 부족합니다 필요 돈 :" + (price4 -= myNum));
        }
    }
}
