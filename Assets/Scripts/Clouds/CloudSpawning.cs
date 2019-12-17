using UnityEngine;

public class CloudSpawning : MonoBehaviour{
    public GameObject cloud;
    public GameObject frontCloud;
    public GameObject gm;
    BackgroundManager bm;

    float spawnTime;
    float frontCloudTime;

    void Start() {
        bm = FindObjectOfType<BackgroundManager>();
    }

    void Update() {
        if(spawnTime <= 0){
            GameObject newCloud = Instantiate(cloud, new Vector3(Random.Range(transform.position.x - 2f, transform.position.x + 4f),Random.Range(transform.position.y - 10f, transform.position.y + 10f), 5), Quaternion.identity);
            newCloud.transform.parent = gm.transform;
            spawnTime = 0.7f;
            frontCloudTime++;
        }else{
            spawnTime -= Time.deltaTime;
        }

        if(frontCloudTime > 10){
            GameObject newCloud = Instantiate(frontCloud, new Vector3(Random.Range(transform.position.x - 2f, transform.position.x + 4f),Random.Range(transform.position.y - 10f, transform.position.y + 10f), 5), Quaternion.identity);            
            frontCloudTime = 0;
        }
    }
}