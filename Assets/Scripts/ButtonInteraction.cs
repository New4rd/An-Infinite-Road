using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    public void LaunchNextSong()
    {
        MusicManager.Inst.NextSong();
    }

    public void SwitchBlinking()
    {
        ColorRythmic cr = GameObject.Find("M U S I C").GetComponent<ColorRythmic>();
        if (cr.enabled) cr.enabled = false;
        else cr.enabled = true;
    }
}
