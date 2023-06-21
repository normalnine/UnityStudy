using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

// 아래로 이동하고싶다.
// 30%의 확류롤 플레이어 방향, 나머지 확률로 아래로 정하고 싶다.
// 살아가면서 그 방향으로 이동하고 싶다.
// 내(Enemy)가 파괴될 때 폭발공장에서 폭발을 만들어서 내 위치에 배치하고 싶다.
// 폭발은 2초 후에 파괴되게 하고 싶다.

public class Enemy : MonoBehaviour
{
    public float speed = 5;
    Vector3 dir;
    EnemyHP enemyHP;
    public GameObject explosionFactory;

    // Start is called before the first frame update
    void Start()
    {
        enemyHP = GetComponent<EnemyHP>();

        // 30%의 확류롤 플레이어 방향, 나머지 확률로 아래로 정하고 싶다.
        int rvalue = Random.Range(0, 10);

        if (rvalue < 3)
        {
            // 플레이어 방향
            GameObject target = GameObject.Find("Player");
            if (target)
            {
                dir = target.transform.position - this.transform.position;
                dir.Normalize();
                transform.up = -dir;
            }
            else
            {
                dir = Vector3.down;
            }
        }
        else
        {
            dir = Vector3.down;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //print(collision.gameObject.name);
        // 만약 부딪힌 상대방의 이름에 Player라는 문자열이 포함되어있다면
        if (collision.gameObject.name.Contains("Player"))
        {
            // collision에게 PlayerHP컴포넌트를 가져오고 싶다.
            // 체력을 1감소하고 싶다.
            PlayerHP php = collision.gameObject.GetComponent<PlayerHP>();
            php.HP--;

            // 체력이 0이라면
            if (php.HP <= 0)
            {
                // 너죽고
                Destroy(collision.gameObject);

                // 게임오버UI를 활성화 하고 싶다.
                GameManager.instance.gameOverUI.SetActive(true);
            }
        }
        else
        {
            // 나 : Enemy, 너(collision) : Bullet
            // 너죽고
            // 비활성 목록에 다시 추가한다.
            PlayerFire.deActivBulletObjectPool.Add(collision.gameObject);
        }
        enemyHP.HP--;

        if (enemyHP.HP <= 0)
        {
            // 적을 파괴될 때 경험치를 1 획득하고 싶다. UI에 표현하고 싶다.
            PlayerLevel.instance.EXP++;

            // 점수를 1점 증가시키고 싶다.
            ScoreManager.instance.SCORE++;
            GameObject explosion = Instantiate(explosionFactory);
            explosion.transform.position = transform.position;
            // 나죽자
            Destroy(this.gameObject);
        }
    }
}
