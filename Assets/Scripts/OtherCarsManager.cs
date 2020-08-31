using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherCarsManager : MonoBehaviour
{

    public GameObject cars;
    bool carIsPresent;
    GameObject instCar;

    private void Awake()
    {
        carIsPresent = false;
    }


    private void Update()
    {
        if (!carIsPresent && PlayerManager.Inst.PlayerRoundedPosition() % 100 == 0)
        {
            Debug.Log("SPAWN CAR");
            SpawnCar();
        }

        if (carIsPresent && (instCar.transform.position.z < PlayerManager.Inst.player.transform.position.z - 100))
        {
            Debug.Log("DESTROY CAR");
            Destroy(instCar);
            carIsPresent = false;
        }
    }


    private void SpawnCar ()
    {
        carIsPresent = true;
        instCar = Instantiate(cars, new Vector3(0, 0, (PlayerManager.Inst.PlayerRoundedPosition() + DecorManager.Inst.distanceDisplay - 1) * 8), Quaternion.identity);
    }
}
