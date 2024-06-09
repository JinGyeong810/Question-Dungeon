using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpenLoader : MonoBehaviour
{
    public Button button;

    void Start()
    {
        button.onClick.AddListener(LoadOpeningScene);
    }
    // Update is called once per frame
    void LoadOpeningScene()
    {
        SceneManager.LoadScene("OpeningScene");
    }
}
