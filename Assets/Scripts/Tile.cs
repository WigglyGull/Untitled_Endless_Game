using UnityEngine;

public class Tile : MonoBehaviour{
    public BoxCollider2D box2D;
    SpriteRenderer sp;
    Sprite leftSprite, rightSprite;
    public LayerMask groundLayerMask;

    RaycastHit2D raycastHitLeft2d, raycastHitRight2d, raycastHitUp2d, raycastHitDown2d;

    public Sprite[] tiles;

    bool down, up, right, left;

    bool setThenSprites = true;
    
    void Start() {
        box2D = GetComponent<BoxCollider2D>();
        sp = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate() {
        if(setThenSprites){
            SetSprites();
            setThenSprites = false;
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
        }else if(!right && left && !down && !up){
            sp.sprite = tiles[3];
            leftSprite = raycastHitLeft2d.transform.GetComponent<SpriteRenderer>().sprite;
            //MiddleRow
            if(leftSprite == tiles[19] || leftSprite == tiles[18] || leftSprite == tiles[16] || leftSprite == tiles[14]){
                sp.sprite = tiles[20];
            }
        }else if(right && !left && !down && !up){
            sp.sprite = tiles[1];
            rightSprite = raycastHitRight2d.transform.GetComponent<SpriteRenderer>().sprite;
            //MiddleRow
            if(rightSprite == tiles[19] || rightSprite == tiles[20] || rightSprite == tiles[16] || rightSprite == tiles[15]){
                sp.sprite = tiles[18];
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
        }
    }

    public bool Down(){
        if(box2D == null) return false;
        raycastHitDown2d = Physics2D.BoxCast(new Vector2(box2D.bounds.center.x, box2D.bounds.center.y - 0.17f), box2D.bounds.size/7, 0f, Vector2.down, 0.01f, groundLayerMask);
        if(raycastHitDown2d.collider == null)return false;
        if(raycastHitDown2d.collider.tag == "Tile"){
            return true;
        }else{
            return false;
        }
    }

    public bool Up(){
        if(box2D == null) return false;
        raycastHitUp2d = Physics2D.BoxCast(new Vector2(box2D.bounds.center.x, box2D.bounds.center.y + 0.17f), box2D.bounds.size/7, 0f, Vector2.down, 0.01f, groundLayerMask);
        if(raycastHitUp2d.collider == null)return false;
        if(raycastHitUp2d.collider.tag == "Tile"){
            return true;
        }else{
            return false;
        }
    }

    public bool Right(){
        if(box2D == null) return false;
        raycastHitRight2d = Physics2D.BoxCast(new Vector2(box2D.bounds.center.x + 0.17f, box2D.bounds.center.y), box2D.bounds.size/7, 0f, Vector2.right, 0.01f, groundLayerMask);
        if(raycastHitRight2d.collider == null)return false;
        if(raycastHitRight2d.collider.tag == "Tile"){
            return true;
        }else{
            return false;
        }
    }

    public bool Left(){
        if(box2D == null) return false;
        raycastHitLeft2d = Physics2D.BoxCast(new Vector2(box2D.bounds.center.x - 0.17f, box2D.bounds.center.y), box2D.bounds.size/7, 0f, Vector2.left, 0.01f, groundLayerMask);
        if(raycastHitLeft2d.collider == null)return false;
        if(raycastHitLeft2d.collider.tag == "Tile"){
            return true;
        }else{
            return false;
        }
    }
}