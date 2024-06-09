using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class scoreMgr : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI StarText;
    public Image Star1;
    public Image Star2;
    public Image Star3;
    public Canvas lastCanvas;

    private int currentScore = 0;
    private int Star = 0;
    private int newScore;
    private int newStar;

    void Start()
    {
        PlayerPrefs.SetInt("Score", currentScore);
        if (PlayerPrefs.HasKey("Score"))
        { newScore = PlayerPrefs.GetInt("Score"); }

        ScoreText.text = newScore.ToString();

        PlayerPrefs.SetInt("Star", Star);

        Star1.gameObject.SetActive(false);
        Star2.gameObject.SetActive(false);
        Star3.gameObject.SetActive(false);
        lastCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey("Score"))
        { newScore = currentScore + PlayerPrefs.GetInt("Score"); }
        
        ScoreText.text = newScore.ToString();

        if (PlayerPrefs.HasKey("Star"))
        {
            newStar = Star + PlayerPrefs.GetInt("Star");

            if (newStar == 1)
            { Star1.gameObject.SetActive(true); }

            if (newStar == 2)
            {
                Star1.gameObject.SetActive(true);
                Star2.gameObject.SetActive(true); 
            }
            if (newStar == 3)
            {
                Star1.gameObject.SetActive(true);
                Star2.gameObject.SetActive(true); 
                Star3.gameObject.SetActive(true);
                lastCanvas.gameObject.SetActive(true);
            }

        }
    }

}
