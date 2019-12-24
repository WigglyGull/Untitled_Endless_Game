using UnityEngine;

public class BalanceCheck : MonoBehaviour{
    Player player;
    BoxCollider2D box2D;

    void Start(){
        player = GetComponentInParent<Player>();
        box2D = GetComponent<BoxCollider2D>();
    }

    void Update(){
        if(!IsGrounded()){
            player.balance = true;
            print("unBalanced");
        } else{
            player.balance = false;
        }
    }

    public bool IsGrounded(){
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(box2D.bounds.center, box2D.bounds.size, 0f, Vector2.down, 0.05f, player.groundLayerMask);
        return raycastHit2d.collider != null;
    }
}
