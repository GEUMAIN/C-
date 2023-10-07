using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypeWriterEffect13 : MonoBehaviour
{
    public TypeWriterEffect8 script8Instance;

    public TMP_Text Txt;

    string dialogue;

    public string[] Txtdialogue;

    public string[] dialogues;

    public int talkNum4;

    private bool isTyping = false;

    public GameObject shop;

    public void Start()
    {
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
        if (!isTyping && talks != null && talks.Length > 0)
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
        //talkNum 초기화
        talkNum4 = 0;
        Debug.Log("대사 끝");
        Application.Quit(); //게임종료
    }
}
