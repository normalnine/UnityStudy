using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star
{
    public string name;

    // 생성자 함수
    public Star()
    {
        
    }

    public Star(string in_name)
    {

    }

    // 소멸자
    ~Star()
    {

    }

    public virtual void Rotate()
    {

    }
}

public class Earths : Star
{
   public override void Rotate()
    {

    }
}

public class ClassObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int a = 10;
        float b = (float)a;
        Earths earths = new Earths();
        earths.Rotate();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
