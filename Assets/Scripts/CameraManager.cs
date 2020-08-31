using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static private CameraManager inst;
    static public CameraManager Inst
    {
        get { return inst; }
    }

    public GameObject cameraToUse;

    private void Awake()
    {
        inst = this;
    }
}
