﻿using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject attack;
    public float player_JumpTime;
    public float jumpPower;
    public float maxspeed;
    Rigidbody2D Rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    public void Awake()
    {
        //초기화
        Rigid = GetComponent<Rigidbody2D>();
        Rigid.freezeRotation = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        
    }

    private void Update()
    {
        if (Input.GetButtonUp("Horizontal")) //만약 키를 입력했을때
        {
            Rigid.velocity = new Vector2(Rigid.velocity.normalized.x * 0.5f, Rigid.velocity.y); //만약 키에서 손을 땠을때 속도를 줄임
        }
        //점프기능
        if (Input.GetButtonDown("Jump") && !animator.GetBool("isJumping"))
        {
            player_JumpTime = 0.0f;
        }
        if (Input.GetButtonUp("Jump") && !animator.GetBool("isJumping"))
        {
            if (player_JumpTime >= 1.0f)
            {
                //Rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                Rigid.velocity = new Vector2(Rigid.velocity.x, jumpPower * 2.5f);
                animator.SetBool("isJumping", true);
            }
            else if (player_JumpTime >= 0.5f)
            {
                Rigid.velocity = new Vector2(Rigid.velocity.x, jumpPower * 2.0f);
                animator.SetBool("isJumping", true);
            }
            else
            {
                Rigid.velocity = new Vector2(Rigid.velocity.x, jumpPower);
                animator.SetBool("isJumping", true);
            }
            player_JumpTime += Time.deltaTime;
        }

        //방향전환
        spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1; //만약 Input.GetAxisRaw("Horizontal")이 -1이라면 flip을 x로 바꿈(왼쪽으로 방향전환)
        //애니메이션 다른 모션으로 변경
        if (Mathf.Abs(Rigid.velocity.x) < 0.3) //만약 속도가 절댓값 0.3보다 작다면 isWalking을 false(가만히 서기)로 설정
            animator.SetBool("isWalking", false);
        else //만약 속도가 절댓값 0.3보다 크다면 isWalking을 true(뛰는 모션)로 설정
            animator.SetBool("isWalking", true);
    }

    void FixedUpdate()
    {
        //키 컨트롤로 움직이기

        float h = Input.GetAxisRaw("Horizontal");

        Rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse); //힘을 1초에 50번을 줌

        if (Rigid.velocity.x > maxspeed) //만약 오른쪽 속도가 최고속도를 넘으면 제한해주는 것
            Rigid.velocity = new Vector2(maxspeed, Rigid.velocity.y);
        else if (Rigid.velocity.x < maxspeed * (-1)) //만약 왼쪽 속도가 최고속도를 넘으면 제한해주는 것 (왼쪽은 음수라서 maxspped에  -1을 곱한다)
            Rigid.velocity = new Vector2(maxspeed * (-1), Rigid.velocity.y);
        if (Rigid.velocity.y < 0)
        {
            Debug.DrawRay(Rigid.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(Rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform")); //시작 방향 빔길이 레이어
            //RaycastHit 레이어에 해당하는 것에 맞았는가?

            //플레이어가 밀착했는지 착지했는지 알아보기
            if (rayHit.collider != null)
            {

                if (rayHit.distance < 0.5f) //착지했는가?
                {
                    animator.SetBool("isJumping", false); //애니메이션 비활성화 해주기
                    //플레이어의 절반크기만큼이여야 바닥에 닿은거있음 알 수 있다
                }

            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy") //적이라는 태그를 가진 적에게 닿았을때
        {
            OnDamaged(collision.transform.position); //호출
        }
    }
    void OnDamaged(Vector2 targetPos)
    {
        gameObject.layer = 11; //레이어11로 바꿈

        spriteRenderer.color = new Color(1, 1, 1, 0.4f); //색 변경 마지막 네번째는 투명도

        //튕겨져 나가기
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1; //왼쪽으로 맞으면 왼쪽으로 오른쪽으로 맞으면 오른쪽
        Rigid.AddForce(new Vector2(dirc, 1)*7, ForceMode2D.Impulse); //튕겨져 나가는 세기
        Invoke("OffDamaged", 2);
    }
    void OffDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
    void append()
    {
        attack.SetActive(true);
    }
    void destory()
    {
        attack.SetActive(false);
    }
    public void OnMouseDown()
    {
        Invoke("append", 0.1f);
        Invoke("destory", 0.2f);
    }
}
