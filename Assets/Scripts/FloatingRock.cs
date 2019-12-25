using UnityEngine;

public class FloatingRock : MonoBehaviour{
    public RuntimeAnimatorController[] rocks;
    float speed;
    Vector3 spawnPos;

    float topY;
    float bottomY;
    float distance;

    bool up;
    bool visible;

    void Start(){
        GetComponent<Animator>().runtimeAnimatorController = rocks[Random.Range(0, rocks.Length)];
        distance = Random.Range(0.1f, 0.5f);
        topY = transform.position.y + distance;
        bottomY = transform.position.y - distance;
    }

    void Update(){
        if(!visible) return;
        speed = Random.Range(0.05f, 0.2f);
        spawnPos = transform.position;

        if(up){
            spawnPos.y += speed * Time.deltaTime;
        }else{
            spawnPos.y -= speed * Time.deltaTime;
        }

        if(spawnPos.y > topY){
            up = false;
            speed = Random.Range(0.05f, 0.15f);
        }else if(spawnPos.y < bottomY){
            up = true;
            speed = Random.Range(0.05f, 0.15f);
        }

        transform.position = spawnPos;   
    }

    void OnBecameVisible(){
        visible = true;
    }

    void OnBecameInvisible(){
        visible = false;
    }
}
