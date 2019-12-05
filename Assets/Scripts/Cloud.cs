using UnityEngine;

public class Cloud : MonoBehaviour
{
    SpriteRenderer sp;
    public Sprite[] clouds;
    Transform player;

    public float speed;

    void Start(){
        sp = GetComponent<SpriteRenderer>();
        sp.sprite = clouds[Random.Range(0, clouds.Length)];
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update(){
        transform.Translate(Vector2.left * (speed * Time.deltaTime), transform);

        float distance = Vector2.Distance(transform.position, player.position);
        if(distance > 15){
            Destroy(gameObject);
        }
    }
}
