using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelParticleHandler : MonoBehaviour
{
    float particleEmissionRate = 0;

    TopDownCarController topDownCarController;

    ParticleSystem particleSystemSmoke;
    ParticleSystem.EmissionModule particleSystemEmissionModule;

    void Awake()
    {
        topDownCarController = GetComponentInParent<TopDownCarController>();

        //파티클 시스템 부여
        particleSystemSmoke = GetComponent<ParticleSystem>();

        //파티클 배출에 컴포던트를 부여
        particleSystemEmissionModule = particleSystemSmoke.emission;

        //파티클 배출량을 0으로 설정한다
        particleSystemEmissionModule.rateOverTime = 0;
    }


    void Update()
    {
        // 파티클이 끝나는 시간을 줄임
        particleEmissionRate = Mathf.Lerp(particleEmissionRate, 0, Time.deltaTime * 5);
        particleSystemEmissionModule.rateOverTime = particleEmissionRate;

        if (topDownCarController.IsTireScreeching(out float lateralVelocity, out bool isBraking))
        {
            //만약 브레이킹 중이라면 파티클 양을 30으로
            if (isBraking)
                particleEmissionRate = 30;
            //아니라면 파티클 양에 절댓값 * 2로 설정
            else particleEmissionRate = Mathf.Abs(lateralVelocity) * 2;
        }
    }
}
