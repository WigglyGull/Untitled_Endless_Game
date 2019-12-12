using UnityEngine;

public class Grass : MonoBehaviour{
    Animator anim;
    public RuntimeAnimatorController[] grass;
    Player playerScript;
    Vector2 Pos;

    [Range(-10, 10)]
    public float distance;

    void Start(){
        if(Random.Range(0, 3) == 0){
            Destroy(gameObject);
        }
        Pos = transform.position;
        Pos.x += Random.Range(-distance, distance);
        transform.position = Pos;

        anim = GetComponent<Animator>();
        anim.runtimeAnimatorController = grass[Random.Range(0, grass.Length)];
    }

    void OnTriggerStay2D(Collider2D other) {
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

    void OnDrawGizmos() {
        Gizmos.DrawLine(new Vector2(distance, transform.localPosition.y), new Vector2(-distance, transform.localPosition.y));
    }
}
