using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    public Transform objectToFollow;
    private void LateUpdate()
    {
        // 플레이어가 오브젝트를 따라가도록 위치를 업데이트합니다.
        Vector3 newPosition = transform.position;
        newPosition.x = objectToFollow.position.x;
        transform.position = newPosition;
    }
}

