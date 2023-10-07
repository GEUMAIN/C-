using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypeWriterEffect : MonoBehaviour
{
    public TypeWriterEffect script1Instance;
    public TypeWriterEffect2 script2Instance;
    public TypeWriterEffect3 script3Instance;
    public TypeWriterEffect4 script4Instance;
    public TypeWriterEffect5 script5Instance;
    public TypeWriterEffect6 script6Instance;
    public TypeWriterEffect7 script7Instance;
    public TypeWriterEffect8 script8Instance;
    public TypeWriterEffect9 script9Instance;
    public TypeWriterEffect10 script10Instance;
    public TypeWriterEffect11 script11Instance;
    public TypeWriterEffect12 script12Instance;
    public TypeWriterEffect13 script13Instance;
    public problemnum script14Instance;
    public TypeWriterEffect14 script15Instance;
    public TypeWriterEffect15 script16Instance;

    public TMP_Text Txt;

    string dialogue;

    public string[] Txtdialogue;

    public string[] dialogues;

    public int talkNum;

    public GameObject openscreen;
    public GameObject gamecreen;
    public GameObject text1;
    public GameObject shop;

    public void Awake()
    {
        script2Instance.enabled = false;
        script3Instance.enabled = false;
        script4Instance.enabled = false;
        script5Instance.enabled = false;
        script6Instance.enabled = false;
        script7Instance.enabled = false;
        script8Instance.enabled = false;
        script9Instance.enabled = false;
        script10Instance.enabled = false;
        script11Instance.enabled = false;
        script12Instance.enabled = false;
        script13Instance.enabled = false;
        script14Instance.enabled = false;
        script15Instance.enabled = false;
        script16Instance.enabled = false;
        shop.SetActive(false);
    }
    public void Start()
    {
        //대사
        //dialogue = "안녕하십니까.";
        //StartCoroutine(Typing(dialogue));
        StartTalk(Txtdialogue);
        if (script2Instance != null && script2Instance.endtext == 0)
        {
            script2Instance.StartTalk(script2Instance.Txtdialogue);
        }

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
        NextTalk();
    }
    public void StartTalk(string[] talks)
    {
        dialogues = talks;

        //talkNum 번째 대사 출력
        StartCoroutine(Typing(dialogues[talkNum]));
    }
    public void NextTalk()
    {
        Txt.text = null;
        //다음 배열을 출력하기 위해 +1
        talkNum++;

        //talkNum이 배열의 길이랑 같으면 끝내기
        if(talkNum == dialogues.Length)
        {
            EndTalk();
            // 대사가 끝났을 때 endtext 값을 변경
            if (script2Instance != null)
            {
                script2Instance.endtext = 0;
            }
            return;
        }

        StartCoroutine(Typing(dialogues[talkNum]));
    }
    public void EndTalk()
    {
        //talkNum 초기화
        talkNum = 0;
        Debug.Log("대사 끝");
        openscreen.SetActive(false);
        gamecreen.SetActive(true);
        text1.SetActive(true);
        script1Instance.enabled = false;
        shop.SetActive(true);
        if (script2Instance != null)
        {
            script2Instance.enabled = true;
            script2Instance.StartTalk(script2Instance.Txtdialogue);
        }
        this.enabled = false;
    }
}
