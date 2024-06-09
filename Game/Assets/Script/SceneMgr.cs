using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    public Button button;

    void Start()
    {
        button.onClick.AddListener(LoadMainScene);
    }

    // Update is called once per frame
    void LoadMainScene()
    {
        SceneManager.LoadScene("Main");
    }
}
