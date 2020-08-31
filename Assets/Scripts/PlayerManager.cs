using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Le PlayerManager est le gestionnaire du joueur, il permet d'y accéder depuis n'importe quel
/// script. Il contient des fonctions relatives au joueur
/// </summary>


public class PlayerManager : MonoBehaviour
{
    static private PlayerManager inst;
    static public PlayerManager Inst
    {
        get { return inst; }
    }


    /// <summary>
    /// Prefab ou objet représentant le joueur dans le jeu
    /// </summary>
    public GameObject player;

    private void Awake()
    {
        inst = this;
    }


    /// <summary>
    /// Fonction permettant de retourner la position du joueur arrondie, selon l'axe Z. Cette fonction
    /// est dépendante du projet, et repose sur l'"index" du joueur, soit le nombre de chunks que celui-ci
    /// a parcourus.
    /// </summary>
    /// <returns>Valeur arrondie de la position du joueur selon l'axe Z, divisée par 8</returns>
    public int PlayerRoundedPosition()
    {
        return (int)player.transform.position.z / 8;
    }
}
