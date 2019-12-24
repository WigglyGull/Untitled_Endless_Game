using UnityEngine;

public class LightFollow : MonoBehaviour{
    public Transform player;

    void Update(){
        Vector2 target = player.position;
        target.x -= 0.06f;
        target.y -= 0.1f;
        transform.position = target;
    }
}
