using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriterEffect8 : MonoBehaviour
{
    public TypeWriterEffect9 script9Instance;
    public Text userInputText; // 사용자 입력을 보여줄 Text

    public TMP_Text Txt;
    public TMP_Text timerText;

    public bool script5Finished = false;

    string dialogue;

    public string[] Txtdialogue;

    public string[] dialogues;


    public int talkNum5;
    public int endtext;
    public int list;

    public float time;

    public string correctAnswer; // 플레이어가 입력해야 하는 정답을 설정합니다.


    // 사용자의 입력을 저장할 변수
    private string userInput = "";

    public static bool hindcheck;

    public GameObject fail;//틀림
    public GameObject clear; //통과
    public GameObject problem;
    public GameObject problem2; //문제 2번
    public GameObject problem3;
    public GameObject timertext;
    public GameObject gamesceen;
    public GameObject opensceen;
    public GameObject hindrance4;
    public GameObject hindbutton;
    public GameObject hindbutton2;
    public GameObject hindbutton3;
    public GameObject sound;
    public GameObject nextexam;

    public AudioSource audioSource;
    public AudioClip failSound;
    public AudioSource audioSource2;
    public AudioClip clearSound;

    private AudioSource previousSound;

    // 이전 프레임에서 입력한 키를 기록할 변수
    private string previousInput = "";

    // 정답을 감지하는 함수

    private void CheckAnswer()
    {
        Debug.Log("User Input: " + userInput.ToLower());
        Debug.Log("Correct Answer: " + correctAnswer.ToLower());
        // 플레이어의 입력과 정답이 일치하면 다음 대사로 넘어가기
        if (userInput.ToLower() == correctAnswer.ToLower())
        {
            // 이전에 재생한 소리가 있다면 정지시킴
            if (previousSound != null)
                previousSound.Stop();

            clear.SetActive(true);
            // 성공 효과음 재생
            if (audioSource2 != null && clearSound != null)
            {
                audioSource2.PlayOneShot(clearSound);
                // 이전에 재생한 소리 변수에 현재 소리를 저장
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
                    Invoke("end", 2f);
                    break;

            }
            userInput = "";
            StartCoroutine(HideClearObject());
            list += 1;
            time = 20;
            problemnum.clearproblemnum += 1;
            endtext++;
            SHOP.currentMoney += 30;
        }
        else
        {
            // 이전에 재생한 소리가 있다면 정지시킴
            if (previousSound != null)
                previousSound.Stop();

            fail.SetActive(true);
            // 실패 효과음 재생
            if (audioSource != null && failSound != null)
            {
                audioSource.PlayOneShot(failSound);
                // 이전에 재생한 소리 변수에 현재 소리를 저장
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
                    Invoke("end", 2f);
                    break;
            }
            userInput = "";
            StartCoroutine(HideFailObject());
            list += 1;
            time = 20;
            problemnum.failproblemnum += 1;
            endtext++;
        }
    }
    public void exam2()
    {
        problem.SetActive(false);
        problem2.SetActive(true);
        clear.SetActive(false);
        fail.SetActive(false);
        Hind();
        hindbutton.SetActive(true);
        sound.SetActive(false);
    }
    public void exam3()
    {
        problem2.SetActive(false);
        problem3.SetActive(true);
        clear.SetActive(false);
        fail.SetActive(false);

    }
    public void end()
    {
        problem.SetActive(false);
        problem2.SetActive(false);
        problem3.SetActive(false);
        sound.SetActive(true);
        nextexam.SetActive(true);
        this.enabled = false;
        script9Instance.enabled = true;
        script9Instance.StartTalk(script9Instance.Txtdialogue);
    }
    public void Hind()
    {
        hindrance4.SetActive(true);
    }
    private void Update()
    {
        // 프레임마다 사용자의 입력을 감지하여 UI를 업데이트
        if (endtext == 1)
        {
            correctAnswer = "THANKS";
        }
        if (endtext == 2)
        {
            correctAnswer = "LEGEND";
        }
        if(SHOP.hind3out == true)
        {
            hindrance4.SetActive(false);

        }
        DetectUserInput();
        time -= Time.deltaTime;
        UpdateTimeUI();
        
    }
    private void UpdateTimeUI()
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (timerText != null)
        {
            timerText.text = "시간: " + formattedTime;
        }
        if (time < 0)
        {
            CheckAnswer();
        }
    }
    private void DetectUserInput()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // 엔터 키를 누르면 정답 확인
            // 시간 초기화
            time = 20;
            CheckAnswer();
            if(correctAnswer == "LEGEND")
            {
                end();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            // 백스페이스 키를 누른 경우, 마지막에 입력한 글자 삭제
            if (userInput.Length > 0)
            {
                userInput = userInput.Substring(0, userInput.Length - 1);
                // 사용자의 입력이 변경될 때마다 UI 업데이트
                UpdateUserInputText();
            }
        }
        else if (Input.inputString.Length > 0)
        {
            // 이전 프레임에서 입력한 키와 다른 키 입력이 있는 경우에만 사용자 입력에 추가
            if (Input.inputString != previousInput)
            {
                userInput += Input.inputString;
                // 사용자의 입력이 변경될 때마다 UI 업데이트
                UpdateUserInputText();
            }
            // 이전 입력을 현재 입력으로 업데이트
            previousInput = Input.inputString;
        }
        // 키를 떼었을 때의 처리 (이 부분은 불필요하므로 삭제합니다)
    }

    private string GetKeyInputAsString()
    {
        // 모든 키를 순회하며 떼어진 키를 찾아서 해당 키를 문자열로 반환합니다.
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyUp(keyCode))
            {
                return keyCode.ToString();
            }
        }

        return ""; // 떼어진 키가 없으면 빈 문자열을 반환
    }

    private IEnumerator HideFailObject()
    {
        // 1초 대기
        yield return new WaitForSeconds(1f);

        // fail 오브젝트를 비활성화
        fail.SetActive(false);
    }
    private IEnumerator HideClearObject()
    {
        // 1초 대기
        yield return new WaitForSeconds(1f);

        // fail 오브젝트를 비활성화
        clear.SetActive(false);
    }

    public void Start()
    {
        if (userInputText != null)
        {
            userInputText.text = "";
        }
        //대사
        //dialogue = "안녕하십니까.";
        //StartCoroutine(Typing(dialogue));
        if(endtext == 0)
        {
            correctAnswer = "SIMPLE";
        }
        hindbutton.SetActive(false);
        hindbutton2.SetActive(false);
        hindbutton3.SetActive(false);
        start();
        list = 0;
        time = 20;
        hindcheck = false;
    }

    private void UpdateUserInputText()
    {
        userInputText.text = userInput;
    }

    public void start()
    {
        if (endtext == 1)
        {
            StartTalk(Txtdialogue);
            Debug.Log("정상작동함");
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
        // 사용자 입력 받기
        if (talkNum5 == 1)
        {
            yield return StartCoroutine(GetUserInput());
            Debug.Log("정상작동함");
        }
    }
    private IEnumerator GetUserInput()
    {
        // 사용자의 입력을 초기화
        userInput = "";
        // 입력 종료 조건 변수
        bool inputEnded = false;

        while (!inputEnded)
        {
            // 사용자 입력을 감지하면 입력 종료
            if (Input.GetKeyDown(KeyCode.Return))
            {
                inputEnded = true;
            }
            else if (Input.inputString.Length > 0)
            {
                // 키 입력이 있는 경우 사용자 입력에 추가
                userInput += Input.inputString;
                // 사용자의 입력이 변경될 때마다 UI 업데이트
                UpdateUserInputText();
            }

            // 0보다 작거나 같으면 입력 시간이 종료된 것으로 처리
            if (time <= 0)
            {
                inputEnded = true;
            }

            yield return null;
        }

        // 시간이 다 되었을 경우 자동으로 틀린 것으로 처리
        if (time <= 0)
        {
            userInput = ""; // 입력 초기화
            fail.SetActive(true);
            StartCoroutine(HideFailObject());
            NextTalk();
        }
        else
        {
            // 정답 확인
            CheckAnswer();
        }
    }

    public void StartTalk(string[] talks)
    {
        dialogues = talks;

        //talkNum 번째 대사 출력
        StartCoroutine(Typing(dialogues[talkNum5]));
    }
    public void NextTalk()
    {
        Txt.text = null;
        //다음 배열을 출력하기 위해 +1
        talkNum5++;

        //talkNum이 배열의 길이랑 같으면 끝내기
        if (talkNum5 == dialogues.Length)
        {
            EndTalk();
            return;
        }
        // talkNum이 1일 때 멈추도록
        if (talkNum5 == 1)
        {
            return;
        }

        StartCoroutine(Typing(dialogues[talkNum5]));
    }
    public void EndTalk()
    {
        //talkNum 초기화
        talkNum5 = 0;
        Debug.Log("대사 끝");
    }
}
