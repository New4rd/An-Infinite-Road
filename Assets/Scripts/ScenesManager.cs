using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{

    static private ScenesManager inst;
    static public ScenesManager Inst
    {
        get { return inst; }
    }

    private GameObject canvas;
    private GameObject panel;


    private void Awake()
    {
        inst = this;
    }

    private IEnumerator Start()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("InterfaceScene", LoadSceneMode.Additive);
        yield return new WaitUntil(() => op.isDone);
        canvas = GameObject.Find("Canvas Interface");
        panel = canvas.transform.GetChild(2).gameObject;
        Debug.Log(panel.name);
        panel.SetActive(false);
    }

    public void SwitchMusicCredits ()
    {
        if (panel.activeSelf)
        {
            panel.SetActive(false);
            return;
        }
        panel.SetActive(true);
    }
}
