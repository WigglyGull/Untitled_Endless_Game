using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkChunk : MonoBehaviour{
    public GameObject spark;
    SpriteRenderer[] sp;
    ParticleSystem[] ps;
    Transform player;
    float distance;

    bool found;

    void Start(){
        for(int i = 0; i < Random.Range(2, 5); i++){
            float x = Random.Range(transform.position.x + 0.5f, transform.position.x - 0.5f);
            float y = Random.Range(transform.position.y + 0.5f, transform.position.y - 0.5f);
            GameObject newSpark = Instantiate(spark, new Vector2(x, y), Quaternion.identity);
            newSpark.transform.parent = gameObject.transform;
        }
    }

    void Update(){
        sp = GetComponentsInChildren<SpriteRenderer>();
        ps = GetComponentsInChildren<ParticleSystem>();
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        distance = Vector2.Distance(transform.position, player.position);

        if(distance < 1.8f){
            found = true;
        }

        if(found == true){
            foreach(SpriteRenderer sprite in sp){
                if(sprite == null) return;
                sprite.enabled = true;
            }
            foreach(ParticleSystem particle in ps){
                if(particle == null) return;
                var em = particle.emission;
                em.enabled = true;
            }
            if(distance > 4){
                Destroy(gameObject);
            }
        }
    }
}
