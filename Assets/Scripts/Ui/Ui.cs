using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui : MonoBehaviour{
    public Text levelText;
    public Slider experienceBar;
    PlayerStats playerStats;

    void Update(){
        if(Input.GetMouseButtonDown(1)){
            playerStats.AddExperience(Random.Range(5, 20));
        }
    }

    void SetExperienceBarSize(float experienceNormalized){
        experienceBar.value = experienceNormalized;
    }

    void SetLevelNum(int levelNumber){
        int lvlnum = levelNumber + 1;
        levelText.text = lvlnum.ToString();
    }

    public void SetLevel(PlayerStats playerStats){
        this.playerStats = playerStats;

        SetLevelNum(playerStats.GetLevelNumber());
        SetExperienceBarSize(playerStats.GetExperienceNormalized());

        playerStats.OnExprienceChanged += PlayerStats_OnExprienceChanged;
        playerStats.OnExprienceChanged += PlayerStats_OnLevelChanged;
    }

    void PlayerStats_OnExprienceChanged(object sender, System.EventArgs e){
        SetExperienceBarSize(playerStats.GetExperienceNormalized());
    }

    void PlayerStats_OnLevelChanged(object sender, System.EventArgs e){
        SetLevelNum(playerStats.GetLevelNumber());
    }
}
