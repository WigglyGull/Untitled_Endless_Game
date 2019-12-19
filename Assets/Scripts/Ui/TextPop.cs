using UnityEngine.UI;
using UnityEngine;

public class TextPop : MonoBehaviour{
    Text text;
    Vector2 startPos;

    bool up = true;

    bool pop;
    float speed;

    void Start(){
        startPos = transform.position;
    }

    void Update(){
        if(!pop) return;
        MoveText();
    }

    public void Pop(){
        up = true;
        pop = true;
    }

    void MoveText(){
        speed = 200 * Time.deltaTime;
        if(up){
            transform.Translate(Vector2.up * speed, transform);
        }else{
            transform.Translate(Vector2.down * speed, transform);
        }

        if(transform.position.y > startPos.y+10 && up){
            up = false;
        } else if(transform.position.y <= startPos.y && !up){
            transform.position = startPos;
            pop = false;
            print("done");
        }
    }
}
