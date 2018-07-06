using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Vector3 spawnPoint = GameObject.FindGameObjectWithTag("PlayerWaitRoom").transform.position;
        spawnPoint.y += 1;
        this.gameObject.transform.position = spawnPoint ;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
