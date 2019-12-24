using UnityEngine;

public class ButterflyBoxs : MonoBehaviour{
    Butterfly butterfly;
    BoxCollider2D boxCollider2D;

    bool spawned;
    void Start(){
        boxCollider2D = GetComponent<BoxCollider2D>();
        butterfly = GetComponentInParent<Butterfly>();

        spawned = true;
    }

    public void SetBox(){
        if(!spawned) return;
        boxCollider2D.transform.position = butterfly.target;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Platform" && spawned){
            butterfly.SetTarget();
        }
    }
}