using UnityEngine;

public class Spark : MonoBehaviour{
    Transform player;
    bool found, move;
    SpriteRenderer sprite;

    float speed;
    float distance;

    ParticleSystem ps;

    void Start() {
        sprite = GetComponent<SpriteRenderer>();
        ps = GetComponentInChildren<ParticleSystem>();
        speed = Time.deltaTime * 40;
    }


    void Update() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        distance = Vector2.Distance(transform.position, player.position);

        if(distance < 0.8f){
            move = true;
        }
        
        MoveTowards();
    }

    void MoveTowards(){
        if(move){
            Vector2 pos= transform.position;
            pos.x += ((player.transform.position.x - pos.x) * 0.1f) * speed;
            pos.y += ((player.transform.position.y - pos.y) * 0.1f) * speed;
            transform.position = pos;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            Destroy(gameObject);
        }
        if(other.tag == "Tile" || other.tag == "Platform"){
            if(move) return;
            Destroy(gameObject);
        }
    }
}
