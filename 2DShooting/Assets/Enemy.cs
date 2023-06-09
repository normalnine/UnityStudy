using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

// 아래로 이동하고싶다.
// 30%의 확류롤 플레이어 방향, 나머지 확률로 아래로 정하고 싶다.
// 살아가면서 그 방향으로 이동하고 싶다.

public class Enemy : MonoBehaviour
{
    public float speed = 5;
    Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        // 30%의 확류롤 플레이어 방향, 나머지 확률로 아래로 정하고 싶다.
        int rvalue = Random.Range(0, 10);

        if(rvalue < 3)
        {
            // 플레이어 방향
            GameObject target = GameObject.Find("Player");
            if(target)
            {
                dir = target.transform.position - this.transform.position;
                dir.Normalize();
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
        // 너죽고
        Destroy(collision.gameObject);
        // 나죽자
        Destroy(this.gameObject);
        //print(collision.gameObject.name);
    }
}
