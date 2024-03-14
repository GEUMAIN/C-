using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTrailRendererHandler : MonoBehaviour
{
    TopDownCarController topDownCarController;
    TrailRenderer trailRenderer;

    void Awake()
    {
        topDownCarController = GetComponentInParent<TopDownCarController>();

        trailRenderer = GetComponent<TrailRenderer>();

        //효과 끄기
        trailRenderer.emitting = false;
    }

    void Update()
    {
        //스크래칭이 만약 활성화 되었다면
        if (topDownCarController.IsTireScreeching(out float lateralVelocity, out bool isBraking))
        //효과를 키고
            trailRenderer.emitting = true;
        //효과를 끄기
        else trailRenderer.emitting = false;
    }
}
