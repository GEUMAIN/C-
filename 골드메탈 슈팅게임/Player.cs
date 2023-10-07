using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    public float speed; //�ӵ�
    public float power;
    public float maxShotDelay; //�߻� ������ ���� �ִ�
    public float curShotDelay; //����


    public bool isTouchTop; //���� ��Ҵ°�?
    public bool isTouchBottom; //�Ʒ��� ��Ҵ°�?
    public bool isTouchLeft; //���ʿ� ��Ҵ°�?
    public bool isTouchRight; //�����ʿ� ��Ҵ°�?

    public GameObject bulletobjA; //�Ѿ�A ������Ʈ
    public GameObject bulletobjB; //�Ѿ�B ������Ʈ
    public GameObject soulHeart1; //�ҿ���Ʈ1
    public GameObject soulHeart2; //�ҿ���Ʈ2
    public GameObject soulHeart3; //�ҿ���Ʈ3
    public GameObject soulHeart4; //�ҿ���Ʈ4
    public GameObject soulHeart5; //�ҿ���Ʈ5
    public GameObject heart1; //ü��
    public GameObject heart2; //ü��
    public GameObject heart3; //ü��
    public GameObject heart4; //ü��
    public GameObject heart5; //ü��

    public GameObject item1; //������1(���ɹ�)
    public GameObject item2; //������2(�Ǹ���1)
    public GameObject item3; //������3(��������ũ)
    public GameObject item4; //������4(�Ǹ���2)
    public GameObject soulheartitem; //�ҿ���Ʈ ������

    public int soulheart; //��ȣ�� ���� ��
    public int minussoulheart;
    public int maxsoulheart; //�ִ� �ҿ���Ʈ ��
    public static int plusdmg; //�߰� �Ǵ� ������

    public GameManager manager; //�Ŵ���

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        soulheart = 0; 
        maxsoulheart = 6;
        plusdmg = 0;
        minussoulheart = 0;
    }
    void Update()
    {
        Move();
        Fire();
        Reload();
        if (Input.GetKeyDown(KeyCode.Keypad1)) //�ܼ� Ű�е�1�� ������ ����1�� ����
        {
            Debug.Log("����1");
            power = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2)) //�ܼ� Ű�е�2�� ������ ����2�� ����
        {
            Debug.Log("����2");
            power = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3)) //�ܼ� Ű�е�3�� ������ ����3���� ����
        {
            Debug.Log("����3");
            power = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4)) //�ܼ� Ű�е�4�� ������ �ҿ���Ʈ �ϳ� �߰�
        {
            Debug.Log("�ҿ���Ʈ �߰�");
            soulheart = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5)) //�ܼ� Ű�е�5�� ������ �ҿ���Ʈ �ϳ� �� �߰�
        {
            Debug.Log("�ҿ���Ʈ �߰�");
            soulheart = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6)) //�ܼ� Ű�е�6�� ������ �ҿ���Ʈ �ϳ� �� �߰�
        {
            Debug.Log("�ҿ���Ʈ �߰�");
            soulheart = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad7)) //�ܼ� Ű�е�7�� ������ �ҿ���Ʈ �ϳ� �� �߰�
        {
            Debug.Log("�ҿ���Ʈ �߰�");
            soulheart = 4;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad8)) //�ܼ� Ű�е�8�� ������ �ҿ���Ʈ �ϳ� �� �߰�
        {
            Debug.Log("�ҿ���Ʈ �߰�");
            soulheart = 5;
        }
        if (soulheart == 1) //�ҿ���Ʈ�� 1�̶�� �ҿ���Ʈ ������Ʈ Ȱ��ȭ�ϱ�
        {
            soulHeart1.SetActive(true);
        }
        else if (soulheart == 2) //�ҿ���Ʈ�� 2�̶�� �ҿ���Ʈ ������Ʈ �ϳ� �� Ȱ��ȭ�ϱ�
        {
            soulHeart2.SetActive(true); //�ҿ���Ʈ�� 3�̶�� �ҿ���Ʈ ������Ʈ �ϳ� �� Ȱ��ȭ�ϱ�
        }
        else if (soulheart == 3)
        {
            soulHeart3.SetActive(true); //�ҿ���Ʈ�� 4�̶�� �ҿ���Ʈ ������Ʈ �ϳ� �� Ȱ��ȭ�ϱ�
        }
        else if (soulheart == 4)
        {
            soulHeart4.SetActive(true); //�ҿ���Ʈ�� 5�̶�� �ҿ���Ʈ ������Ʈ �ϳ� �� Ȱ��ȭ�ϱ� 
        }
        else if (soulheart == 5)
        {
            soulHeart5.SetActive(true); //�ҿ���Ʈ�� 6�̶�� �ҿ���Ʈ ������Ʈ �ϳ� �� Ȱ��ȭ�ϱ�
        }
        if (minussoulheart == 1) //���� ���̳ʽ� ��Ʈ�� 1�϶�
        {
            soulHeart5.SetActive(false); //�ҿ���Ʈ5�� ��Ȱ��ȭ
            minussoulheart = 0; //���̳ʽ� ��Ʈ�� �ʱ�ȭ
        }
        else if (minussoulheart == 2)//���� ���̳ʽ� ��Ʈ�� 2�϶�
        {
            soulHeart4.SetActive(false); //�ҿ���Ʈ4�� ��Ȱ��ȭ
            minussoulheart = 0; //���̳ʽ� ��Ʈ�� �ʱ�ȭ
        }
        else if (minussoulheart == 3) //���� ���̳ʽ� ��Ʈ�� 3�϶�
        {
            soulHeart3.SetActive(false); //�ҿ���Ʈ3�� ��Ȱ��ȭ
            minussoulheart = 0; //���̳ʽ� ��Ʈ�� �ʱ�ȭ
        }
        else if (minussoulheart == 4) //���� ���̳ʽ� ��Ʈ�� 4�϶�
        {
            soulHeart2.SetActive(false); //�ҿ���Ʈ2�� ��Ȱ��ȭ
            minussoulheart = 0; //���̳ʽ� ��Ʈ�� �ʱ�ȭ
        }
        else if (minussoulheart == 5) //���� ���̳ʽ� ��Ʈ�� 5�϶�
        {
            soulHeart1.SetActive(false); //�ҿ���Ʈ1�� ��Ȱ��ȭ
            minussoulheart = 0; //���̳ʽ� ��Ʈ�� �ʱ�ȭ
        }
    }
    void Move()
    {
        //�÷��̾� �����̱�
        float h = Input.GetAxisRaw("Horizontal"); //����
        if ((isTouchRight == true && h == -1) || (isTouchLeft == true && h == 1)) //���� h�� 1�̰ų� Right�� true��� or ���� h�� -1�̰ų� Left�� true���
            h = 0;
        float v = Input.GetAxisRaw("Vertical"); //����
        if ((isTouchTop == true && v == 1) || (isTouchBottom == true && v == -1)) //���� v�� 1�̰ų� Top�� true��� or ���� v�� -1�̰ų� Bottom�� true���
            v = 0;
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime; //!!transform�� �ݵ�� Time.deltaTime�� ������ ��!(������ �̵��� ��� x)
        transform.position = curPos + nextPos;


        if (Input.GetButtonDown("Horizontal") ||
            Input.GetButtonUp("Horizontal"))
        {
            anim.SetInteger("Input", (int)h);
        }
    }
    void Fire()
    {
        //�Ѿ� ���
        if (!Input.GetButton("Fire1")) //fire1��ư�� �����ٸ�?
        {
            return;
        }
        if(curShotDelay < maxShotDelay) //���� �������� �ִ뺸�� ���� �ʾҴٸ�?
        {
            return;
        }
        switch (power){
            case 1: //����1
                //�Ŀ��� �ϳ���
                GameObject bullet = Instantiate(bulletobjA, transform.position, transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>(); //���ֱ�
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);//�� �������� �ӵ�*10��ŭ ���ư���
                break;
            case 2: //����2
                GameObject bulletR = Instantiate(bulletobjA, transform.position+Vector3.right*0.1f, transform.rotation);
                GameObject bulletL = Instantiate(bulletobjA, transform.position+Vector3.left*0.1f, transform.rotation);
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>(); //������ ���ֱ�
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>(); //���� ���ֱ�
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);//�� �������� �ӵ�*10��ŭ ���ư���
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);//�� �������� �ӵ�*10��ŭ ���ư���
                break;
            case 3: //����3
                GameObject bulletRR = Instantiate(bulletobjA, transform.position + Vector3.right * 0.25f, transform.rotation);
                GameObject bulletCC = Instantiate(bulletobjB, transform.position, transform.rotation); //ȥ�� �ٸ��� ū �Ѿ� ���
                GameObject bulletLL = Instantiate(bulletobjA, transform.position + Vector3.left * 0.25f, transform.rotation);
                Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>(); //������ ���ֱ�
                Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>(); //��� ���ֱ�
                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>(); //���� ���ֱ�
                rigidRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);//�� �������� �ӵ�*10��ŭ ���ư���
                rigidCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);//�� �������� �ӵ�*10��ŭ ���ư���
                rigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);//�� �������� �ӵ�*10��ŭ ���ư���
                break;
        }

        curShotDelay = 0; //�Ѿ��� ��� 0���� �ʱ�ȭ ���ֱ�
    }
    void Reload()
    {
        curShotDelay += Time.deltaTime;

    }

    void OnTriggerEnter2D(Collider2D collision) //��輱�� ���Դٸ�
    {
        if(collision.gameObject.tag == "Border") //���� Border��� �±׸� ���� ���� ������Ʈ�� �������
        {
            //���� ������Ʈ top bottom left right�� ��Ҵٸ� isTouch���� true�� �ٲ��ش�
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
        else if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet") //���� ������ ��Ұų� �� ź�� ��Ҵٸ�
        {
            manager.RespawnPlayer(); //������ ���ð�
            gameObject.SetActive(false); //�÷��̾ ��Ȱ��ȭ
            if(soulheart == 5)  //���� �ҿ���Ʈ�� 5���
            {
                minussoulheart += 1; //-�ҿ���Ʈ �ϳ� �߰�
                soulheart -= 1; //�ҿ���Ʈ �ϳ� ����
            }
            else if (soulheart == 4)  //���� �ҿ���Ʈ�� 4���
            {
                for (int i = 1; i <= 2; i++) //�ݺ��� 2��
                {
                    minussoulheart += 1; //-�ҿ���Ʈ �ϳ� �߰�
                }
                soulheart -= 1; //�ҿ���Ʈ �ϳ� ����
            }
            else if (soulheart == 3)  //���� �ҿ���Ʈ�� 3�̶��
            {
                for (int i = 1; i <= 3; i++) //�ݺ��� 3��
                {
                    minussoulheart += 1; //-�ҿ���Ʈ �ϳ� �߰�
                }
                soulheart -= 1; //�ҿ���Ʈ �ϳ� ����
            }
            else if (soulheart == 2)  //���� �ҿ���Ʈ�� 2���
            {
                for (int i = 1; i <= 4; i++) //�ݺ��� 4��
                {
                    minussoulheart += 1; //-�ҿ���Ʈ �ϳ� �߰�
                }
                soulheart -= 1; //�ҿ���Ʈ �ϳ� ����
            }
            else if (soulheart == 1)  //���� �ҿ���Ʈ�� 1�̶��
            {
                for (int i = 1; i <= 5; i++) //�ݺ��� 5��
                {
                    minussoulheart += 1; //-�ҿ���Ʈ �ϳ� �߰�
                }
                soulheart -= 1; //�ҿ���Ʈ �ϳ� ����
            }
            else if (soulheart != 5 && soulheart != 4 && soulheart != 3 && soulheart != 2 && soulheart != 1 && Heart.heart == 5)
            { //���� �ҿ���Ʈ�� ���� ������ ü���� 5ĭ�̶��
                Heart.minusheart += 1; //�߰� ü���� ����
            }
            else if (soulheart != 5 && soulheart != 4 && soulheart != 3 && soulheart != 2 && soulheart != 1 && Heart.heart == 4)
            { //���� �ҿ���Ʈ�� ���� ���ٸ� ü���� 4ĭ�̶��
                Heart.minusheart += 1; //�߰� ü���� ����
            }
            else //���δ� �ƴ϶��
            {
                Heart.heart -= 1; //�׳� ü���� ����
            }
        }
        else if (collision.gameObject.tag == "Item")
        {
            item Item = collision.gameObject.GetComponent<item>();
            switch (Item.type)
            {
                case "SoulHeart": //�ҿ���Ʈ �������� �Ծ��ٸ�
                    if(soulheart < maxsoulheart)
                    {
                        soulheart += 1; //�ҿ���Ʈ �ϳ� �߰�
                    }
                    break;
                case "1": //������1�� �Ծ��ٸ�
                    Heart.heart += 1; //ü�� �߰�
                    speed -= 0.3f; //�ӵ� 0.3��ŭ �پ��
                    plusdmg += 1; //������ 1 ����
                    Enemy.deathcnt = 0; //���� ī��Ʈ �ʱ�ȭ
                    GameManager.stage += 10; //���� �������� ���� +10
                    item2.SetActive(false);
                    break;
                case "2":
                    speed -= 2; //�ӵ� -2��ŭ �پ��
                    plusdmg += 15; //���ݷ� 15��ŭ ����
                    Enemy.deathcnt = 0; //���� ī��Ʈ �ʱ�ȭ
                    GameManager.stage += 10; //���� �������� ���� +10
                    item1.SetActive(false);
                    break;
                case "3":
                    maxShotDelay -= 0.1f; //�ѽ� �����̰� 0.1��ŭ �پ��
                    Heart.heart += 1; //�ִ�ü�� ����
                    Enemy.deathcnt = 0; //���� ī��Ʈ �ʱ�ȭ
                    GameManager.stage += 10; //���� �������� ���� +10
                    item4.SetActive(false);
                    break;
                case "4":
                    maxShotDelay -= 0.17f; //�ѽ� �����̰� 0.17��ŭ �پ��
                    plusdmg -= 1; //������ 1��ŭ  ����
                    if(Bullet.attack >= 15) //���� �������� 15�� ���ų� �Ѵ´ٸ�
                    {
                        plusdmg -= 10; //�������� 10����
                    }
                    if(power == 3) //���� ������ 3�Ƹ���
                    {
                        plusdmg -= 3; //�������� 3 ����
                    }
                    if(Heart.heart == 5) //���� ü���� 5 ĭ�̶�� 
                    {
                        Heart.minusheart += 2; //ü���� ��ĭ ��´�
                        heart5.SetActive(false);
                        heart4.SetActive(false);

                    }
                    else if (Heart.heart == 4)
                    { //���� ü���� 4 ĭ�̶�� 
                        Heart.minusheart += 2; //ü���� ��ĭ ��´�
                        Heart.heart -= 1;
                        heart4.SetActive(false);
                        heart3.SetActive(false);

                    }
                    else if (Heart.heart == 3) //���� ü���� 3 ĭ�̶�� 
                    {
                        Heart.heart -= 2; //ü���� ��ĭ ��´�
                        heart3.SetActive(false);
                        heart2.SetActive(false);
                    }
                    else if (Heart.heart == 2) //���� ü���� 2 ĭ�̶�� 
                    {
                        Heart.heart -= 2; //ü���� ��ĭ ��´�
                        heart2.SetActive(false);
                        heart1.SetActive(false);
                    }
                    else
                    {
                        Heart.heart -= 1; //ü���� ��´�
                        heart1.SetActive(false);
                    }
                    Enemy.deathcnt = 0; //���� ī��Ʈ �ʱ�ȭ
                    GameManager.stage += 10; //���� �������� ���� +10
                    item3.SetActive(false);
                    break;

            }
            Destroy(collision.gameObject);
            
        }

    }

    void OnTriggerExit2D(Collider2D collision) //��輱���� ���Դٸ�
    {
        if (collision.gameObject.tag == "Border") //���� Border��� �±׸� ���� ���� ������Ʈ�� �������
        {
            //���� ������Ʈ top bottom left right�� ��Ҵٸ� isTouch���� false�� �ٲ��ش�
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
