using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawning : MonoBehaviour{
    public GameObject cloud;
    public GameObject gm;

    float spawnTime;


    void Update() {
        if(spawnTime < 0){
            GameObject newCloud = Instantiate(cloud, new Vector2(Random.Range(transform.position.x - 1f, transform.position.x + 2f),Random.Range(transform.position.y - 6f, transform.position.y + 6f)), Quaternion.identity);
            newCloud.transform.parent = gm.transform;
            spawnTime = 1f;
        }else{
            spawnTime -= Time.deltaTime;
        }
    }
}
