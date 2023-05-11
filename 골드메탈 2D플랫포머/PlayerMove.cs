using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxspeed;
    Rigidbody2D Rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    public void Awake()
    {
        //초기화
        Rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); 
    }

    void Update()
    {
        if (Input.GetButtonUp("Horizontal")) //만약 키를 입력했을때
        {
            Rigid.velocity = new Vector2(Rigid.velocity.normalized.x*0.5f, Rigid.velocity.y); //만약 키에서 손을 땠을때 속도를 줄임
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
        else if (Rigid.velocity.x < maxspeed*(-1)) //만약 왼쪽 속도가 최고속도를 넘으면 제한해주는 것 (왼쪽은 음수라서 maxspped에  -1을 곱한다)
            Rigid.velocity = new Vector2(maxspeed * (-1), Rigid.velocity.y);
    }
}
