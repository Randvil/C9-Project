using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StaticAudio : MonoBehaviour
{
    public static StaticAudio Instance { get; private set; }

    public List<AudioClip> backgroundTracks; // Create elements from inspector, don't use "= new()" !

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource buttonClickSource;
    [SerializeField] private AudioSource hintSource;
    [SerializeField] private AudioSource deathSource;

    void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);

            SceneManager.activeSceneChanged += (_, _) => SubscribeButtonsOnSound();
            SceneManager.activeSceneChanged += (_, _) => musicSource.Stop();
        }
    }

    public void PlayEffect(eAudioEffect type)
    {
        switch (type)
        {
            case eAudioEffect.ButtonClick:
                buttonClickSource.Play();
                break;

            case eAudioEffect.Hint:
                hintSource.Play();
                break;

            case eAudioEffect.Music: // Should be used by ChangeBackground track method
                musicSource.Play();
                break;

            case eAudioEffect.Death:
                deathSource.Play();
                break;
        }
    }

    public void ChangeBackgroundTrack(string trackName)
    {
        musicSource.Stop();

        musicSource.clip = backgroundTracks.Find(x => x.name == trackName);
        if (musicSource.clip != null)
            musicSource.Play();
    }

    void SubscribeButtonsOnSound()
    {
        foreach (var doc in FindObjectsOfType<UIDocument>())
            doc.rootVisualElement.Query<Button>().ForEach(b => b.clicked += buttonClickSource.Play);
    }
}
