using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CarSfxHandler : MonoBehaviour
{
    [Header("Audio sourcces")]
    public AudioMixer audioMixer;

    public AudioSource IsTireScreechingAudioSource;
    public AudioSource enigneAudioSource;
    public AudioSource carHitAudioSource;

    TopDownCarController topDownCarController;

    float desiredEnginePitch = 0.5f;
    float IsTireScreechPitch = 0.5f;

    void Awake()
    {
        topDownCarController = GetComponentInParent<TopDownCarController>();    
    }

    void Start(){
        audioMixer.SetFloat("SFXVolume", 0.5f);
    }

    void Update()
    {
        UpdateEnigneSFX();
        UpdateTiresScreechingSFX();
    }

    void UpdateEnigneSFX()
    {
        //자동차 핸들 엔진 소리팩
        float velocityMagnitude = topDownCarController.GetVelocityMagnitude();
        //차가 빨라지면 엔진 소리가 점점 커지게
        float desiredEngineVolume = velocityMagnitude * 0.05f;
        //소리가 최대 1.0f 를 넘지 않고 최소 0.2f를 가지게
        desiredEngineVolume = Mathf.Clamp(desiredEngineVolume, 0.2f, 1.0f); //Clamp 고정시키다 (현재좌표, 최소좌표, 최대좌표)

        enigneAudioSource.volume = Mathf.Lerp(enigneAudioSource.volume, desiredEngineVolume, Time.deltaTime * 10);

        //엔진 소리에 더 큰 변화를 위해 피치도 설정
        desiredEnginePitch = velocityMagnitude * 0.2f;
        desiredEnginePitch = Mathf.Clamp(desiredEnginePitch, 0.5f, 2f);
        enigneAudioSource.pitch = Mathf.Lerp(enigneAudioSource.pitch, desiredEnginePitch, Time.deltaTime * 1.5f);
    }

    void UpdateTiresScreechingSFX()
    {   //자동차 핸들 스크래치 사운드 팩
        if(topDownCarController.IsTireScreeching(out float lateralVelocity, out bool isBraking))
        {   
            //만약 차가 브레이킹 중이라면 소리도 커지고 피치도 달라진다
            if (isBraking)
            {
                IsTireScreechingAudioSource.volume = Mathf.Lerp(IsTireScreechingAudioSource.volume, 1.0f, Time.deltaTime * 10);
                IsTireScreechPitch = Mathf.Lerp(IsTireScreechPitch, 0.5f, Time.deltaTime * 10);
            }
            else
            {
                //만약 브레이킹을 하지 않고 여전히 드리프팅하고 있다면 해당 소리를 재생
                IsTireScreechingAudioSource.volume = Mathf.Abs(lateralVelocity) * 0.05f;
                IsTireScreechPitch = Mathf.Abs(lateralVelocity) * 0.01f;
            }
        }
        //타이어에서 소리가 나지 않는다면 SFX에서 소리 삭제
        else IsTireScreechingAudioSource.volume = Mathf.Lerp(IsTireScreechingAudioSource.volume, 0 , Time.deltaTime * 10);
    }
    
    void OnCollisionEnter2D(Collision2D collision2D)
    {   
        //충돌의 대상의 속도를 구하기
        float relativeVelocity = collision2D.relativeVelocity.magnitude;

        float volume = relativeVelocity * 0.1f;

        carHitAudioSource.pitch = Random.Range(0.95f, 1.05f);
        carHitAudioSource.volume = volume;

        if (!carHitAudioSource.isPlaying)
            carHitAudioSource.Play();
    }
}
