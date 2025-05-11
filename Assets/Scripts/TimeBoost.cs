using UnityEngine;
using System.Collections;


public class TimeBoost : MonoBehaviour
{
    public GameObject TimeBoostItemPrefab;

    void Start()
    {
        StartCoroutine(TimeBoostItemSpawnCoroutine( 15f ));
    }
    
    IEnumerator TimeBoostItemSpawnCoroutine(float spawnInterval)
    {
        while (true)
        {
          yield return new WaitForSeconds(spawnInterval);

            Vector3 spawnPos = new Vector3( Random.Range(-14.43f, 14.83f) , 1.3f , Random.Range(-10.81f, 3.4f) );

            Instantiate(TimeBoostItemPrefab, spawnPos, TimeBoostItemPrefab.transform.rotation);
        }
    }
}