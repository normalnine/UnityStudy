using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이벤트 함수를 제작하고 싶다.
// Hit, AttackFinished 일 때
public class Enemy2AnimEvent : MonoBehaviour
{
    Enemy2 enemy2;

    void Awake()
    {
        enemy2 = GetComponentInParent<Enemy2>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnAttack_Hit()
    {
        enemy2.OnAttack_Hit();
    }

    void OnAttack_Finished()
    {
        enemy2.OnAttack_Finished();
    }

    void OnAttackWait_Finished()
    {
        enemy2.OnAttackWait_Finished();
    }
}
