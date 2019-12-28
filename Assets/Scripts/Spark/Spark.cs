using UnityEngine;

public class Spark : MonoBehaviour{
    bool move, found;
    Ui ui;

    void Update() {
        Transform player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        float distance = Vector2.Distance(transform.position, player.position);

        if(distance < 0.8f)  move = true;
        if(distance < 1.8f) found = true;
        
        MoveTowards(player.position);
    }

    void MoveTowards(Vector2 player){
        if(move){
            Vector2 pos= transform.position;
            pos.x += (player.x - pos.x) * (Time.deltaTime * 5);
            pos.y += (player.y - pos.y) * (Time.deltaTime * 5);
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
