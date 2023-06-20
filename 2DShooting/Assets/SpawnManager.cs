using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Transform 으로 된 Spawn 목록을 가지고 있고 싶다.
// 일정 시간마다 Spawn 목록 중에 랜덤으로 하나 정하고 싶다.
// 적 공장에서 적을 만들어서 그 위치에 배치하고 싶다.
public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnList;
    float currTime = 0;
    public float makeTime = 1;
    public GameObject enemyFactory;
    int prevChooseIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        enemyFactory = Resources.Load<GameObject>("Enemy");
    }

    // Update is called once per frame
    void Update()
    {   // 적 공장에서 적을 만들어서 그 위치에 배치하고 싶다.

        // 1. 시간이 흐르다가
        currTime += Time.deltaTime;

        // 2. 현재시간이 생성 시간이 되면
        if(currTime > makeTime)
        {
            // 3. 적 공장에서 적을 만들어서
            GameObject enemy = Instantiate(enemyFactory);
            // 4. 일정 시간마다 Spawn 목록 중에 랜덤으로 하나 정하고 싶다.            
            int chooseIndex = Random.Range(0, spawnList.Length);
            // 4.1 랜덤 인덱스가 직전 인덱스와 같다면 다시 정하고 싶다.
            if (prevChooseIndex == chooseIndex)
            {
                chooseIndex = (chooseIndex + spawnList.Length - 1) % spawnList.Length;
                //     chooseIndex = (chooseIndex + 1) % spawnList.Length;
                //// chooseIndex 1을 더하고 싶다.
                //chooseIndex++;

                //// 만약 chooseIndex가 배열의 범위를 벗어난다면 0으로 초기화하고 싶다.
                //if (chooseIndex >= spawnList.Length)
                //{
                //    chooseIndex = 0;
                //}
            }
            // 직전 인덱스에 현재 인덱스를 기억하고 싶다.
            prevChooseIndex = chooseIndex;
            enemy.transform.position = spawnList[chooseIndex].transform.position;
            // 5. 현재 시간을 0으로 초기화 하고싶다.
            currTime = 0;
        }
        
    }
}
