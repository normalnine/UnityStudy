using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 시간이 흐르다가 생성 시간이 되면 적 공장에서 적을 만들어서 내 위치에 배치하고 싶다.
public class EnemyManager : MonoBehaviour
{
    // 현재시간
    float currentTime = 0;
    // 생성시간
    public float createTime = 2;
    // 적공장
    public GameObject enemyFactory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 1. 시간이 흐르다가
        currentTime += Time.deltaTime;
        // 2. 만약 현재시긴이 생성시간이 되면
        if (currentTime > createTime)
        {
            // 3. 적 공장에서 적을 만들어서
            GameObject enemy = Instantiate(enemyFactory);
            // 4. 내 위치에 배치하고 싶다.
            enemy.transform.position = transform.position;
            enemy.transform.rotation = transform.rotation;
            // 5. 현재 시간을 초기화
            currentTime = 0;
        }

    }
}
