using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed; //속도
    public float power;
    public float maxShotDelay; //발사 딜레이 변수 최대
    public float curShotDelay; //현재

    public bool isTouchTop; //위에 닿았는가?
    public bool isTouchBottom; //아래에 닿았는가?
    public bool isTouchLeft; //왼쪽에 닿았는가?
    public bool isTouchRight; //오른쪽에 닿았는가?

    public GameObject bulletobjA;
    public GameObject bulletobjB;

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        Move();
        Fire();
        Reload();
        if (Input.GetKeyDown(KeyCode.Keypad1)) //콘솔 키패드1을 누르면 레벨1로 변경
        {
            power = 1;

        }
        if (Input.GetKeyDown(KeyCode.Keypad2)) //콘솔 키패드2을 누르면 레벨2로 변경
        {
            power = 2;

        }
        if (Input.GetKeyDown(KeyCode.Keypad3)) //콘솔 키패드3을 누르면 레벨3으로 변경
        {
            power = 3;

        }
    }
    void Move()
    {
        //플레이어 움직이기
        float h = Input.GetAxisRaw("Horizontal"); //수평
        if ((isTouchRight == true && h == -1) || (isTouchLeft == true && h == 1)) //만약 h가 1이거나 Right가 true라면 or 만약 h가 -1이거나 Left가 true라면
            h = 0;
        float v = Input.GetAxisRaw("Vertical"); //수직
        if ((isTouchTop == true && v == 1) || (isTouchBottom == true && v == -1)) //만약 v가 1이거나 Top가 true라면 or 만약 v가 -1이거나 Bottom가 true라면
            v = 0;
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime; //!!transform은 반드시 Time.deltaTime을 곱해줄 것!(물리적 이동은 상관 x)
        transform.position = curPos + nextPos;


        if (Input.GetButtonDown("Horizontal") ||
            Input.GetButtonUp("Horizontal"))
        {
            anim.SetInteger("Input", (int)h);
        }
    }
    void Fire()
    {
        //총알 쏘기
        if (!Input.GetButton("Fire1")) //fire1버튼을 눌렀다면?
        {
            return;
        }
        if(curShotDelay < maxShotDelay) //현재 장전수가 최대보다 넘지 않았다면?
        {
            return;
        }
        switch (power){
            case 1: //레벨1
                //파워가 하나다
                GameObject bullet = Instantiate(bulletobjA, transform.position, transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>(); //힘주기
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);//위 방향으로 속도*10만큼 나아가게
                break;
            case 2: //레벨2
                GameObject bulletR = Instantiate(bulletobjA, transform.position+Vector3.right*0.1f, transform.rotation);
                GameObject bulletL = Instantiate(bulletobjA, transform.position+Vector3.left*0.1f, transform.rotation);
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>(); //오른쪽 힘주기
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>(); //왼쪽 힘주기
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);//위 방향으로 속도*10만큼 나아가게
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);//위 방향으로 속도*10만큼 나아가게
                break;
            case 3: //레벨3
                GameObject bulletRR = Instantiate(bulletobjA, transform.position + Vector3.right * 0.25f, transform.rotation);
                GameObject bulletCC = Instantiate(bulletobjB, transform.position, transform.rotation); //혼자 다르게 큰 총알 쏘기
                GameObject bulletLL = Instantiate(bulletobjA, transform.position + Vector3.left * 0.25f, transform.rotation);
                Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>(); //오른쪽 힘주기
                Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>(); //가운데 힘주기
                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>(); //왼쪽 힘주기
                rigidRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);//위 방향으로 속도*10만큼 나아가게
                rigidCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);//위 방향으로 속도*10만큼 나아가게
                rigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);//위 방향으로 속도*10만큼 나아가게
                break;
        }

        curShotDelay = 0; //총알을 쏘고 0으로 초기화 해주기
    }
    void Reload()
    {
        curShotDelay += Time.deltaTime;

    }

    void OnTriggerEnter2D(Collider2D collision) //경계선에 들어왔다면
    {
        if(collision.gameObject.tag == "Border") //만약 Border라는 태그를 가진 게임 오브젝트에 닿았으면
        {
            //게임 오브젝트 top bottom left right에 닿았다면 isTouch들을 true로 바꿔준다
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision) //경계선에서 나왔다면
    {
        if (collision.gameObject.tag == "Border") //만약 Border라는 태그를 가진 게임 오브젝트에 닿았으면
        {
            //게임 오브젝트 top bottom left right에 닿았다면 isTouch들을 false로 바꿔준다
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
            }
        }
    }
}
