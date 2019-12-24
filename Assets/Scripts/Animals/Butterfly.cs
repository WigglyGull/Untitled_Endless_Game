using UnityEngine;

public class Butterfly : MonoBehaviour{
    public RuntimeAnimatorController[] butterflys;

    [HideInInspector] public Vector2 target;
    float targetX;
    float targetY;

    SpriteRenderer sp;
    ButterflyBoxs bb;

    float distance;

    void Start(){
        sp = GetComponent<SpriteRenderer>();
        bb = GetComponentInChildren<ButterflyBoxs>();

        Animator anim = GetComponent<Animator>();
        anim.runtimeAnimatorController = butterflys[Random.Range(0, butterflys.Length)];

        SetTarget();
        Flip();

        if(Random.Range(0, 4) != 0){
            Destroy(gameObject);
        }
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

    public void SetTarget(){
        targetX = Random.Range(transform.position.x + 0.15f, transform.position.x - 0.15f);
        targetY = Random.Range(transform.position.y + 0.15f, transform.position.y - 0.15f);

        target = new Vector2(targetX, targetY);
        bb.SetBox();
    }
}