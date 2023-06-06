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
        if (Input.GetKeyDown(KeyCode.Keypad1)) //�ܼ� Ű�е�1�� ������ ����1�� ����
        {
            power = 1;

        }
        if (Input.GetKeyDown(KeyCode.Keypad2)) //�ܼ� Ű�е�2�� ������ ����2�� ����
        {
            power = 2;

        }
        if (Input.GetKeyDown(KeyCode.Keypad3)) //�ܼ� Ű�е�3�� ������ ����3���� ����
        {
            power = 3;

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
