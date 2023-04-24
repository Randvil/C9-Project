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

            SceneManager.activeSceneChanged += (_, _) => SubscribeButtonsOnSound();
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
