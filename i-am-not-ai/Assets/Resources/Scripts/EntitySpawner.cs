using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EntitySpawner : NetworkBehaviour {

    public GameObject floor;
    public GameObject enemyPrefab;
    public int numEnemies;

    public override void OnStartServer()
    {
        Vector3 offset = floor.transform.position;
        base.OnStartServer();
        for (int i = 0; i < numEnemies; i++)
        {
            var pos = offset+new Vector3(Random.Range(-8f, 8f), 0.2f, Random.Range(-8f, 8f));
            var rotation = Quaternion.Euler(0, Random.Range(0, 180), 0);

            var enemy = (GameObject)Instantiate(enemyPrefab, pos, rotation);
            NetworkServer.Spawn(enemy);
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
