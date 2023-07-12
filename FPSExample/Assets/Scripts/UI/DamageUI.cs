using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 태어날 때 수명을 1초로 정하고 싶다.
// 1초동안 위로 이동하고 싶다.
public class DamageUI : MonoBehaviour
{
    public float lifeSpan = 0.8f;
    public float speed = 5;
    public AnimationCurve ac;
    float currTime = 0;
    public float height = 1;
    // 태어날 때 내 위치를 기억하고 싶다.
    Vector3 origin;

    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
        Destroy(gameObject, lifeSpan);        
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime / speed;
        float value = ac.Evaluate(currTime);
        transform.position = origin + Vector3.up * value * speed * height;
    }
}
