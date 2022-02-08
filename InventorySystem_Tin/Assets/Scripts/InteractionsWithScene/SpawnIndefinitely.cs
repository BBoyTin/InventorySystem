using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIndefinitely : MonoBehaviour
{
    [SerializeField]
    ItemInWorld _itemInWorld;

    [Space]

    [SerializeField]
    KeyCode _keyCodeToSpawn = KeyCode.Space;

    [SerializeField]
    Item[] _itemsToSpawn;


    private void Update()
    {
        SpawnIfKeyDown();
    }

    private void SpawnIfKeyDown()
    {
        if (Input.GetKeyDown(_keyCodeToSpawn))
        {
            _itemInWorld.SetItem(_itemsToSpawn[Random.Range(0, _itemsToSpawn.Length)]);
            Instantiate(_itemInWorld,this.transform.position+new Vector3(Random.Range(-2f,2f),Random.Range(-2f,2f)),Quaternion.identity);
        }
    }
}
