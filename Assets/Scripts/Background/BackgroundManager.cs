using UnityEngine;

public class BackgroundManager : MonoBehaviour{

    public GameObject DayBackground;
    public SpriteRenderer darkness;
    public SpriteRenderer sunset;

    public Color dusk;
    public Color dawn;
    public Color normal;

    Color sunsetColor;
    Color darknessColor;
    Color spColor;

    SpriteRenderer[] backgroundSp;
    SpriteRenderer[] clouds;
    SpriteRenderer sp;

    public bool day = true;
    bool fadeBackground;
    bool fadeInBackGround;

    [Range(-10, 60)]
    public float time;

    void Start(){
        sp = DayBackground.GetComponent<SpriteRenderer>();
        sunsetColor.a = 0;
    }

    void Update(){
        time += Time.deltaTime;
        if(time > 30){
            fadeBackground = false;
            fadeInBackGround = false;
        }
        if(time > 55 && day){
            sp.color = Color.Lerp(sp.color, dusk, Time.deltaTime * 0.05f);

            sunsetColor = sunset.color;
            sunsetColor.a = Mathf.Lerp(sunset.color.a, 0.1f, Time.deltaTime*0.1f);
            sunset.color = sunsetColor;
        }
        if(time >= 80){
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
            darknessColor = darkness.color;
            spColor = sp.color;
            sunsetColor = sunset.color;

            spColor.a = Mathf.Lerp(sp.color.a, 0, Time.deltaTime*0.15f);
            darknessColor.a = Mathf.Lerp(darkness.color.a, 0.40f, Time.deltaTime*0.1f);
            sunsetColor.a = Mathf.Lerp(sunset.color.a, 0, Time.deltaTime*0.1f);

            sp.color = spColor;
            darkness.color = darknessColor;
            sunset.color = sunsetColor;
        }else if(fadeInBackGround){
            darknessColor = darkness.color;

            sp.color = Color.Lerp(sp.color, normal, Time.deltaTime * 0.1f);
            darknessColor.a = Mathf.Lerp(darkness.color.a, 0, Time.deltaTime*0.1f);

            darkness.color = darknessColor;
        }
        SetBackgroundSp();
    }

    void SetBackgroundSp(){
        backgroundSp = DayBackground.GetComponentsInChildren<SpriteRenderer>();
        clouds = GetComponentsInChildren<SpriteRenderer>();
        foreach (var background in backgroundSp){
            background.color = sp.color;
        }
        foreach(var cloud in clouds){
            cloud.color = sp.color;
        }
    }
}