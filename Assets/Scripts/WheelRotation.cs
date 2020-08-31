using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{

    public float rotationSpeed = .1f;

    private void Update()
    {
        transform.Rotate(rotationSpeed, 0, 0, Space.Self);
    }
}
