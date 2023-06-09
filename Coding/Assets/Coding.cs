using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coding : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int result = Add(10, 20);
        print(result);
    }

    //더하기 함수
    //반환자료형 함수이름([매개변수...])
    int Add(int a, int b)
    {
        return a + b;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
