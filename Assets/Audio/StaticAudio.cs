using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StaticAudio : MonoBehaviour
{
    public static StaticAudio Instance { get; private set; }

    public List<AudioClip> backgroundTracks;
    [SerializeField] private AudioSource musicSource;

    public AudioSource buttonClickSource;
    public AudioSource hintSource;

    void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);

            ChangeBackgroundTrack(SceneManager.GetActiveScene().buildIndex);

            SceneManager.activeSceneChanged += (_, nextScene) => ChangeBackgroundTrack(nextScene.buildIndex);
            SubscribeButtonsOnSound();
        }
    }

    void ChangeBackgroundTrack(int sceneIndex)
    {
        if (sceneIndex >= backgroundTracks.Count)
        {
            Debug.Log("Add new background tracks!");
            return;
        }
        musicSource.clip = backgroundTracks[sceneIndex];
        musicSource.Play();
        //...
    }

    void SubscribeButtonsOnSound()
    {
        foreach (var doc in FindObjectOfType<PanelManager>().docs)
            doc.rootVisualElement.Query<Button>().ForEach(b => b.clicked += buttonClickSource.Play);
    }
}
