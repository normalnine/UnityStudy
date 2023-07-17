using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BesierCurveTest : MonoBehaviour
{
    LineRenderer lr;
    public Transform a, b, c;
    public int pointCount = 100;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = pointCount;
        lr.SetPosition(0, a.position);
        lr.SetPosition(1, b.position);
        lr.SetPosition(2, c.position);
    }

    // Update is called once per frame
    void Update()
    {
        lr.positionCount = pointCount;
        for (int i = 0; i < pointCount; i++)
        {
            float t = (float)i / (pointCount - 1);
            lr.SetPosition(i, GetCurvePoint(a.position, b.position, c.position, t));
        }
    }

    Vector3 GetCurvePoint(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);

        return Vector3.Lerp(ab, bc, t);
    }

    Vector3 GetCurvePoint4(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);
        Vector3 cd = Vector3.Lerp(c, d, t);

        Vector3 abbc = Vector3.Lerp(ab, bc, t);
        Vector3 bccd = Vector3.Lerp(bc, cd, t);


        return Vector3.Lerp(abbc, bccd, t);
    }
}
