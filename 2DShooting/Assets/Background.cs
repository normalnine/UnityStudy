using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 위로 스크롤 이동 하고싶다.
public class Background : MonoBehaviour
{
    public float speed = 1000;
    Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        // 태어날 때 렌더러 컴포넌트를 가져와서 기억하고 싶다.
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        renderer.material.mainTextureOffset += Vector2.up * speed * Time.deltaTime;    
    }
}
