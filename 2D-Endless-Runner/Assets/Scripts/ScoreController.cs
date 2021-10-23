using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{

    [Header("Score Hightlight")]
    public int scoreHrange;
    public CharacterSoundController sound;

    private int lastScoreH = 0;
    private int currscore = 0;
    // Start is called before the first frame update
    private void Start()
    {
        // reset
        currscore = 0;
        lastScoreH = 0;
    }

    public float Getcurrscore()
    {
        return currscore;
    }

    public void Increasecurrscore(int increment)
    {
        currscore += increment;

        if(currscore - lastScoreH > scoreHrange)
        {
            sound.PlayScoreH();
            lastScoreH += scoreHrange;
        }
    }

    public void FinishScore()
    {
        // set high Score
        if(currscore > ScoreData.hScore)
        {
            ScoreData.hScore = currscore;
        }
    }
    
}
