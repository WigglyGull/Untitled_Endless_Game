using UnityEngine;

public class Grass : MonoBehaviour{
    Animator anim;
    public RuntimeAnimatorController[] grass;
    Player playerScript;

    void Start(){
        anim = GetComponent<Animator>();
        anim.runtimeAnimatorController = grass[Random.Range(0, grass.Length)];
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            playerScript = other.GetComponent<Player>();
            if(playerScript.fall || playerScript.jump){
                anim.SetTrigger("Jump");
            }
            if(playerScript.IsGrounded()){
                anim.SetTrigger("Walk");
            }
        }

        if(other.tag == "Tile" || other.tag == "Platform"){
            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            playerScript = other.GetComponent<Player>();
            if(playerScript.fall || playerScript.jump){
                anim.SetTrigger("Jump");
            }
            if(playerScript.IsGrounded()){
                anim.SetTrigger("Walk");
            }
        }
    }
}
