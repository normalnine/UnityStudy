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

    //���ϱ� �Լ�
    //��ȯ�ڷ��� �Լ��̸�([�Ű�����...])
    int Add(int a, int b)
    {
        return a + b;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
