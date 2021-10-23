using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreController : MonoBehaviour
{
    [Header("UI")]
    public Text score;
    public Text hScore;

    [Header("Score")]
    public ScoreController scoreController;
    // Update is called once per frame
    private void Update()
    {
        score.text = scoreController.Getcurrscore().ToString();
        hScore.text = ScoreData.hScore.ToString();
    }
}
