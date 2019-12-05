using UnityEngine;

public class Breath : MonoBehaviour{
    public float liveTime;
    SpriteRenderer sp;

    void Start() {
        sp = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if(liveTime < 0){
            Destroy(gameObject);
        }else{
            liveTime -= 1 * Time.deltaTime;
            var newColor = sp.color;
            newColor.a -= 2 * Time.deltaTime;
            sp.color = newColor;
        }
    }
}
