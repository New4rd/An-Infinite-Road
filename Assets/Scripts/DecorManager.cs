using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorManager : MonoBehaviour
{

    static private DecorManager inst;
    static public DecorManager Inst
    {
        get { return inst; }
    }

    public int distanceDisplay = 15;        // distance d'affichage maximale (~ nombre de chunks chargés)
    public int maximumDecorationAmount = 3; // quantité maximale de triangles à générer

    // coordonnées "seuil" autour desquelles les décorations peuvent apparaître
    public float minimumXGeneration = 10; public float maximumXGeneration = 20;
    // valeurs "seuil" permettant de générer les décorations selon des tailles minimales/maximales
    public float minimumSize = 1; public float maximumSize = 1;
    // fréquence d'apparition des panneaux directionnels
    public int signsFrequency = 10;

    private void Awake()
    {
        inst = this;
    }


    private void Start()
    {
        TerrainGenerator.Inst.InitialTerrainGeneration();
        DecorationGenerator.Inst.InitialDecorationsGeneration();
    }


    private void Update()
    {
        if (PlayerManager.Inst.PlayerRoundedPosition() != TerrainGenerator.Inst.playerCurrentRoundedPosition)
        {
            TerrainGenerator.Inst.UpdateChunks();
            DecorationGenerator.Inst.UpdateDecorations();
        }
    }
}
