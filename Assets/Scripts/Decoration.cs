using UnityEngine;

public class Decoration : MonoBehaviour{
    public GameObject[] decorations;
    public Sprite[] crystalSprites;
    public Sprite[] bushSprites;
    public Sprite[] signSprites;

    float randomNum;
    Vector2 pos;

    void Start(){
        int decorationNum = Random.Range(0, decorations.Length);

        GameObject decor = Instantiate(decorations[decorationNum], transform.position, Quaternion.identity);
        decor.transform.parent = gameObject.transform;
        
        pos = decor.transform.position;
        if(decorationNum == 1){
            pos.y -= 0.006f;
        }
        decor.transform.position = pos;

        SpriteRenderer decorSprite = decor.GetComponent<SpriteRenderer>();

        if(decorationNum == 0){
            decorSprite.sprite = crystalSprites[Random.Range(0, crystalSprites.Length)];
        }else if(decorationNum == 1){
            decorSprite.sprite = bushSprites[Random.Range(0, bushSprites.Length)];
        }else if(decorationNum == 2){
            decorSprite.sprite = signSprites[Random.Range(0, signSprites.Length)];
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Tile" || other.tag == "Platform"){
            Destroy(gameObject);
        }
    }
}
