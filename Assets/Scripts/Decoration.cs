using UnityEngine;

public class Decoration : MonoBehaviour{
    public int decorations;
    public Sprite[] snowPile;
    public Sprite[] signs;
    public Sprite[] trashCans;

    SpriteRenderer sp;

    float randomNum;
    BoxCollider2D box2D;

    void Start(){
        sp = GetComponent<SpriteRenderer>();
        box2D = GetComponent<BoxCollider2D>();
        
        if(Random.Range(0, 2) == 0){
            Destroy(gameObject);
        }

        SetSprite();
    }

    void SetSprite(){
        int decorationNum = Random.Range(0, decorations);

        Vector2 pos = transform.position;
        Vector2 box = box2D.size;
        Vector2 boxTransform = box2D.offset;
        switch(decorationNum){
            case(0):
                sp.sprite = snowPile[Random.Range(0, snowPile.Length)];
                break;
            case(1):
                sp.sprite = signs[Random.Range(0, signs.Length)];
                pos.y += 0.14f;
                boxTransform.y += 0.1f;
                box.y += 0.2f;
                break;
            case(2):
                sp.sprite = trashCans[Random.Range(0, trashCans.Length)];
                pos.y += 0.12f;
                break;
        }
        transform.position = pos;
        box2D.size = box;
        box2D.offset = boxTransform;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Tile" || other.tag == "Platform"){
            Destroy(gameObject);
        }
    }
}
