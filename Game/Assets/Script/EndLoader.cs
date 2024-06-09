using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndLoader : MonoBehaviour
{
    public Button button;

    void Start()
    {
        button.onClick.AddListener(LoadEndingScene);
    }
    // Update is called once per frame
    void LoadEndingScene()
    {
        SceneManager.LoadScene("EndingScene");
    }
}
