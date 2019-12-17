using UnityEngine;
using UnityEngine.UI;

public class Ui : MonoBehaviour{
    public Text levelText;
    public Text SparkText;
    public Slider experienceBar;
    public Image itemSocket;
    public Sprite[] itemSockets;
    public Animator sparkAnim;
    PlayerStats playerStats;
    ExpBarAnimator expBarAnimated;

    void Awake() {
        PlayerStats playerStats = new PlayerStats();
        SetLevel(playerStats);

        ExpBarAnimator expBar = new ExpBarAnimator(playerStats);
        SetLevelAnimatied(expBar);
    }

    void Update(){
        if(Input.GetMouseButtonDown(0)) playerStats.AddExperience(10);
    }

    public void SetSocketSprite(){
        var itemNum = playerStats.GetItemNum();
        if(itemNum == 0){
            itemSocket.sprite = itemSockets[0];
        }else if(itemNum == 1){
            itemSocket.sprite = itemSockets[1];
        }
    }

    public void AddExperience(){
        playerStats.AddExperience(10);
    }

    public void AddSparks(){
        playerStats.AddSparks(sparkAnim);
        SetSparkNum();
    }

    void SetExperienceBarSize(float experienceNormalized){
        experienceBar.value = experienceNormalized;
    }

    void SetLevelNum(int levelNumber){
        levelText.text = (levelNumber + 1).ToString();
    }

    public void SetSparkNum(){
        SparkText.text = "x" + playerStats.GetSparks().ToString();
    }

    public void SetLevel(PlayerStats playerStats){
        this.playerStats = playerStats;
    }

    public void SetLevelAnimatied(ExpBarAnimator expBarAnimated){
        this.expBarAnimated = expBarAnimated;

        SetLevelNum(playerStats.GetLevelNumber());
        SetSparkNum();
        SetExperienceBarSize(playerStats.GetExperienceNormalized());

        expBarAnimated.OnExprienceChanged += PlayerStats_OnExprienceChanged;
        expBarAnimated.OnExprienceChanged += PlayerStats_OnLevelChanged;
    }

    void PlayerStats_OnExprienceChanged(object sender, System.EventArgs e){
        SetExperienceBarSize(expBarAnimated.GetExperienceNormalized());
    }

    void PlayerStats_OnLevelChanged(object sender, System.EventArgs e){
        SetLevelNum(expBarAnimated.GetLevelNumber());
    }
}
