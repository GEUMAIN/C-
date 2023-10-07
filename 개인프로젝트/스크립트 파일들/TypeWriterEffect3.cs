using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypeWriterEffect3 : MonoBehaviour
{
    public TypeWriterEffect script1Instance;
    public TypeWriterEffect2 script2Instance;
    public TypeWriterEffect4 script4Instance;

    public TMP_Text Txt;

    string dialogue;

    public string[] Txtdialogue;

    public string[] dialogues;

    public int talkNum3;

    private bool isTyping = false;

    public GameObject openscreen;
    public GameObject gamescreen;
    public GameObject time;
    public GameObject exam;
    public GameObject answer;
    public GameObject shop;

    public void Start()
    {
        //대사
        //dialogue = "안녕하십니까.";
        //StartCoroutine(Typing(dialogue));
        StartTalk(Txtdialogue);
        shop.SetActive(false);
    }

     IEnumerator Typing(string talk)
    {
        //텍스트를 null값으로 변경
        Txt.text = null;

        //띄어쓰기 두 번이면 줄바꿈으로 바꿔주기
        if (talk.Contains("  ")) talk = talk.Replace(" ", "\n");

        for (int i = 0; i < talk.Length; i++)
        {
            Txt.text += talk[i];
            //속도
            yield return new WaitForSeconds(0.05f);
        }
        //다음 대사 딜레이
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
            StartCoroutine(Typing(dialogues[talkNum3]));
        }
    }
    public void NextTalk()
    {
        if (!isTyping)
        {
            isTyping = true;
            Txt.text = null;
            talkNum3++;

            if (talkNum3 == dialogues.Length)
            {
                EndTalk();
                return;
            }

            StartCoroutine(Typing(dialogues[talkNum3]));
        }
    }
    public void EndTalk()
    {
        //talkNum 초기화
        talkNum3 = 0;
        Debug.Log("대사 끝");
        script4Instance.enabled = true;
        openscreen.SetActive(false);
        gamescreen.SetActive(true);
        time.SetActive(true);
        exam.SetActive(true);
        answer.SetActive(false);
        shop.SetActive(true);
        this.enabled = false;
    }
}
