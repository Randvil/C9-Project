using UnityEngine;
using System.Collections;

public static class Coroutines
{
    public class CoroutineHelper : MonoBehaviour { }

    static MonoBehaviour s_Instance;

    static MonoBehaviour Instance
    {
        get
        {
            if (s_Instance == null)
            {
                var instance = new GameObject(nameof(Coroutines), typeof(CoroutineHelper));
                s_Instance = instance.GetComponent<CoroutineHelper>();
                instance.hideFlags = HideFlags.HideAndDontSave;
                Object.DontDestroyOnLoad(instance);
            }

            return s_Instance;
        }
    }

    /// <summary>
    /// Starts a coroutine
    /// </summary>
    /// <param name="routine">The coroutine to start</param>
    /// <returns>The started coroutine</returns>
    public static Coroutine StartCoroutine(IEnumerator routine) => Instance.StartCoroutine(routine);

    /// <summary>
    /// Stops a coroutine
    /// </summary>
    /// <param name="coroutine">The coroutine to stop</param>
    public static void StopCoroutine(ref Coroutine coroutine)
    {
        if (coroutine != null)
        {
            Instance.StopCoroutine(coroutine);
            coroutine = null;
        }
    }
}