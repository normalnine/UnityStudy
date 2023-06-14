using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 태어날 때 점수를 0점으로 하고 싶다. UI로도 표현하고 싶다.
// 적이 총알과 부딪혔다면 점수를 1점 증가시키고 싶다.
public class ScoreManager : MonoBehaviour
{
    readonly string saveKey = "HIGH_SCORE";
    public static ScoreManager instance;

    private void Awake()
    {
        instance = this;
    }

    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textHighScore;
    int score;
    int highScore;

    // Start is called before the first frame update
    void Start()
    {
        //SetScore(0);
        SCORE = 0;
        //HIGH_SCORE = 0;
        HIGH_SCORE = PlayerPrefs.GetInt(saveKey, 0);

    }

    public int HIGH_SCORE
    {
        get
        {
            return highScore;
        }
        set
        {
            highScore = value;            
            textHighScore.text = "HighScore : " + highScore;
        }
    }

    public int SCORE
    {
        get 
        { 
            return score; 
        }
        set 
        {
            score = value;
            textScore.text = "Score : " + score;   
            
            //만약 score가 highScore보다 크다면 저장하고 싶다.
            if(score > highScore)
            {
                HIGH_SCORE = score;

                // 최고점 저장하기
                PlayerPrefs.SetInt(saveKey, highScore); 
            }
        }
    }

    //public void SetScore(int value)
    //{
    //    score = value;
    //    textScore.text = "Score : " + score;
    //}

    //public int GetScore()
    //{
    //    return score;
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
