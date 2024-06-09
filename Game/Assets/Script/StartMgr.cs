using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMgr : MonoBehaviour
{
    public Canvas uiCanvas;
    public Canvas canvas2;

    private int i=0;

    void Start()
    {
        uiCanvas.gameObject.SetActive(true);
        canvas2.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.anyKeyDown)
        {
            uiCanvas.gameObject.SetActive(false);
            canvas2.gameObject.SetActive(true);
        }
    }
}
