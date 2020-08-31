using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// Script permettant de générer des prefabs de terrain au fur et à mesure que le joueur avance dans l'espace,
/// donnant le sentiment que la route est infinie. La gestion des chunks de terrains se base sur l'utilisation
/// d'une liste, que l'on va remplir et vider selon la position du joueur. Cette liste permet de garder une trace
/// de l'instanciation de chaque chunk, tant que celui-ci est chargé et affiché à l'écran.
/// </summary>


public class TerrainGenerator : MonoBehaviour
{

    static private TerrainGenerator inst;
    static public TerrainGenerator Inst
    {
        get { return inst; }
    }

    private GameObject chunk;                   // chunk de terrain "neutre" à charger
    private GameObject chunkPalm;               // chunk de terrain à charger, contenant les palmiers
    private GameObject chunkSign;               // chunk de terrain à charger, contenant les panneaux
    private List<GameObject> chunkList;         // liste des chunks à charger

    private int _playerCurrentRoundedPosition;  // indice de la position du joueur
    public int playerCurrentRoundedPosition
    {
        get { return _playerCurrentRoundedPosition; }
    }

    private void Awake()
    {
        inst = this;
        // chargement des différents prefabs
        chunk = Resources.Load("Prefabs/Small Chunk") as GameObject;
        chunkPalm = Resources.Load("Prefabs/Small Chunk Palm Trees") as GameObject;
        chunkSign = Resources.Load("Prefabs/Small Chunk Sign") as GameObject;

        // initialisation de la liste de chunks
        chunkList = new List<GameObject>();
        _playerCurrentRoundedPosition = 0;
    }


    /// <summary>
    /// Fonction effectuant la mise à jour des chunks selon la position du joueur à une frame donnée. Les
    /// instances sont créées/détruites, et les éléments de la liste sont ajoutés/supprimés en conséquence
    /// </summary>
    public void UpdateChunks ()
    {
        GameObject instance;

        // Si l'indice de position du joueur est impair, on charge le chunk contenant un panneau d'affichage
        if (PlayerManager.Inst.PlayerRoundedPosition() % DecorManager.Inst.signsFrequency == 1)
        {
            instance = Instantiate(chunkSign, new Vector3(0, -5, (PlayerManager.Inst.PlayerRoundedPosition() + DecorManager.Inst.distanceDisplay - 1) * 8), Quaternion.identity);
        }

        // Sinon, si l'indice de position est pair, on charge le chunk "neutre"
        else if (PlayerManager.Inst.PlayerRoundedPosition() % 2 == 0)
        {
            instance = Instantiate(chunk, new Vector3(0, -5, (PlayerManager.Inst.PlayerRoundedPosition() + DecorManager.Inst.distanceDisplay - 1) * 8), Quaternion.identity);
        }

        // Sinon, on charge le chunk contenant des palmiers
        else
        {
            instance = Instantiate(chunkPalm, new Vector3(0, -5, (PlayerManager.Inst.PlayerRoundedPosition() + DecorManager.Inst.distanceDisplay - 1) * 8), Quaternion.identity);
        }

        // Lancement de la routine d'élévation du terrain
        StartCoroutine(RiseTerrain(instance, -5));
        // on ajoute l'objet instancié à la liste des instances
        chunkList.Add(instance);
        // Lancement de la routine de destruction des chunks de terrain inutiles
        StartCoroutine(DelayedChunkDestroy());

        _playerCurrentRoundedPosition = PlayerManager.Inst.PlayerRoundedPosition();
    }


    /// <summary>
    /// Fonction effectuant la génération de terrain initiale, lors du lancement du programme. Les modalités
    /// de génération concernant la position en Z indexée du joueur sont les mêmes que pour la mise à jour
    /// continue des chunks de terrain
    /// </summary>
    public void InitialTerrainGeneration ()
    {
        GameObject instance;
        for (int i = 0; i <= DecorManager.Inst.distanceDisplay; i++)
        {
            if (i % 2 == 1)
            {
                instance = Instantiate(chunk, new Vector3(0, 0, i * 8), Quaternion.identity);
            }
            else if (i % DecorManager.Inst.signsFrequency == 0)
            {
                instance = Instantiate(chunkSign, new Vector3(0, 0, i * 8), Quaternion.identity);
            }
            else
            {
                instance = Instantiate(chunkPalm, new Vector3(0, 0, i * 8), Quaternion.identity);
            }
            chunkList.Add(instance);
        }
    }


    /// <summary>
    /// Fonction détruisant le premier élément de la liste des terrains instanciés, avec 1 seconde de décalage
    /// afin que la suppression ne soit pas visible par le joueur.
    /// </summary>
    /// <returns>Attente d'1 seconde</returns>
    private IEnumerator DelayedChunkDestroy ()
    {
        yield return new WaitForSecondsRealtime(1f);

        Destroy(chunkList.First());
        chunkList.RemoveAt(0);
    }


    /// <summary>
    /// Coroutine effectuant l'élévation du terrain jusqu'à sa position en y=0, permettant une apparition
    /// douce des chunks
    /// </summary>
    /// <param name="inst">Instance du prefab sur lequel effectuer l'élévation</param>
    /// <param name="y">Position de départ du prefab, selon l'axe Y</param>
    /// <returns></returns>
    private IEnumerator RiseTerrain (GameObject inst, int y)
    {
        for (float i = y; i <= 0; i+=.5f)
        {
            inst.transform.position = new Vector3(inst.transform.position.x, i, inst.transform.position.z);
            yield return new WaitForSecondsRealtime(.01f);
        }
        

    }
}
