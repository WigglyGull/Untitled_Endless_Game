using UnityEngine;

public class Background : MonoBehaviour{
    public float speed, clamPos;
    float newPos;

    Vector2 startPos;

    void Start() {
        startPos = transform.position;
    }

    void Update(){
        newPos = Mathf.Repeat(Time.time * speed, clamPos);
        transform.localPosition = startPos + Vector2.left * newPos;
    }
}
