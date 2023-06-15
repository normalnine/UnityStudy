using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//  포션 아이템과 부딪혔다면 포션 갯수를 1 증가시키고 싶다.
// 만약 1번키를 눌렀다면 포션이 1개 이상 있다면 체력을 최대 체력으로 하고 싶다.
public class PlayerInventory : MonoBehaviour
{
    int potion;
    public TextMeshProUGUI textPotion;
    PlayerHP playerHP;

    public int POTION
    {
        get { return potion; }
        set
        {
            potion = value;
            textPotion.text = "x " + potion;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 만약 부딪힌 상대가 Item 이라면
        if(other.gameObject.CompareTag("Item"))
        {
            // 포션갯수를 1개 증가시키고 UI도 표현하고 싶다.
            POTION++;
            // 아이템 게임오브젝트는 파괴하고 싶다.
            Destroy(other.transform.parent.gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        playerHP = GetComponent<PlayerHP>();
    }

    // Update is called once per frame
    void Update()
    {
        // 만약 1번키를 눌렀다면 포션이 1개 이상 있다면 체력을 최대 체력으로 하고 싶다.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(POTION > 0)
            {
                playerHP.HP = playerHP.maxHP;
                POTION--;
            }            
        }
    }
}
