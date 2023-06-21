using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 태어나면 목적지로 이동하고 싶다.
// 이동이 끝나면 공격하고 싶다.
// 공격이 끝나면 3초 대기하고 싶다.
// 공격과 대기를 반복하고 싶다.
public class Boss : MonoBehaviour
{
    const int MOVE = 0;
    const int ATTACK = 1;
    const int WAIT = 2;

    enum State
    {
       
    }

    int state;
    public float speed = 10;

    public Transform moveTarget;

    // Start is called before the first frame update
    void Start()
    {
        // 명시적
        state = MOVE;
    }

    // Update is called once per frame
    void Update()
    {
        // 나는 한번에 한가지 상태만 처리할 수 있다.
        switch (state)
        {
            case MOVE:
                UpdateMove();
                break;
            case ATTACK:
                UpdateAttack();
                break;
            case WAIT:
                UpdateWait();
                break;
        }
    }

    void UpdateMove()
    {
        Vector3 dir = moveTarget.transform.position - transform.position;
        
        transform.position += dir.normalized * speed * Time.deltaTime;

        if(dir.magnitude < 0.1f)
        {
            transform.position = moveTarget.position;
            state = ATTACK;
        }
        
    }

    public GameObject bossBulletFactory;
    float angleZ;
    public float oneStepAngle = 30;
    public float fireTime = 0.1f;
    public float maxAngle = 720;
    void UpdateAttack()
    {
        currTime += Time.deltaTime;
        if(currTime > fireTime)
        {
            currTime = 0;
            GameObject bullet = Instantiate(bossBulletFactory);
            bullet.transform.position = transform.position;
            bullet.transform.eulerAngles = new Vector3(0, 0, angleZ);
            angleZ += oneStepAngle;
        }        

        if(angleZ >= maxAngle)
        {            
            state = WAIT;
            angleZ = 0;
            currTime = 0;
        }
    }

    float currTime = 0;
    public float waitTime = 1;
    void UpdateWait()
    {
        // 시간이 흐르다가
        currTime += Time.deltaTime;
        // 만약 대기시간이 되면
        if(currTime>waitTime)
        {
            // 공격을 하고 싶다.
            state = ATTACK;
            currTime = 0;
            
        }
    }


}
