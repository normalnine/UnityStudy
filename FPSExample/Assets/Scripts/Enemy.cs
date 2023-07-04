using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 태어날 때 agent에게 목적지(플레이어)를 알려주고 싶다.
// 살아가면서 목적지 방향으로 agent를 이용해서 이동하고 싶다.
// 대기, 이동, 공격(공격대기)
public class Enemy : MonoBehaviour
{
    public enum State
    {
        Idle,
        Move,
        Attack
    }

    public State state;
    public Animator anim;
   

    public float speed = 5;
    GameObject target;
    NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle: 
                UpdateIdle();
                break;
            case State.Move:
                UpdateMove();
                break;
            case State.Attack:
                UpdateAttack();
                break;
            default:
                break;
        }       
    }

    private void UpdateIdle()
    {
        // 태어날 때 agent에게 목적지(플레이어)를 알려주고 싶다.
        target = GameObject.Find("Player");
        print("idle->move");
        // 만약 목적지를 찾았다면
        if (target)
        {
            
            // 이동상태로 전이하고 싶다.
            state = State.Move;
            anim.SetTrigger("Move");
        }        
    }

    private void UpdateMove()
    {
        // agent야 너의 목적지는 target의 위치야
        agent.destination = target.transform.position;
        // 목적지와 나의 거리를 재고 싶다.
        float distance = Vector3.Distance(target.transform.position, transform.position);
        // 만약 그거리가 공격 가능 거리보다 작다면
        if (distance < attackRange)
        {
            // 공격 상태로 전이하고 싶다.
            state = State.Attack;
            anim.SetTrigger("Attack");
        }
    }

    // SubState를 만들고 싶다.(isAttackWait) 공격/공격대기
    // isAttackWait == true : 공격 대기
    // isAttackWait == false : 공격 중   

    enum AttackSubState
    {
        Attack,
        Wait
    }

    AttackSubState attackSubState;
    bool isAttackWait;
    bool isAttackHit;
    public float attackRange = 3;
    public float currTime = 0;
    public float attackHitTime = 0.91f;
    public float attackFinishedTime = 2.2f;
    public float attackWaitTime = 2;

    private void UpdateAttack()
    {
        // 시간이 흐르다가
        currTime += Time.deltaTime;

        if(attackSubState == AttackSubState.Wait)
        {
            // 대기 시간을 초과하면
            if(currTime > attackWaitTime)
            {
                // 공격 상태로 전이하고 싶다.
                attackSubState = AttackSubState.Attack;
                currTime = 0;
                isAttackHit = false;
                anim.SetBool("isAttack", true);
            }
        }
        else // 공격 중
        {
            // 타격시간을 초과하면
            if(currTime > attackHitTime)
            {
                // 타격을 하고 싶다.
                if (isAttackHit == false)
                {
                    // 타격!
                    isAttackHit = true;
                    anim.SetBool("isAttack", false);
                    print("Enemy -> Player Hit!!!");
                }
                // 공격 끝 시간이 초과하면
                if(currTime > attackFinishedTime)
                {
                    // 공격 대기 상태로 전이하고 싶다.
                    attackSubState = AttackSubState.Wait;
                    currTime = 0;
                }
            }
        }
    }
}
