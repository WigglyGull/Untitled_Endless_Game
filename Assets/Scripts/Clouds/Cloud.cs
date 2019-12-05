using UnityEngine;

public class Cloud : MonoBehaviour{
    SpriteRenderer sp;
    public Sprite[] clouds;
    Transform player;

    float speed;
    float distance;

    void Start(){
        sp = GetComponent<SpriteRenderer>();
        sp.sprite = clouds[Random.Range(0, clouds.Length)];
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        speed = Random.Range(0.1f, 0.5f);
        if(speed >= 0.2f && speed <= 0.3f){
            sp.sortingOrder = 1;
        }else if(speed >= 0.3f && speed <= 0.4f){
            sp.sortingOrder = 2;
        }else if(speed >= 0.4f && speed <= 0.5f){
            sp.sortingOrder = 3;
        }
    }

    void Update(){
        transform.Translate(Vector2.left * (speed * Time.deltaTime), transform);

        distance = Vector2.Distance(transform.position, player.position);
        if(distance >= 12){
            Destroy(gameObject);
        }
    }
}
