using UnityEngine;

public class Butterfly : MonoBehaviour{
    public RuntimeAnimatorController[] butterflys;

    Vector2 target;
    float targetX;
    float targetY;

    SpriteRenderer sp;

    float distance;

    void Start(){
        sp = GetComponent<SpriteRenderer>();

        Animator anim = GetComponent<Animator>();
        anim.runtimeAnimatorController = butterflys[Random.Range(0, butterflys.Length)];

        SetTarget();
        Flip();
    }

    void Update(){
        distance = Vector2.Distance(transform.position, target);
        transform.position = Vector2.MoveTowards(transform.position, target, 0.2f * Time.deltaTime);

        if(distance < 0.01f){
            SetTarget();
        }
        Flip();
    }

    void Flip(){
        if(targetX > transform.position.x){
            sp.flipX = true;
        }else{
            sp.flipX = false;
        }
    }

    void SetTarget(){
        targetX = Random.Range(transform.position.x + 0.15f, transform.position.x - 0.15f);
        targetY = Random.Range(transform.position.y + 0.15f, transform.position.y - 0.15f);

        target = new Vector2(targetX, targetY);
    }
}
