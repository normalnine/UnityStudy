using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager : MonoBehaviour
{
    public GameObject imageHit;
    public static HitManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // 번쩍이는 기능을 호출할 함수를 만들고 싶다.
    public void DoHit()
    {
        StartCoroutine(IEHit());       
    }

    // 코루틴 함수를 만들고 싶다.
    IEnumerator IEHit()
    {
        // 1. imageHit 를 보이게 하고 싶다.
        imageHit.SetActive(true);
        // 2. 0.1초 기다렸다가
        yield return new WaitForSeconds(0.1f);
        // 3. imageHit를 보이지 않게 하고 싶다.
        imageHit.SetActive(false);
    }
}
