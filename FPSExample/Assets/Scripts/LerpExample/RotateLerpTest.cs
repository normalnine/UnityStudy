using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 목적지를 바라보고 싶다.
public class RotateLerpTest : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(target, Vector3.up);
        // 부드럽게 회전하고 싶다.
        // 1. 바라볼 방향
        Vector3 lookDir = target.position - transform.position;
        lookDir.Normalize();
        // 2. 그 방향의 회전값
        Quaternion targetRotate = Quaternion.LookRotation(lookDir);
        // 3. 그 회전값으로 Lerp하고 싶다.
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotate, Time.deltaTime * 5);
    }
}
