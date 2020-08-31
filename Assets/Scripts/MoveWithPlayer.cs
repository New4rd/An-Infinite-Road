using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script permettant de suivre la position du joueur selon l'axe Z. 
/// </summary>

public class MoveWithPlayer : MonoBehaviour
{

    /// <summary>
    /// Distance à laquelle placer l'objet du joueur selon l'axe horizontal Z
    /// </summary>
    public float positionZAxis;

    void Update()
    {
        transform.position = new Vector3 (
            transform.position.x,
            transform.position.y,
            PlayerManager.Inst.player.transform.position.z + positionZAxis);
    }
}
