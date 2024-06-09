using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scorePrinter : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    private int currentScore;
    void Start()
    {
        currentScore = PlayerPrefs.GetInt("Score");
        ScoreText.text = currentScore.ToString();
    }

}
