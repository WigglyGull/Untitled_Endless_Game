using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Parallax : MonoBehaviour{
    public GameObject[] backgrounds;
    Transform cam;
    float[] parallaxScales;
    public float smoothing = 1f;
    Vector3 previousCamPos;

    float parallax;
    float backgroundTargetPosX;
    Vector3 backgroundTargetPos;

    void Awake(){
        cam = Camera.main.transform;
    }

    void Start(){
        previousCamPos = cam.position;
    }

    void LateUpdate(){
        SetBackground();
        for (int i = 0; i < backgrounds.Length; i++){
            parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
            backgroundTargetPosX = backgrounds[i].transform.position.x + parallax;
            backgroundTargetPos.Set(backgroundTargetPosX, backgrounds[i].transform.position.y, backgrounds[i].transform.position.z);
            backgrounds[i].transform.position = Vector3.Lerp(backgrounds[i].transform.position, backgroundTargetPos, smoothing * Time.deltaTime);
        }
        previousCamPos = cam.position;
    }

    void SetBackground(){
        backgrounds = GameObject.FindGameObjectsWithTag("FloatingRock");
        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++){
            parallaxScales[i] = backgrounds[i].transform.position.z * -1;
        }
    }
}