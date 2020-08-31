using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    static private MusicManager inst;
    static public MusicManager Inst
    {
        get { return inst; }
    }

    public int currentSong = -1;
    public AudioClip[] songs;
    private AudioSource audioSource;

    private void Awake()
    {
        inst = this;
        audioSource = GetComponent<AudioSource>();
        songs = Resources.LoadAll<AudioClip>("Sounds");
        NextSong();
    }

    public void NextSong()
    {
        currentSong++;
        if (currentSong == songs.Length)
        {
            currentSong = 0;
        }

        audioSource.clip = songs[currentSong];
        audioSource.Play();
    }
}
