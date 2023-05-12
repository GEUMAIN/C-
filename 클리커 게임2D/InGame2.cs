using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InGame2 : MonoBehaviour
{
    public int myNum; //Ŭ���Ҷ����� �þ ���ھ� �����
    public int moneyNum; //�׸��� �ٲ� ���ھ� �����
    public GameObject money1; //���� ������Ʈ money1 �����(�̹��� money1�� �� ���ӿ�����Ʈ�� ����)
    public GameObject money2; //���� ������Ʈ money2 �����(�̹��� money2�� �� ���ӿ�����Ʈ�� ����)
    public Text numTxt; //���� ���ھ �˷��ִ� text�����
    public int levelup; //�������Ҷ� �߰��� ���� ��� ���ִ°�
    public GameObject shop; //���� ������Ʈ
    public Text Numlevel; //���� ���� ���¸� �˷���
    public int price; //������ �Ҷ� ����
    public GameObject Level_Up; //�������� �ϱ� ���� ������Ʈ
    public GameObject Close; //������ �ݱ� ���� ������Ʈ

    void Start()
    {
        moneyNum = 100; //100�϶� �׸��� �ٲ�
        numTxt.text = "0"; //���� ���ھ 0���� ��Ÿ����
        price = 100; //�������Ҷ� �ʿ��� ��
        levelup = 0; //���� ����
        myNum = 0; //�� ��
        Numlevel.text ="����" + "0"; //������ �� ���¸� �����ֱ�
    }
    public void Click()
    {
        Debug.Log("Click!"); //Ŭ���϶�� ����ϱ�
        myNum += levelup +1; //Ŭ���Ҷ����� ���� �����ٰ� 1 ���ϱ�
        numTxt.text = myNum.ToString(); //String ���ڿ��� ��ȯ�ϱ�
        Debug.Log("���� �� :" + myNum); //���� �� ���¸� �˷��ֱ�
        if (myNum == moneyNum) //���� ���� ���ھ��ϰ� �ٲ� ���ھ ���ٸ� �̹����� �ٲ۴�
        {
            money2.SetActive(true); //money2�׸��� Ȱ��ȭ�Ѵ�
            money1.SetActive(false); //money1�׸��� ��Ȱ��ȭ �Ѵ�
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
            Debug.Log("���� �����մϴ�"); //���� �ƴ϶�� ���� �����ϴٰ� ����� ���
        }
    }
    public void shop_close()
    {
        Level_Up.SetActive(false); //�ݱ� ������Ʈ�� ������ ������ â ��Ȱ��ȭ
        Close.SetActive(false); //�ݱ� ������Ʈ�� ������ �ݱ� â ��Ȱ��ȭ
    }

}
