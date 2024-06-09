using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameMgr : MonoBehaviour
{
    public Button buttonA;
    public Button buttonB;

    private string value;

    public void Start()
    {
        buttonA.onClick.AddListener(OnButtonAClick);
        buttonB.onClick.AddListener(OnButtonBClick);

    }
    void OnButtonAClick()
    {
        value = "CommonSense";
        PlayerPrefs.SetString("Category", value);
    }
    void OnButtonBClick()
    {
        value = "Arith";
        PlayerPrefs.SetString("Category", value);
    }
}
