using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 일정 시간마다 적 공장에서 적을 생성해서
// 스폰 목록 중에 랜덤한 위치에 배치하고 싶다.
public class SpawnManager : MonoBehaviour
{
    public float makeTime = 2;
    public GameObject enemyFactory;
    public Transform[] spawnList;
    public float minTime = 1;
    public float maxTime = 2;

    // 생성 최대 갯수를 제한하고 싶다.
    // 만약 생성된 녀석이 파괴되면 생성 수를 1 감소하고 싶다.
    public int makeCount = 0;



    public int maxMakeCount = 5;

    internal void ImDie(GameObject gameObject)
    {
        makeCount--;
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true)
        {
            // 만약 생성 수가 최대 수 미만이라면 생성하고 싶다.
            if(makeCount < maxMakeCount)
            {
                GameObject enemy = Instantiate(enemyFactory);
                enemy.GetComponent<Enemy2>().Init(this);
                makeCount++;
                int index = Random.Range(0, spawnList.Length);
                enemy.transform.position = spawnList[index].position;
                yield return new WaitForSeconds(makeTime);

                // 생성 후 랜덤한 시간에 생성하게 해준다.
                makeTime = Random.Range(minTime, maxTime);
            }
            yield return 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
