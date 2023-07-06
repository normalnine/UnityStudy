using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineExample : MonoBehaviour
{
    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IEMoveManager());
    }

    IEnumerator IEMoveManager()
    {
        yield return StartCoroutine(IEMove(Vector3.right));
        yield return StartCoroutine(IEMove(Vector3.up));
        yield return StartCoroutine(IEMove(Vector3.left));
        yield return StartCoroutine(IEMove(Vector3.down));
        yield return 0;
    }

    IEnumerator IEMove(Vector3 direction)
    {
        for(float t=0; t<=1; t+=Time.deltaTime)
        {
            transform.position += direction * speed * Time.deltaTime;
            yield return 0;
        }
    }
}
