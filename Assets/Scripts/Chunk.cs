using UnityEngine;

public class Chunk : MonoBehaviour{
    public GameObject[] platform;
    public GameObject[] islands;
    public GameObject rocks;
    public GameObject spark;

    GameObject newPlatform;
    GameObject newRock;
    Vector2 spawnPos;

    Tile[] tiles;

    float downAmount = 7;
    float totalAmount;
    float ranDistance;

    void Start() {
        spawnPos = transform.position;
        ranDistance = Random.Range(0, 3);
        InvokeRepeating("SpawnTiles", 0f, 0.02f);

        SpawnObjects(3, rocks);
        
        if(ranDistance == 0){
            GameObject newIsland = Instantiate(islands[Random.Range(0, islands.Length)], new Vector2(Random.Range(transform.position.x, transform.position.x + 8), Random.Range(transform.position.y, transform.position.y - 8)), Quaternion.identity);
            newIsland.transform.parent = gameObject.transform;
        }
    }

    void SpawnObjects(int time, GameObject gameobject){
        for (int i = 0; i < time; i++){
            newRock = Instantiate(gameObject, new Vector3(Random.Range(transform.position.x, transform.position.x + 8), Random.Range(transform.position.y, transform.position.y - 8), 5), Quaternion.identity);
            newRock.transform.parent = gameObject.transform;
        }
    }

    public void SpawnTiles(){
        if(totalAmount >= 60){
            tiles = FindObjectsOfType(typeof(Tile)) as Tile[];
            foreach(Tile tile in tiles){
                if(tile == null) return;
                tile.SetSprites();
            }
            CancelInvoke("SpawnTiles");
        }

        if(totalAmount >= downAmount){
            ranDistance = Random.Range(0, 3);
            if(ranDistance == 0){
                spawnPos.Set(transform.position.x, spawnPos.y -= 0.9f);
            }else if(ranDistance == 1){
                spawnPos.Set(transform.position.x + 0.3f, spawnPos.y -= 1.2f);
            }else{
                spawnPos.Set(transform.position.x - 0.3f, spawnPos.y -= 0.9f);
            }
            downAmount += 7;
        }
             
        newPlatform = Instantiate(platform[Random.Range(0, platform.Length)], spawnPos, Quaternion.identity);
        newPlatform.transform.parent = gameObject.transform;

        ranDistance = Random.Range(0, 11);
        if(ranDistance == 0){
            GameObject newSpark =Instantiate(spark, new Vector2(Random.Range(transform.position.x, transform.position.x + 10), Random.Range(transform.position.y, transform.position.y - 10)), Quaternion.identity);
            newSpark.transform.parent = gameObject.transform;
        }
            
        ranDistance = Random.Range(0, 3);
        if(ranDistance == 0){
            spawnPos.Set(spawnPos.x += 1.2f, spawnPos.y);
        }else if(ranDistance == 1){
            spawnPos.Set(spawnPos.x += 1.5f, spawnPos.y);
        }else{
            spawnPos.Set(spawnPos.x += 1.8f, spawnPos.y);
        }
        totalAmount++;
    }
}