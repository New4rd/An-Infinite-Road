using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsMovement : MonoBehaviour
{

    void Update()
    {
        transform.position = new Vector3(-2.7f, transform.position.y, transform.position.z + (PlayerMovement.Inst.speed-.10f) * Time.deltaTime);

    }
}
