using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
public class InGame2 : MonoBehaviour
{
    public int myNum; //Ŭ���Ҷ����� �þ ���ھ� �����
    public int hpNum; //�׸��� �ٲ� ���ھ� �����
    public GameObject picha1; //���� ������Ʈ money1 �����(�̹��� money1�� �� ���ӿ�����Ʈ�� ����)
    public GameObject picha2; //���� ������Ʈ money2 �����(�̹��� money2�� �� ���ӿ�����Ʈ�� ����)
    public Text numTxt; //���� ���ھ �˷��ִ� text�����
    public int levelup; //�������Ҷ� �߰��� ���� ��� ���ִ°�
    public GameObject shop; //���� ������Ʈ
    public Text Numlevel; //���� ���� ���¸� �˷���
    public int price; //������ �Ҷ� ����
    public GameObject Level_Up; //�������� �ϱ� ���� ������Ʈ
    public GameObject Close; //������ �ݱ� ���� ������Ʈ
    public Text hptxt; //���� ��ī�� ä���� ��Ÿ���� ��
    public int price2; //��ų1�� ��µ� �ʿ��� ��
    public GameObject skill1; //��ų ������Ʈ1
    public GameObject skill2; //��ų ������Ʈ2
    public int price3; //��ų2�� ��µ� �ʿ��� ��
    public GameObject verystrongskill; //��ų ������Ʈ3
    public int price4; //�ʻ�⸦ ��µ� �ʿ��� ��
    public GameObject Finish;
    void Start()
    {
        hptxt.text = "ü�� : " +"1000000";
        hpNum = 1000000; //100�϶� �׸��� �ٲ�
        numTxt.text ="���� �� : "+"0"; //���� ���ھ 0���� ��Ÿ����
        price = 100; //�������Ҷ� �ʿ��� ��
        price2 = 200; //��ų1 ����
        price3 = 500; //��ų2 ����
        price4 = 4000; //�ʻ�� ����
        levelup = 0; //���� ����
        myNum = 0; //�� ��
        Numlevel.text ="����" + "0"; //������ �� ���¸� �����ֱ�
    }
    public void Click()
    {
        Debug.Log("Click!"); //Ŭ���϶�� ����ϱ�
        myNum += levelup +1; //Ŭ���Ҷ����� ���� �����ٰ� 1 ���ϱ�
        hpNum -= 1; //��ī���� ü�� -1
        numTxt.text = myNum.ToString(); //String ���ڿ��� ��ȯ�ϱ�
        hptxt.text = hpNum.ToString(); //ü���� String ���ڿ��� ��ȯ
        Debug.Log("���� �� :" + myNum); //���� �� ���¸� �˷��ֱ�
        if (hpNum == 0) //���� ���� ���ھ��ϰ� �ٲ� ���ھ ���ٸ� �̹����� �ٲ۴�
        {
            picha2.SetActive(true); // picha2�׸��� Ȱ��ȭ�Ѵ�
            picha1.SetActive(false); //picha1�׸��� ��Ȱ��ȭ �Ѵ�
            Finish.SetActive(true); //���� �� �޼����� ��Ÿ���� ��
        }

    }
    public void ShopClick()
    {
        Level_Up.SetActive(true); //������ â�� Ȱ��ȭ�Ѵ�
        Close.SetActive(true); //������ â�� ��Ȱ��ȭ �Ѵ�
    }

    public void ShopLevel()
    {
        if (myNum >= price) //���� Ŭ�������� �� ���� �ʿ��� ������ ���ٸ�
        {
            myNum -= price; //��-������ �Ͽ� �������� �Ѵ�
            levelup++; //���� ��ġ +1
            Numlevel.text = levelup.ToString(); //���� ��ġ�� string����  ��ȯ�ؼ� ����
        }
        else
        {
            Debug.Log("���� �����մϴ� �ʿ� �� :"+(price -= myNum)); //���� �ƴ϶�� ���� �����ϴٰ� ����� ���
        }
    }
    public void shop_close()
    {
        Level_Up.SetActive(false); //�ݱ� ������Ʈ�� ������ ������ â ��Ȱ��ȭ
        Close.SetActive(false); //�ݱ� ������Ʈ�� ������ �ݱ� â ��Ȱ��ȭ
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
            Debug.Log("���� �����մϴ� �ʿ� �� :" + (price2 -= myNum));
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
            Debug.Log("���� �����մϴ� �ʿ� �� :" + (price3 -= myNum));
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
            Debug.Log("���� �����մϴ� �ʿ� �� :" + (price4 -= myNum));
        }
    }
}
