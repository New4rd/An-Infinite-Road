using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// Ce script permet d'effectuer la génération des décorations entourant la route. La génération est aléatoire, peut être
/// paramétrée selon des critères de quantité, d'intervalle de position, et de taille. Cette génération se base sur l'utilisation
/// d'une liste de listes d'objets, permettant de conserver une trace des prefabs et de les supprimer au besoin.
///
/// La liste contient des listes, qui contiennent elles-mêmes des décorations instanciées à chaque mise à jour de l'index
/// de position du joueur.
/// </summary>


public class DecorationGenerator : MonoBehaviour
{

    static private DecorationGenerator inst;
    static public DecorationGenerator Inst
    {
        get { return inst; }
    }

    private GameObject decoration;                      // objet de décoration à instancier
    private List<List<GameObject>> decorationList;      // liste des décorations instanciées

    private void Awake()
    {
        inst = this;
        decorationList = new List<List<GameObject>>();
        decoration = Resources.Load("Prefabs/Triangle") as GameObject;  // chargement de la décoration (pyramide)
    }


    /// <summary>
    /// Fonction effectuant la mise à jour des décorations sur le terrain selon la modification de l'index de position du joueur.
    /// Pour chaque chunk de terrain, la fonction génère un nombre aléatoire de décorations, seuillé par une valeur.
    /// Pour chaque décoration, sa position (x,y) relative au chunk est attribuée aléatoirement, selon des seuils spécifiés.
    /// La fonction instancie ces décorations dans l'espace, les supprime au besoin, et met à jour la liste d'objets
    /// </summary>
    public void UpdateDecorations()
    {
        float _randomScale;

        // création d'une liste qui contiendra les objets à instancier
        List<GameObject> decorations = new List<GameObject>();

        for (int i = 0; i < Random.Range(0, DecorManager.Inst.maximumDecorationAmount); i++)
        {
            _randomScale = Random.Range(DecorManager.Inst.minimumSize, DecorManager.Inst.maximumSize);

            GameObject instLeft = Instantiate(
                decoration,
                new Vector3(
                    Random.Range(-DecorManager.Inst.minimumXGeneration, -DecorManager.Inst.maximumXGeneration),
                    -_randomScale,
                    (PlayerManager.Inst.PlayerRoundedPosition() + DecorManager.Inst.distanceDisplay - 1) * 8 + Random.Range(0, 8)),
                Quaternion.identity);

            instLeft.transform.localScale = new Vector3(_randomScale, _randomScale, _randomScale);


            _randomScale = Random.Range(DecorManager.Inst.minimumSize, DecorManager.Inst.maximumSize);

            GameObject instRight = Instantiate(
                decoration,
                new Vector3(
                    Random.Range(DecorManager.Inst.minimumXGeneration, DecorManager.Inst.maximumXGeneration),
                    -_randomScale,
                    (PlayerManager.Inst.PlayerRoundedPosition() + DecorManager.Inst.distanceDisplay - 1) * 8 + Random.Range(0, 8)),
                Quaternion.identity);

            instRight.transform.localScale = new Vector3(_randomScale, _randomScale, _randomScale);

            decorations.Add(instLeft);
            decorations.Add(instRight);
        }
        decorationList.Add(decorations);

        StartCoroutine(DelayedDecorationsDestroy());
    }

    private IEnumerator DelayedDecorationsDestroy()
    {
        yield return new WaitForSecondsRealtime(1f);

        List<GameObject> firstDecorations = decorationList.First();
        foreach (GameObject dec in firstDecorations)
        {
            Destroy(dec);
        }
        decorationList.RemoveAt(0);
    }

    public void InitialDecorationsGeneration()
    {
        for (int i = 0; i < DecorManager.Inst.distanceDisplay; i++)
        {
            List<GameObject> decorations = new List<GameObject>();

            for (int j = 0; j < Random.Range(0, DecorManager.Inst.maximumDecorationAmount); j++)
            {
                GameObject inst = Instantiate(decoration,
                    new Vector3(
                        Random.Range(DecorManager.Inst.minimumXGeneration, DecorManager.Inst.maximumXGeneration),
                        0,
                        i + Random.Range(0,8)),
                    Quaternion.identity);

                decorations.Add(inst);
            }
            decorationList.Add(decorations);
        }
    }

    public void LoadMaterial (GameObject decoration)
    {

        ///// A COMPLETER
        Material mat = new Material(Shader.Find("Standard"));
        //mat.SetColor
    }
}
