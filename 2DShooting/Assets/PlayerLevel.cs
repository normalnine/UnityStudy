using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 적을 파괴될 때 경험치를 1 획득하고 싶다. UI에 표현하고 싶다.
// 경험치가 5가되면 레벨 업 하고 싶다.
// 레벨업을 하면 레벨업 VFX 를 재생하고 싶다.
public class PlayerLevel : MonoBehaviour
{
    public static PlayerLevel instance;

    private void Awake()
    {
        instance = this;
    }

    public ParticleSystem levelUpParticleSystem;

    long exp;
    public long needExp = 5;
    public RectTransform imageExp;
    public TextMeshProUGUI textExp;

    int level;
    public TextMeshProUGUI textLevel;
    
    public int LEVEL
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
            textLevel.text = "Lv " + level;
            StageLevel.instance.LEVEL++;
        }
    }

    public long EXP
    {
        get { return exp; }
        set
        {
            exp = value;
            // 만약 exp가 needExp 이상이라면
            long count = exp / needExp;
            for(int i = 0; i<count; i++)
            {
                if(exp >= needExp)
                {
                    // 레벨업 하고 싶다.
                    LEVEL++;
                    exp -= needExp;                   
                }
            }
            if(count > 0)
            {
                // VFX 연출                
                levelUpParticleSystem.Stop();
                levelUpParticleSystem.Play();
            }
            
            Vector3 ls = imageExp.localScale;
            float per = (float)exp / (needExp);
            ls.x = per;
            imageExp.localScale = ls;
            textExp.text = (per * 100) + "%";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        EXP = 0;
        LEVEL = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

