using System.Collections;
using UnityEngine;

public class SpiderBoy : MonoBehaviour
{
    [SerializeField]
    private float SpawnGroupDelay;

    [SerializeField]
    private float SpawnSpiderDelay;

    [SerializeField]
    private int SpiderGroup;

    [SerializeField]
    private GameObject SpiderPrefab;

    private float positionX;

    private void Start()
    {
        positionX = transform.position.x;
        positionX -= 1;

        InvokeRepeating("SpawnSpiders", .5f, SpawnGroupDelay);
    }

    public IEnumerator SpawnSpiderGroup()
    {
        for (int i = 0; i < SpiderGroup; i++)
        {
            yield return new WaitForSeconds(SpawnSpiderDelay);
            Instantiate(SpiderPrefab, new Vector3(positionX, 0, 0), Quaternion.identity);
        }
        
    }

    //add check if player is near
    public void SpawnSpiders()
    {
        StartCoroutine(SpawnSpiderGroup());
    }

}
