using UnityEngine.SceneManagement;

public class SpawnpointCreator : Creator
{
    public Spawnpoint SpawnpointComponent { get => newGameObject.GetComponent<Spawnpoint>(); }
}
