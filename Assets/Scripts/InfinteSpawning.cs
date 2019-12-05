using System.Collections.Generic;
using UnityEngine;

public class InfinteSpawning : MonoBehaviour{
    public float renderDistance;
    public GameObject chunk;
    public Transform player;
    public GameObject[] islands;

    public static int size = 9;

    Dictionary<Vector2, Chunk> chunkMap = new Dictionary<Vector2, Chunk>();

    void Update(){
        FindChunks(player.position.x, player.position.y);
        RemoveChunk();
    }

    void FindChunks(float x, float y){
        int xPos = Mathf.RoundToInt(x);
        int yPos = Mathf.RoundToInt(y);
        for (int i = xPos - size; i < xPos +  (1.5f*size); i+=size){
            for (int j = yPos - size; j < yPos + (1.5f*size); j+=size){
                CreateChunk(i, j);
            }
        }
    }

    void CreateChunk(int i, int j){
        i = (Mathf.FloorToInt(i / (float)size) * size);
        j = (Mathf.FloorToInt(j / (float)size) * size);

        i -= (size/4);
        j += size;

        if(!chunkMap.ContainsKey(new Vector2(i, j))){
            GameObject go = Instantiate(chunk, new Vector2(i, j), Quaternion.identity);
            go.transform.parent = gameObject.transform;
            chunkMap.Add(new Vector2(i, j), go.GetComponent<Chunk>());
        }
    }

    void RemoveChunk(){
        List<Chunk> deleteChunk = new List<Chunk>(chunkMap.Values);
        Queue<Chunk> deleteQueue = new Queue<Chunk>();

        for (int i = 0; i < deleteChunk.Count; i++){
            float distance = Vector2.Distance(player.position, deleteChunk[i].transform.position);
            if(distance > renderDistance * size){
                deleteQueue.Enqueue(deleteChunk[i]);
            }
        }

        

        while (deleteQueue.Count > 0){
            Chunk chunk = deleteQueue.Dequeue();
            chunkMap.Remove(chunk.transform.position);

            Destroy(chunk.gameObject);
        }
    }
}