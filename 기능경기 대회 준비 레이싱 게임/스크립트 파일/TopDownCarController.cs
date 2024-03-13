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

    float acclerationInput = 0; //눌렀을 때 가속 변하는걸 저장하는 변수
    float steeringInput = 0; //눌렀을때

    float rotationAngle = 0; //회전 각도

    float velocityVsUp = 0; //정면 속도


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
        return Vector2.Dot(transform.right, carRigidbody2D.velocity);
    }

    public bool IsTireScreeching(out float lateralVelocity, out bool isBraking)
    {
        lateralVelocity = GetLateralVelocity();
        isBraking = false;

        if (acclerationInput < 0 && velocityVsUp > 0)
        {
            isBraking = true;
            return true;
        }

        if (Mathf.Abs(GetLateralVelocity()) > 4.0f)
            return true;

        return false;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        acclerationInput = inputVector.y;
    }
    public float GetVelocityMagnitude()
    {
      return carRigidbody2D.velocity.magnitude;
    } 
}