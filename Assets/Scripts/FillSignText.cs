using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillSignText : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Text>().text = TextsManager.Inst.GetText(Random.Range(0, TextsManager.Inst.TextAmount()));
    }
}
