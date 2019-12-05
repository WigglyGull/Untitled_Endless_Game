using UnityEngine;

public class Chunk : MonoBehaviour{
    public GameObject[] platform;
    public GameObject[] islands;
    public GameObject rocks;
    GameObject newPlatform;
    GameObject newRock;
    Vector2 spawnPos;

    float downAmount = 7;
    float totalAmount;
    float ranDistance;

    void Start() {
        spawnPos = transform.position;
        InvokeRepeating("SpawnTiles", 0f, 0.02f);
        for (int i = 0; i < 3; i++){
            newRock = Instantiate(rocks, new Vector3(Random.Range(transform.position.x, transform.position.x + 8), Random.Range(transform.position.y, transform.position.y - 8), 5), Quaternion.identity);
            newRock.transform.parent = gameObject.transform;
        }
        ranDistance = Random.Range(0, 3);
        if(ranDistance == 0){
            GameObject newIsland = Instantiate(islands[Random.Range(0, islands.Length)], new Vector2(Random.Range(transform.position.x, transform.position.x + 8), Random.Range(transform.position.y, transform.position.y - 8)), Quaternion.identity);
            newIsland.transform.parent = gameObject.transform;
        }
    }

    public void SpawnTiles(){
        if(totalAmount >= 63){
            CancelInvoke("SpawnTiles");
        }

        if(totalAmount >= downAmount){
            ranDistance = Random.Range(0, 3);
            if(ranDistance == 0){
                spawnPos.Set(transform.position.x, spawnPos.y -= 0.9f);
            }else if(ranDistance == 1){
                spawnPos.Set(transform.position.x + 0.3f, spawnPos.y -= 0.9f);
            }else{
                spawnPos.Set(transform.position.x - 0.3f, spawnPos.y -= 0.9f);
            }
            downAmount += 7;
        }
             
        newPlatform = Instantiate(platform[Random.Range(0, platform.Length)], spawnPos, Quaternion.identity);
        newPlatform.transform.parent = gameObject.transform;
            
        ranDistance = Random.Range(0, 3);
        if(ranDistance == 0){
            spawnPos.Set(spawnPos.x += 0.9f, spawnPos.y);
        }else if(ranDistance == 1){
            spawnPos.Set(spawnPos.x += 1.2f, spawnPos.y);
        }else{
            spawnPos.Set(spawnPos.x += 1.5f, spawnPos.y);
        }
        totalAmount++;
    }
}