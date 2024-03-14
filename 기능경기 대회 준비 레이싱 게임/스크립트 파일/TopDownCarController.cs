using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCarController : MonoBehaviour
{
    [Header("Car Settings")] //헤더
    public float driftFactor = 0.95f; //드리프트 할 때 받는 힘
    public float acclerationFactor = 30.0f; //가속
    public float turnFactor = 3.5f; //회전
    public float maxSpeed = 20; //최고 속도

    [Header("Sprites")]
    public SpriteRenderer carSpriteRenderer;
    public SpriteRenderer carShadowRenderer;

    [Header("Jumping")]
    public AnimationCurve jumpCurve;

    float acclerationInput = 0; //눌렀을 때 가속 변하는걸 저장하는 변수
    float steeringInput = 0; //눌렀을때

    float rotationAngle = 0; //회전 각도

    float velocityVsUp = 0; //정면 속도

    bool isJumping = false;


    //컴포넌트 불러오기
    Rigidbody2D carRigidbody2D;

    void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>(); //컴포넌트 설정
    }

    void FixedUpdate()
    {

        ApplyEngineForce();

        KillOrthogonalVelocity();

        ApplySteering();

    }

    void ApplyEngineForce()
    {

        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity); //Dot로 각도를 측정해 얼마나 앞으로 가고 있는지 계산

        if (velocityVsUp > maxSpeed && acclerationInput > 0) //만약 정면으로 스피드보다 크고 가속이 0보다 크다면 제한
            return; 

        if (velocityVsUp < -maxSpeed * 0.5f && acclerationInput < 0) // 역 방향으로 최대속도의 50% 보다 빨리 갈 수 없도록 제한
            return;

        if (carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && acclerationInput > 0 ) //sqrMagnitde로 거리를 계산해주고 정면으로 이것이 스피드와 스피드를 곱한 것보다 크다면 제한 
            return;

        if (acclerationInput == 0) //가속이 있을 경우 드래그를 적용시키고 플레이어가 멈췄다면 차가 멈추게 만든다
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        else carRigidbody2D.drag = 0;
        
        Vector2 engineForceVector = transform.up * acclerationInput * acclerationFactor; //엔진힘은 위치를 위로 올리는 up에 눌렀을때의 가속 그리고 가속힘을 곱한다

        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force); //힘주는 기능(엔진힘, 무게 적용)

    }

    void ApplySteering()
    {

        float minSpeedBeforeAllowTurningFactor = (carRigidbody2D.velocity.magnitude / 8); //속도 규모에 나누기 8한 값을 저장한다 
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor; // 회전각도에 운전한 만큼 회전힘 곱한 정도를 뺀다

        carRigidbody2D.MoveRotation(rotationAngle); // 물리법칙을 적용받지 않고 강제로 회전각도만큼 이동
    }

    void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up); //자동차의 정면속도를 계산
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right); //자동차의 오른쪽으로 갔을때의 속도를 계산

        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    float GetLateralVelocity()
    {
        //벡터의 각도를 리턴한다
        return Vector2.Dot(transform.right, carRigidbody2D.velocity);
    }

    public bool IsTireScreeching(out float lateralVelocity, out bool isBraking)
    {
        //측면속도에 벡터의 각도를 저장
        lateralVelocity = GetLateralVelocity();
        //브레이킹 멈춤
        isBraking = false;
        
        //가속이 0보다 작고 앞으로가는 속도가 0보다 크다면
        if (acclerationInput < 0 && velocityVsUp > 0)
        {
            //브레이킹을 활성화하고
            isBraking = true;
            //결과를 리턴한다
            return true;
        }
        //만약 벡터의 각도가 4보다 크다면
        if (Mathf.Abs(GetLateralVelocity()) > 4.0f)
        //결과를 리턴한다
            return true;
        //모두 해당하지 않으면 결과를 리턴하지 않는다
        return false;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        //좌우입력의 입력벡터의 x 저장
        steeringInput = inputVector.x;
        //좌우입력의 입력벡터의 y 저장
        acclerationInput = inputVector.y;
    }

    public float GetVelocityMagnitude()
    {
      return carRigidbody2D.velocity.magnitude;
    } 

    public void Jump(float jumpHeightScale, float jumpPushScale)
    {
        //만약 점프한 상태가 아니라면 JumpCo 코르틴 활성화
        if(!isJumping)
            StartCoroutine(JumpCo(jumpHeightScale, jumpPushScale));
    }

    private IEnumerator JumpCo(float jumpHeightScale, float jumpPushScale)
    {
        //점프 활성화
        isJumping = true;
        //점프 시작 시간에 시간을 저장
        float jumpStartTime = Time.time;
        //점프 지속시간
        float jumpDuration = 2;
        //점프가 활성화 상태라면
        while (isJumping)
        {
            //점프 과정에 있는 곳의 백분율 0 - 1.0로 계산
            float jumpCompletedPercentage = (Time.time - jumpStartTime) / jumpDuration;
            jumpCompletedPercentage = Mathf.Clamp01(jumpCompletedPercentage);

            carSpriteRenderer.transform.localScale = Vector3.one + Vector3.one * jumpCurve.Evaluate(jumpCompletedPercentage) * jumpHeightScale;

            carShadowRenderer.transform.localScale = carSpriteRenderer.transform.localScale * 0.75f;

            carShadowRenderer.transform.localPosition = new Vector3(1, -1, 0.0f) * 3 * jumpCurve.Evaluate(jumpCompletedPercentage) * jumpHeightScale;

            if(jumpCompletedPercentage == 1.0f)
                break;

            //함수 호출까지 실행 유예
            yield return null;
        }

        carSpriteRenderer.transform.localScale = new Vector3(0.5f, 1, 1);

        
        carShadowRenderer.transform.localPosition = Vector3.zero;
        carShadowRenderer.transform.localScale = carSpriteRenderer.transform.localScale;

        isJumping = false;
    }
}