using UnityEngine;

public class Cloud : MonoBehaviour{
    SpriteRenderer sp;
    public Sprite[] clouds;
    public Sprite[] furtherClouds;
    Transform player;

    float speed;
    float distance;

    void Start(){
        sp = GetComponent<SpriteRenderer>();
        sp.sprite = clouds[Random.Range(0, clouds.Length)];
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        speed = Random.Range(0.2f, 0.6f);
        if(speed >= 0.3f && speed <= 0.4f){
            sp.sortingOrder = 1;
        }else if(speed >= 0.4f && speed <= 0.5f){
            sp.sortingOrder = 2;
        }else if(speed >= 0.5f && speed <= 0.6f){
            sp.sortingOrder = 3;
        }

        if(sp.sortingOrder == 0 || sp.sortingOrder == 1){
            if(sp.sprite == clouds[0]){
                sp.sprite = furtherClouds[0];
            }else if(sp.sprite == clouds[1]){
                sp.sprite = furtherClouds[1];
            }else if(sp.sprite == clouds[2]){
                sp.sprite = furtherClouds[2];
            }else if(sp.sprite == clouds[3]){
                sp.sprite = furtherClouds[3];
            }else if(sp.sprite == clouds[4]){
                sp.sprite = furtherClouds[4];
            }else if(sp.sprite == clouds[5]){
                sp.sprite = furtherClouds[5];
            }
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
