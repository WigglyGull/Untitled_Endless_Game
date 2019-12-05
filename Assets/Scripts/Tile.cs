using UnityEngine;

public class Tile : MonoBehaviour{
    BoxCollider2D box2D;
    SpriteRenderer sp;
    Sprite leftSprite, rightSprite;
    public LayerMask groundLayerMask;

    RaycastHit2D raycastHitLeft2d, raycastHitRight2d, raycastHitUp2d, raycastHitDown2d;

    public Sprite[] tiles;

    bool down, up, right, left;

    float tileDestroy = 1f;
    bool shouldSetSprites = true;
    
    void Start() {
        box2D = GetComponent<BoxCollider2D>();
        sp = GetComponent<SpriteRenderer>();
    }

    public void Update(){
        if(shouldSetSprites){
            if(tileDestroy <= 0){
                shouldSetSprites = false;
            }else{
                SetSprites();
                tileDestroy -= Time.deltaTime;
            }  
        }
    }

    public void SetSprites(){
        down = Down();
        up = Up();
        right = Right();
        left = Left();

        if(!down && !up && right && left){
            sp.sprite = tiles[2];
            leftSprite = raycastHitLeft2d.transform.GetComponent<SpriteRenderer>().sprite;
            rightSprite = raycastHitRight2d.transform.GetComponent<SpriteRenderer>().sprite;
            //MiddleRow
            if(leftSprite == tiles[18] || rightSprite == tiles[20] || rightSprite == tiles[19] || leftSprite == tiles[16] || leftSprite == tiles[14] || rightSprite == tiles[15]){
                sp.sprite = tiles[19];
            }
            //LastRow 
            if(leftSprite == tiles[7] || leftSprite == tiles[12] || leftSprite == tiles[13] || leftSprite == tiles[8] || leftSprite == tiles[21] || rightSprite == tiles[21] || rightSprite == tiles[8] || rightSprite == tiles[9]){
                sp.sprite = tiles[21];
            }
        }else if(!right && left && !down && !up){
            sp.sprite = tiles[3];
            leftSprite = raycastHitLeft2d.transform.GetComponent<SpriteRenderer>().sprite;
            //MiddleRow
            if(leftSprite == tiles[19] || leftSprite == tiles[18] || leftSprite == tiles[16] || leftSprite == tiles[14]){
                sp.sprite = tiles[20];
            }
            //LastRow
            if(leftSprite == tiles[7] || leftSprite == tiles[8] || leftSprite == tiles[21]){
                sp.sprite = tiles[13];
            }
        }else if(right && !left && !down && !up){
            sp.sprite = tiles[1];
            rightSprite = raycastHitRight2d.transform.GetComponent<SpriteRenderer>().sprite;
            //MiddleRow
            if(rightSprite == tiles[19] || rightSprite == tiles[20] || rightSprite == tiles[16] || rightSprite == tiles[15]){
                sp.sprite = tiles[18];
            }
            //LastRow
            if(rightSprite == tiles[8] || rightSprite == tiles[9] || rightSprite == tiles[11] || rightSprite == tiles[21]){
                sp.sprite = tiles[12];
            }
        }else if(!down && up && !right && !left){
            sp.sprite = tiles[10];
        }else if(down && !up && !right&& !left){
            sp.sprite = tiles[11];
        }else if(!down && up && right && !left){
            sp.sprite = tiles[7];
        }else if(down && !up && right && !left){
            sp.sprite = tiles[6];
        }else if(down && !up && right && left){
            sp.sprite = tiles[5];
        }else if(!down && up && !right && left){
            sp.sprite = tiles[9];
        }else if(down && !up && !right && left){
            sp.sprite = tiles[4];
        }else if(!down && up && right && left){
            sp.sprite = tiles[8];
        }else if(down && up && right && !left){
            sp.sprite = tiles[14];
        }else if(down && up && !right && left){
            sp.sprite = tiles[16];
        }else if(down && up && right && left){
            sp.sprite = tiles[15];
        }else if(down && up && !right && !left){
            sp.sprite = tiles[17];
        }else{
            sp.sprite = tiles[0];
        }
    }

    public bool Down(){
        raycastHitDown2d = Physics2D.BoxCast(new Vector2(box2D.bounds.center.x, box2D.bounds.center.y - 0.17f), box2D.bounds.size/8, 0f, Vector2.down, 0.001f, groundLayerMask);
        if(raycastHitDown2d.collider == null)return false;
        if(raycastHitDown2d.collider.tag == "Tile"){
            return true;
        }else{
            return false;
        }
    }

    public bool Up(){
        raycastHitUp2d = Physics2D.BoxCast(new Vector2(box2D.bounds.center.x, box2D.bounds.center.y + 0.17f), box2D.bounds.size/8, 0f, Vector2.down, 0.001f, groundLayerMask);
        if(raycastHitUp2d.collider == null)return false;
        if(raycastHitUp2d.collider.tag == "Tile"){
            return true;
        }else{
            return false;
        }
    }

    public bool Right(){
        raycastHitRight2d = Physics2D.BoxCast(new Vector2(box2D.bounds.center.x + 0.17f, box2D.bounds.center.y), box2D.bounds.size/8, 0f, Vector2.right, 0.001f, groundLayerMask);
        if(raycastHitRight2d.collider == null)return false;
        if(raycastHitRight2d.collider.tag == "Tile"){
            return true;
        }else{
            return false;
        }
    }

    public bool Left(){
        raycastHitLeft2d = Physics2D.BoxCast(new Vector2(box2D.bounds.center.x - 0.17f, box2D.bounds.center.y), box2D.bounds.size/8, 0f, Vector2.left, 0.001f, groundLayerMask);
        if(raycastHitLeft2d.collider == null)return false;
        if(raycastHitLeft2d.collider.tag == "Tile"){
            return true;
        }else{
            return false;
        }
    }
}