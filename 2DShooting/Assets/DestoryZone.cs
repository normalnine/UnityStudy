using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 누군가 나와 감지충돌 되면 상대방을 파괴하고 싶다.
public class DestoryZone : MonoBehaviour
{ 
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
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
