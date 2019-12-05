using UnityEngine;

public class CameraScript : MonoBehaviour{
    float smoothTimeY = 0.05f;
    float smoothTimeX = 0.05f;

    Vector2 velocity;
    Vector3 position;

    float posY;
    float posX;

    GameObject player;
    Player pScript;
    Camera cam;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
        pScript = player.GetComponent<Player>();
        cam = GetComponent<Camera>();
    }

    void FixedUpdate(){
        if (player != null){
            posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
            posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);
            position.Set(posX, posY, transform.position.z);
            transform.position = position;
        }
        if(pScript.sit){
            if(cam.orthographicSize > 1.6){
                cam.orthographicSize -= 0.03f;
            }
        }else if(cam.orthographicSize < 1.8){
            cam.orthographicSize += 0.03f;
        }
    }
}
