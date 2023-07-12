using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

// 상태머신으로 제어하고 싶다.
// Agent를 이용해서 이동하고 싶다.
// 나를 생성한 SpawnManager 기억하고, 내가 죽을 때 걔한테 알려주고 싶다.
public class Enemy2 : MonoBehaviour
{
    SpawnManager myspawnManager;
    public void Init(SpawnManager spawnMgr)
    {
        myspawnManager = spawnMgr;
    }

    private void OnDestroy()
    {
        myspawnManager.ImDie(this);
    }

    EnemyHP enemyHP;

    NavMeshAgent agent;
    Animator anim;
    GameObject target;
    public float attackRange = 3;
    public GameObject damageUIFactory;

    public enum State
    {
        Idle,
        Move,
        Attack,
        React,  // 데미지
        Die,    // 죽음
    }

    public State state;

    // Start is called before the first frame update
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        enemyHP = GetComponent<EnemyHP>();
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

    #region 애니메이션 이벤트 함수를 통해 호출되는 함수들...
    public void OnAttack_Hit()
    {
        anim.SetBool("isAttack", false);
        // 타격할 수 있는 조건
        // 목적지와의 거리가 공격가능 거리 이하일 때 가능
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if(distance <= attackRange)
        {
            print("Enemy -> Player Hit!!!");
            HitManager.instance.DoHit();
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

    internal void OnReact_Finished()
    {
        // 리액션이 끝났으니 Move상태로 전이하고 싶다.
        state = State.Move;
        anim.SetTrigger("Move");
        agent.isStopped = false;

    }
    #endregion

    // 데미지를 입으면 데미지 UI를 내위치 위쪽으로 1M 위로
    internal void DamageProcess(int damage = 1)
    {
        // 만약 내상태가 죽음상태라면
        if(state == State.Die)
        {
            return;
        }

        GameObject ui = Instantiate(damageUIFactory);
        ui.transform.position = transform.position + Vector3.up * 1.1f;
        

        // 적 체력을 damage 만큼 감소하고 싶다.
        enemyHP.HP-=damage;

        agent.isStopped = true;

        // 만약 적 체력이 0이하라면
        if (enemyHP.HP <= 0)
        {
            state = State.Die;
            // 파괴하고 싶다.
            Destroy(this.gameObject, 5);
            anim.SetTrigger("Die");

            // 나의 콜라이더 컴포넌트를 끄고싶다.
            //GetComponent<CapsuleCollider>().enabled = false;
        }
        // 체력이 남아있으면 리액션하고 싶다.
        else
        {
            state = State.React;
            anim.SetTrigger("React");
        }
    }

    
}
