using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public interface IDeathLoad
{
    public void RewriteData();
    public void LoadCheckpoint();
}