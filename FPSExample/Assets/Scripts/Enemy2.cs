using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 상태머신으로 제어하고 싶다.
// Agent를 이용해서 이동하고 싶다.
public class Enemy2 : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    GameObject target;
    public float attackRange = 3;

    public enum State
    {
        Idle,
        Move,
        Attack
    }

    public State state;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // state를 기준으로 분기처리
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
        target = GameObject.Find("Player");
        if(target)
        {
            state = State.Move;
            anim.SetTrigger("Move");
            agent.isStopped = false;
        }
    }


    private void UpdateMove()
    {
        agent.destination = target.transform.position;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        
        if(distance < attackRange)
        {
            state = State.Attack;
            anim.SetTrigger("Attack");
            // agent 멈추기
            agent.isStopped = true;
        }
    }   

    private void UpdateAttack()
    {
        // 공격 중에는 목적지를 바라보게 하고 싶다.
        transform.LookAt(target.transform);
    }

    public void OnAttack_Hit()
    {
        anim.SetBool("isAttack", false);
        // 타격할 수 있는 조건
        // 목적지와의 거리가 공격가능 거리 이하일 때 가능
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if(distance <= attackRange)
        {
            print("Enemy -> Player Hit!!!");
        }
    }

    public void OnAttack_Finished()
    {
        // 대기 상태로 가야하는 가?
        // 목적지와의 거리가 공격 가능 거리 이상이라면
        // 이동 상태로 전이하고 싶다.
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance > attackRange)
        {
            // 이동 상태로 전이하고 싶다.
            state = State.Move;
            anim.SetTrigger("Move");
            agent.isStopped = false;
            
        }
    }

    public void OnAttackWait_Finished()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance > attackRange) // 공격범위 벗어남
        {
            // 이동 상태로 전이하고 싶다.
            state = State.Move;
            anim.SetTrigger("Move");
            agent.isStopped = false;
        }
        else // 공격 가능한 거리
        {
            anim.SetBool("isAttack", true);
        }
    }
}
