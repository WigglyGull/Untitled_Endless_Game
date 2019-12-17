using UnityEngine;

public class Spark : MonoBehaviour{
    Transform player;
    bool move, found;
    Ui ui;

    float distance;

    void Update() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        distance = Vector2.Distance(transform.position, player.position);

        if(distance < 0.8f){
            move = true;
        }
        
        if(distance < 1.8f){
            found = true;
        }
        
        MoveTowards();
    }

    void MoveTowards(){
        if(move){
            Vector2 pos= transform.position;
            pos.x += (player.transform.position.x - pos.x) * (Time.deltaTime * 5);
            pos.y += (player.transform.position.y - pos.y) * (Time.deltaTime * 5);
            transform.position = pos;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            ui = FindObjectOfType<Ui>();
            ui.AddExperience();
            ui.AddSparks();
            Destroy(gameObject);
        }
        if(!move && !found){
            if(other.tag == "Tile" || other.tag == "Platform"){
                Destroy(gameObject);
            }
        }
    }
}
