using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 물리로 힘을 받아서 이동하고
// 어딘가에 부딪히면 파괴되고 싶다.
// 그 때 만약 부딪힌 것이 적이라면
// 데미지를 2점 주고 싶다.
[RequireComponent(typeof(Rigidbody))]
public class Grenade : MonoBehaviour
{
    public GameObject expFactory;
    Rigidbody rb;
    public float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // 어딘가에 부딪히면 3초 후에 파괴되고 싶다.    
    bool isCollsionCheck;
    Collision other;
    private void OnCollisionEnter(Collision collision)
    {
        if(isCollsionCheck)
        {
            return;
        }

        other = collision;
        isCollsionCheck = true;

        StartCoroutine(IEBoom());

    }

    IEnumerator IEBoom()
    {
        // 삐소리내기
        yield return new WaitForSeconds(1);
        // 삐소리내기
        yield return new WaitForSeconds(1);
        // 삐소리내기
        yield return new WaitForSeconds(1);
        // 펑소리내기
        // 반경 3M 안의 충돌체 중에 적이 있다면
        int layer = 1 << LayerMask.NameToLayer("Enemy");
        Collider[] cols = Physics.OverlapSphere(transform.position, 3, layer);
        for(int i = 0; i<cols.Length; i++)
        {
            // 데미지를 2점 주고 싶다.
            cols[i].GetComponent<Enemy2>().DamageProcess(2);
        }

        //수류탄도 파괴하고 싶다.
        Destroy(this.gameObject);

        GameObject explosion = Instantiate(expFactory);
        explosion.transform.position = transform.position;
    }
}
