using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 태어날 때 체력을 최대 체력으로 하고 싶다.
// 그리고 화면에 포현하고 싶다.
// 적과 플레이어가 충돌했을 때 체력을 1 감소하고 싶다.
// 체력이 0이 되었을 때 게임오버 처리하고 싶다.
public class PlayerHP : MonoBehaviour
{
    public int hp;
    public int maxHP = 3;
    public Slider sliderHP;

    // Start is called before the first frame update
    void Start()
    {
        // 태어날 때 체력을 최대 체력으로 하고 싶다.
        // 그리고 화면에 포현하고 싶다.
        sliderHP.maxValue = maxHP;
        HP = maxHP;
    }

    public int HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            sliderHP.value = hp;
        }
    }
    
    //public void SetHP(int value)
    //{
    //    hp = value;
    //    sliderHP.value = hp;
    //}

    //public int GetHP()
    //{
    //    return hp;
    //}

    // Update is called once per frame
    void Update()
    {
    }
}
