using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class TextsManager : MonoBehaviour
{

    static private TextsManager inst;
    static public TextsManager Inst
    {
        get { return inst; }
    }

    public TextAsset textFile;
    private string[] lines;

    private void Awake()
    {
        inst = this;

        string fs = textFile.text;
        lines = Regex.Split(fs, "\n|\r|\r\n");
    }

    public string GetText(int number)
    {
        if (number < 0 || number > TextAmount())
        {
            return "NULL";
        }
        return lines[number];
    }

    public int TextAmount ()
    {
        return lines.Length;
    }
}
