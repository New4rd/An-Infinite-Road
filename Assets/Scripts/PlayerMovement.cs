using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script enregistrant les Inputs effectués au clavier, et permettant au joueur d'interagir.
/// Liste des contrôles actuels :
///     - Appuyer sur "Flèche Gauche" : si c'est possible, déplace le véhicule vers la voie de gauche
///     - Appuyer sur "Flèche Droite" : si c'est possible, déplace le véhicule vers la voie de droite
///     - Appuyer sur "Espace" : en gardant la touche pressée, permet au véhicule d'accélérer
///     - Appuyer sur "Flèche Bas" : en gardant la touche pressée, permet au véhicule de ralentir
/// </summary>


public class PlayerMovement : MonoBehaviour
{

    static private PlayerMovement inst;
    static public PlayerMovement Inst
    {
        get { return inst; }
    }

    private Vector3 pos;    // vecteur enregistrant la position du joueur dans l'espace

    // booléens liés aux fonctions de décalage du véhicule vers la gauche/droite
    private bool isStrafingLeft, isStrafingRight;
    // valeur stockant le nombre de décalages effectués pour que le décalage d'une position à l'autre
    // soit effectué
    private int strafeAmount = 0;

    /// <summary>
    /// Vitesse du véhicule
    /// </summary>
    public float speed = 50;

    /// <summary>
    /// Vitesse de déplacement du véhicule d'une position à l'autre (gauche/droite)
    /// </summary>
    public float strafeSpeed = 0.3f;

    /// <summary>
    /// Modificateur de vitesse indiquant le degré d'accélération/décélération effectué par le véhicule
    /// </summary>
    public float speedModification = 15f;


    private void Awake()
    {
        inst = this;
    }

    private void Start()
    {
        pos = transform.position;
        isStrafingLeft = false; isStrafingRight = false;
    }


    void Update()
    {
        pos = new Vector3(pos.x, pos.y, pos.z + speed * Time.deltaTime);

        // Si le joueur appuie sur "Flèche Gauche" et qu'il n'est pas sur la position la plus à gauche...
        if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > -2.7f) {
            // ... on autorise le décalage vers la gauche pour la fonction associée
            isStrafingLeft = true;
        }

        // Si le joueur appuie sur "Flèche Gauche" et qu'il n'est pas sur la position la plus à droite...
        if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < 2.7f) {
            // ... on autorise le décalage vers la droite pour la fonction associée
            isStrafingRight = true;
        }

        // Si le joueur garde enfoncée la touche "Flèche Bas" ou relâche la touche "Espace", on diminue la vitesse du véhicule
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.Space))
        {
            speed -= speedModification;
        }

        // Si le joueur garde enfoncée la touche "Espace" ou relâche la touche "Flèche Bas", on augment la vitesse du véhicule
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            speed += speedModification;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ScenesManager.Inst.SwitchMusicCredits();
        }

        // appel de la fonction de décalage (NOTE: rien ne se passera si au moins un des deux booléens n'est pas actif)
        ComputeStrafeStep();

        pos.x = Mathf.Round(pos.x * 10.0f) * 0.1f;  // on arrondit la position à 1 décimale pour éviter les erreurs numériques invisibles
        transform.position = pos;
    }


    /// <summary>
    /// La fonction de décalage effectue plusieurs modifications de position via une multitude de petits
    /// décalages vers la gauche ou vers la droite
    /// </summary>
    private void ComputeStrafeStep ()
    {
        if (isStrafingLeft)
        {
            pos.x -= strafeSpeed;
            strafeAmount++;

            if (pos.x <= -2.7f || strafeAmount >= 27 / (10*strafeSpeed))
            {
                isStrafingLeft = false;
                strafeAmount = 0;
            }
        }

        if (isStrafingRight)
        {
            pos.x += strafeSpeed;
            strafeAmount++;

            if (pos.x >= 2.7f || strafeAmount >= 27 / (10*strafeSpeed))
            {
                isStrafingRight = false;
                strafeAmount = 0;
            }
        }
    }
}
