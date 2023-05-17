using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfGame : MonoBehaviour
{
    [SerializeField] private PanelManager parentPanelManager;

    private ComicsSwitcher comicsSwitcher;

    private void Awake()
    {
        comicsSwitcher = GetComponent<ComicsSwitcher>();

        comicsSwitcher.ComicsEndsEvent.AddListener(OnEndOfComics);
    }

    private IEnumerator Start()
    {
        StaticAudio.Instance.ChangeBackgroundTrack("endGameTheme");

        yield return new WaitForSecondsRealtime(parentPanelManager.PanelTweenDuration);

        comicsSwitcher.StartNGAfterComics = false;
        comicsSwitcher.ToNextPage();
    }

    private void OnEndOfComics()
    {
        // credits

        SceneManager.LoadScene("MainMenu");
    }
}
