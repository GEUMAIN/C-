using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriterEffect9 : MonoBehaviour
{
    public TypeWriterEffect10 script10Instance;

    public TMP_Text Txt;
    public TMP_Text timerText;

    string dialogue;

    public string[] Txtdialogue;

    public string[] dialogues;

    public int talkNum4;

    private bool isTyping = false;

    public Button answer16;
    public Button answer17;
    public Button answer18;

    public Button answer19;
    public Button answer20;
    public Button answer21;

    public Button answer22;
    public Button answer23;
    public Button answer24;

    public Button answer25;
    public Button answer26;
    public Button answer27;

    public Button answer28;
    public Button answer29;
    public Button answer30;

    public GameObject Answer6;
    public GameObject Answer7;
    public GameObject Answer8;
    public GameObject Answer9;
    public GameObject Answer10;

    public GameObject nextAnswer;

    public GameObject gamescreen;

    public GameObject useranswer;

    public GameObject clear;
    public GameObject fail;

    public GameObject problem;
    public GameObject problem1;
    public GameObject problem2;
    public GameObject problem3;
    public GameObject problem4;

    public GameObject hindrance3;
    public GameObject hindbutton2;

    public GameObject sound;

    public AudioSource audioSource;
    public AudioClip failSound;
    public AudioSource audioSource2;
    public AudioClip clearSound;

    private AudioSource previousSound;

    public int canswer;
    public int list;

    public float time;

    public void Start()
    {
        //���
        //dialogue = "�ȳ��Ͻʴϱ�.";
        //StartCoroutine(Typing(dialogue));
        Answer6.SetActive(true);
        answer16.onClick.AddListener(() => CheckAnswer(16));
        answer17.onClick.AddListener(() => CheckAnswer(17));
        answer18.onClick.AddListener(() => CheckAnswer(18));

        answer19.onClick.AddListener(() => CheckAnswer(19));
        answer20.onClick.AddListener(() => CheckAnswer(20));
        answer21.onClick.AddListener(() => CheckAnswer(21));

        answer22.onClick.AddListener(() => CheckAnswer(22));
        answer23.onClick.AddListener(() => CheckAnswer(23));
        answer24.onClick.AddListener(() => CheckAnswer(24));

        answer25.onClick.AddListener(() => CheckAnswer(25));
        answer26.onClick.AddListener(() => CheckAnswer(26));
        answer27.onClick.AddListener(() => CheckAnswer(27));

        answer28.onClick.AddListener(() => CheckAnswer(28));
        answer29.onClick.AddListener(() => CheckAnswer(29));
        answer30.onClick.AddListener(() => CheckAnswer(30));
        canswer = 17;
        time = 20;
        list = 0;
    }
    public void CheckAnswer(int selectedButton)
    {
        if (selectedButton == canswer)
        {
            if (previousSound != null)
                previousSound.Stop();

            clear.SetActive(true);
            // ���� ȿ���� ���
            if (audioSource2 != null && clearSound != null)
            {
                audioSource2.PlayOneShot(clearSound);
                // ������ ����� �Ҹ� ������ ���� �Ҹ��� ����
                previousSound = audioSource2;
            }
            switch (list)
            {
                case 0:
                    Invoke("exam2", 1f);
                    break;
                case 1:
                    Invoke("exam3", 1f);
                    break;
                case 2:
                    Invoke("exam4", 1f);
                    break;
                case 3:
                    Invoke("exam5", 1f);
                    break;
                case 4:
                    Invoke("end", 2f);
                    break;
            }
            StartCoroutine(HideClearObject());
            list++;
            time = 20;
            SHOP.currentMoney += 30;
            problemnum.clearproblemnum += 1;
        }
        else
        {
            // ������ ����� �Ҹ��� �ִٸ� ������Ŵ
            if (previousSound != null)
                previousSound.Stop();

            fail.SetActive(true);
            // ���� ȿ���� ���
            if (audioSource != null && failSound != null)
            {
                audioSource.PlayOneShot(failSound);
                // ������ ����� �Ҹ� ������ ���� �Ҹ��� ����
                previousSound = audioSource;
            }
            switch (list)
            {
                case 0:
                    Invoke("exam2", 1f);
                    break;
                case 1:
                    Invoke("exam3", 1f);
                    break;
                case 2:
                    Invoke("exam4", 1f);
                    break;
                case 3:
                    Invoke("exam5", 1f);
                    break;
                case 4:
                    Invoke("end", 2f);
                    break;
            }
            StartCoroutine(HideFailObject());
            list++;
            time = 20;
            problemnum.failproblemnum += 1;
        }
    }
    public void end()
    {
        Answer6.SetActive(false);
        Answer7.SetActive(false);
        Answer8.SetActive(false);
        Answer9.SetActive(false);
        Answer10.SetActive(false);
        problem.SetActive(false);
        problem1.SetActive(false);
        problem2.SetActive(false);
        problem3.SetActive(false);
        problem4.SetActive(false);
        useranswer.SetActive(true);
        nextAnswer.SetActive(true);
        sound.SetActive(true);
        this.enabled = false;
        script10Instance.enabled = true;
        script10Instance.StartTalk(script10Instance.Txtdialogue);
    }
    public void exam2()
    {
        Answer6.SetActive(false);
        Answer7.SetActive(true);
        problem.SetActive(false);
        problem1.SetActive(true);
        canswer = 20;
    }
    public void exam3()
    {
        Answer7.SetActive(false);
        Answer8.SetActive(true);
        problem1.SetActive(false);
        problem2.SetActive(true);
        Hind();
        hindbutton2.SetActive(true);
        canswer = 24;
    }
    public void exam4()
    {
        Answer8.SetActive(false);
        Answer9.SetActive(true);
        problem2.SetActive(false);
        problem3.SetActive(true);
        canswer = 25;
    }
    public void exam5()
    {
        Answer9.SetActive(false);
        Answer10.SetActive(true);
        problem3.SetActive(false);
        problem4.SetActive(true);
        canswer = 28;
    }
    public void Hind()
    {
        hindrance3.SetActive(true);
    }
    private IEnumerator HideFailObject()
    {
        // 1�� ���
        yield return new WaitForSeconds(1f);

        // fail ������Ʈ�� ��Ȱ��ȭ
        fail.SetActive(false);
    }
    private IEnumerator HideClearObject()
    {
        // 1�� ���
        yield return new WaitForSeconds(1f);

        // fail ������Ʈ�� ��Ȱ��ȭ
        clear.SetActive(false);
    }
    private void UpdateTimeUI()
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (timerText != null)
        {
            timerText.text = "�ð�: " + formattedTime;
        }
        if (time < 0)
        {
            CheckAnswer(canswer);
        }
    }

    private void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            UpdateTimeUI();
        }
        if (SHOP.hind2out == true)
        {
            hindrance3.SetActive(false);

        }
    }

    IEnumerator Typing(string talk)
    {
        //�ؽ�Ʈ�� null������ ����
        Txt.text = null;

        //���� �� ���̸� �ٹٲ����� �ٲ��ֱ�
        if (talk.Contains("  ")) talk = talk.Replace(" ", "\n");

        for (int i = 0; i < talk.Length; i++)
        {
            Txt.text += talk[i];
            //�ӵ�
            yield return new WaitForSeconds(0.05f);
        }
        //���� ��� ������
        yield return new WaitForSeconds(1.5f);
        isTyping = false;
        NextTalk();
    }
    public void StartTalk(string[] talks)
    {
        if (!isTyping)
        {
            isTyping = true;
            dialogues = talks;
            StartCoroutine(Typing(dialogues[talkNum4]));
        }
    }
    public void NextTalk()
    {
        if (!isTyping)
        {
            isTyping = true;
            Txt.text = null;
            talkNum4++;

            if (talkNum4 == dialogues.Length)
            {
                EndTalk();
                return;
            }

            StartCoroutine(Typing(dialogues[talkNum4]));
        }
    }
    public void EndTalk()
    {
        //talkNum �ʱ�ȭ
        talkNum4 = 0;
        Debug.Log("��� ��");
        this.enabled = false;
    }
}
