using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour{
    Transform target;
    Vector3 initalPos;
    
    bool isShaking = false;

    float pendingShakeDuration = 0f;
    float distance = 0.05f;

    void Start(){
        target = GetComponent<Transform>();
    }

    private void Update(){
        initalPos = target.localPosition;
        if(pendingShakeDuration > 0 && !isShaking){
            StartCoroutine(DoShake(distance));
        }
    }

    public void Shake(float duration, float distance){
        if (duration > 0){
            pendingShakeDuration += duration;
            this.distance = distance;
        }
    }

    IEnumerator DoShake(float distance){
        isShaking = true;
        var startTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < startTime + pendingShakeDuration){
            var randomPoint = new Vector3(Random.Range(transform.position.x-distance, transform.position.x+distance), Random.Range(transform.position.y - distance, transform.position.y + distance), transform.position.z);
            target.localPosition = randomPoint;
            yield return null;
        }
        EndShake();
    }

    void EndShake(){
        pendingShakeDuration = 0f;
        target.localPosition = initalPos;
        isShaking = false;
    }
}