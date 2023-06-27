using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 누군가 나와 감지충돌 되면 상대방을 파괴하고 싶다.
public class DestoryZone : MonoBehaviour
{ 
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            // 총알은 ObjectPool로 되어있으니 파괴하지 않고 비활성화한다.
            other.gameObject.SetActive(false);
            // 비활성 목록에 다시 추가한다.
            PlayerFire.deActivBulletObjectPool.Add(other.gameObject);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
