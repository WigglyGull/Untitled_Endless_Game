using UnityEngine;

public class Unspawnable : MonoBehaviour{
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Platform"){
            Destroy(other.gameObject);
        }
    }
}