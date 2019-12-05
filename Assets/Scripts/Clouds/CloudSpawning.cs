using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawning : MonoBehaviour{
    public GameObject cloud;
    public GameObject gm;
    BackgroundManager bm;

    float spawnTime;

    void Start() {
        bm = FindObjectOfType<BackgroundManager>();
    }

    void Update() {
        if(spawnTime <= 0){
            GameObject newCloud = Instantiate(cloud, new Vector3(Random.Range(transform.position.x - 0.5f, transform.position.x + 4f),Random.Range(transform.position.y - 8f, transform.position.y + 8f), 5), Quaternion.identity);
            newCloud.transform.parent = gm.transform;
            spawnTime = 0.9f;
        }else{
            spawnTime -= Time.deltaTime;
        }
    }
}
