using PatrolNChase;
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
        myspawnManager.ImDie(gameObject);
    }

    EnemyHP enemyHP;

    NavMeshAgent agent;
    Animator anim;
    GameObject target;
    public float attackRange = 3;
    public GameObject damageUIFactory;

    // 태어날 때 순찰 상태로 하고 싶다.
    // 순찰할 때 플레이어가 근처에 오면
    // 플레이어를 추적하고 싶다.
    // 추적중에 플렐이어가 너무 멀어지면 다시 순찰 상태로 전이하고 싶다.
    public enum State
    {
        Idle,
        Chase,   // 추적
        Attack,
        React,  // 데미지
        Die,    // 죽음
        Patrol, // 순찰
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
            case State.Chase:
                UpdateChase();
                break;
            case State.Attack:
                UpdateAttack();
                break;
            case State.Patrol:
                UpdatePatrol();
                break;
            default:
                break;
        }
    }
    int targetIndex;
    private void UpdatePatrol()
    {
        // 길정보를 알고싶다.
        // 내가 길의 어떤 위치로 갈 것인지 알고 싶다.
        Vector3 pos = PathManager.instance.points[targetIndex].position;
        // 그곳으로 이동하고 싶다.
        agent.SetDestination(pos);

        // 0.1M까지 근접했다면 도착한 것으로 하고 싶다.
        pos.y = transform.position.y;
        float dist = Vector3.Distance(transform.position, pos);
        // 도착했다면 targetIndex를 1증가(순환)시키고 싶다.
        if (dist <= agent.stoppingDistance)
        {
            targetIndex = (targetIndex + 1) % PathManager.instance.points.Length;
        }

        // 만약 플레이어가 내 인식거리안에 들어왔다면
        float dist2 = Vector3.Distance(transform.position, target.transform.position);
        if (dist2 < attackDistance)
        {
            // 추적상태로 전이하고 싶다.
            state = State.Chase;
        }
    }

    float attackDistance = 5;

    private void UpdateIdle()
    {
        target = GameObject.Find("Player");
        if(target)
        {
            // 순찰 상태로 전이하고 싶다.
            state = State.Patrol;
            anim.SetTrigger("Move");
            agent.isStopped = false;
        }
    }

    private void UpdateChase()
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
        // 만약 추격중에 플레이어와 거리가 포기거리보다 크다면
        else if(distance > farDistance)
        {
            // 순찰 상태로 전이하고 싶다.
            state = State.Patrol;
        }
    }
    public float farDistance = 10;

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
            state = State.Chase;
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
            state = State.Chase;
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
        state = State.Chase;
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
