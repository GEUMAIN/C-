using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer SpriteRenderer;
    public int nextMove;
    public int hp;
    public GameObject Enemy;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        hp = 5;
        Invoke("Think", 5); //딜레이라 생각하면 편하다
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y); //적 이동

        //지형체크
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.2f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            Trun();

        }
        if (hp == 0)
        {
            Enemy.SetActive(false);
        }
    }
    //재귀함수 함수가 자기 함수를 또 호출하는 것
    void Think() //왼쪽인지 오른쪽인지 멈춰 있는지 생각
    {
        //랜덤으로 다음행동 정하기
        nextMove = Random.Range(-1, 2);

        //스프라이트 애니메이션 
        anim.SetInteger("WalkSpeed", nextMove);
        //방향 바꾸기
        if (nextMove != 0)
        {
            SpriteRenderer.flipX = nextMove == 1;
        }
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }
    void Trun()
    {
        SpriteRenderer.flipX = nextMove == 1;
        nextMove *= -1; //낭떠러지인것을 인지하고 방향을 바꿈
        CancelInvoke(); //생각을 멈추게 하는 것
        Invoke("Think", 2);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Attack") //공격이라는 태그를 가진 오브젝트에 닿았을때
        {
            hp -= 1;
        }
    }
}
