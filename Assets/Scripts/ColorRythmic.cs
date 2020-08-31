using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;


public class ColorRythmic : MonoBehaviour
{
    public TextAsset beatConfig;
    public List<Material> materialToColorize;

    public List<Material> palmTreeMaterials;
    public List<Texture> palmTreeImages;
    public List<Texture> FLIPPEDPalmTreeImages;

    private bool blinking = false; bool funcEntry = true;
    private int state = 0;
    private string[] tempos;


    private void Awake()
    {
        string fs = beatConfig.text;
        tempos = Regex.Split(fs, "\n|\r|\r\n");
    }


    private void Start()
    {
        StartCoroutine(AlternateColors());
    }

    private void Update()
    {
        if (!blinking && funcEntry)
        {
            StartCoroutine(AlternateColors());
        }
    }

    private IEnumerator AlternateColors()
    {
        funcEntry = false;
        blinking = true;

        foreach (Material mat in materialToColorize)
        {
            if (state==1) mat.SetColor("_EmissionColor", Color.cyan);
            else mat.SetColor("_EmissionColor", Color.magenta);
        }

        palmTreeMaterials[0].mainTexture = palmTreeImages[state];
        palmTreeMaterials[1].mainTexture = FLIPPEDPalmTreeImages[state];

        

        state = 1 - state;
        yield return new WaitForSecondsRealtime(60 / float.Parse(tempos[MusicManager.Inst.currentSong]));

        blinking = false;
        funcEntry = true;
    }
}
