using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour{

    public GameObject DayBackground;
    public GameObject NightBackground;
    public SpriteRenderer darkness;

    public Color dusk;
    public Color dawn;
    public Color normal;

    SpriteRenderer[] backgroundSp;
    SpriteRenderer sp;

    bool day = true;
    bool fadeBackground;
    bool fadeInBackGround;

    [Range(-10, 60)]
    public float time;

    void Start(){
        sp = DayBackground.GetComponent<SpriteRenderer>();
    }

    void Update(){
        time += Time.deltaTime;
        if(time > 30){
            fadeBackground = false;
            fadeInBackGround = false;
        }
        if(time > 40 && day){
            sp.color = Color.Lerp(sp.color, dusk, Time.deltaTime * 0.1f);
        }
        if(time >= 60){
            if(day){
                fadeBackground = true;
                time = -10;
                day = false;
            }else if(!day){
                sp.color = dawn;
                fadeInBackGround = true;
                time = -10;
                day = true;
            }
        }

        if(fadeBackground){
            Color darknessColor = darkness.color;
            Color spColor = sp.color;

            spColor.a = Mathf.Lerp(sp.color.a, 0, Time.deltaTime*0.15f);
            darknessColor.a = Mathf.Lerp(darkness.color.a, 0.25f, Time.deltaTime*0.1f);

            sp.color = spColor;
            darkness.color = darknessColor;
        }else if(fadeInBackGround){
            Color darknessColor = darkness.color;

            sp.color = Color.Lerp(sp.color, normal, Time.deltaTime * 0.15f);
            darknessColor.a = Mathf.Lerp(darkness.color.a, 0, Time.deltaTime*0.1f);

            darkness.color = darknessColor;
        }
        SetBackgroundSp();
    }

    void SetBackgroundSp(){
        backgroundSp = DayBackground.GetComponentsInChildren<SpriteRenderer>();
        foreach (var background in backgroundSp){
            background.color = sp.color;
        }
    }
}