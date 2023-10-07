using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriterEffect6:MonoBehaviour
{
    public TypeWriterEffect6 script6Instance;
    public TypeWriterEffect7 script7Instance;
    public Text userInputText; // 사용자 입력을 보여줄 Text

    public TMP_Text Txt;
    public TMP_Text timerText;
    public TMP_Text time2Text;
    public TMP_Text time3Text;

    public bool script5Finished = false;

    string dialogue;

    public string[] Txtdialogue;

    public string[] dialogues;

    private Vector2 direction; // 랜덤한 이동 방향

    public int talkNum5;
    public int endtext;
    public int list;

    public float time;

    public float time2;
    public float time3;

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
    public GameObject timetext;
    public GameObject timetext2;
    public GameObject gamesceen;
    public GameObject opensceen;
    public GameObject hindrance3;
    public GameObject hindbutton2;
    public GameObject Timetext;
    public GameObject tip;
    public GameObject tip2;
    public GameObject hind;

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
                    Invoke("exam2", 5f);
                    break;
                case 1:
                    Invoke("exam3", 5f);
                    break;
                case 2:
                    Invoke("end", 2f);
                    break;

            }
            userInput = "";
            StartCoroutine(HideClearObject());
            list += 1;
            time = 60;
            time2 = 5;
            time3 = 10;
            endtext++;
            SHOP.currentMoney += 30;
            problemnum.clearproblemnum += 1;
            timetext.SetActive(true);
            timetext2.SetActive(false);
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
                    Invoke("exam2", 5f);
                    break;
                case 1:
                    Invoke("exam3", 5f);
                    break;
                case 2:
                    Invoke("end", 2f);
                    break;
            }
            userInput = "";
            StartCoroutine(HideFailObject());
            list += 1;
            time = 60;
            time2 = 5;
            time3 = 10;
            problemnum.failproblemnum += 1;
            endtext++;
            timetext.SetActive(true);
            timetext2.SetActive(false);
        }
    }
    public void exam2()
    {
        problem2.SetActive(true);
        clear.SetActive(false);
        fail.SetActive(false);
        Hind();
        hindbutton2.SetActive(true);
    }
    public void exam3()
    {
        problem3.SetActive(true);
        clear.SetActive(false);
        fail.SetActive(false);
    }
    public void end()
    {
        SHOP.hind1out = false;
        SHOP.hind2out = false;
        SHOP.hind3out = false;
        problem.SetActive(false);
        problem2.SetActive(false);
        problem3.SetActive(false);
        gamesceen.SetActive(false);
        opensceen.SetActive(true);
        tip.SetActive(false);
        tip2.SetActive(true);
        timetext.SetActive(false);
        timetext2.SetActive(false);
        this.enabled = false;
        script7Instance.enabled = true;
        script7Instance.StartTalk(script7Instance.Txtdialogue);
    }
    public void Hind()
    {
        hindrance3.SetActive(true);
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
            correctAnswer = "SIMPLE";
        }
        if(SHOP.hind2out == true)
        {
            hindrance3.SetActive(false);

        }
        DetectUserInput();
        time -= Time.deltaTime;
        time2 -= Time.deltaTime;
        if (time2 < 0)
        {
            timetext.SetActive(false);
            timetext2.SetActive(true);
            time3 -= Time.deltaTime;
        }
        UpdateTimeUI();
        
    }
    private void UpdateTimeUI()
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int seconds2 = Mathf.FloorToInt(time2 % 60);
        int seconds3 = Mathf.FloorToInt(time3 % 60);
        string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        string lefttime = string.Format("{0:00}",seconds2);
        string endtime = string.Format("{0:00}", seconds3);

        if (timerText != null)
        {
            timerText.text = "시간: " + formattedTime;
        }
        if (time2Text != null)
        {
            time2Text.text = lefttime;
        }
        if (time3Text != null)
        {
            time3Text.text = endtime;
        }
        if (time < 0)
        {
            CheckAnswer();
        }
        if (time3 < 0)
        {
            CheckAnswer();
        }
        if (time3 < 0)
        {
            problem.SetActive(false);
            problem2.SetActive(false);
            problem3.SetActive(false);
        }
    }
    private void DetectUserInput()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // 엔터 키를 누르면 정답 확인
            // 시간 초기화
            time = 30;
            CheckAnswer();
            if(correctAnswer == "SIMPLE")
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
            correctAnswer = "LEGEND";
            Invoke("firstexam", 5f);
        }
        start();
        list = 0;
        time = 60;
        time2 = 5;
        time3 = 10;
        Timetext.SetActive(false);
        hind.SetActive(false);
        hindcheck = false;
    }
    public void firstexam()
    {
        problem.SetActive(true);
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
