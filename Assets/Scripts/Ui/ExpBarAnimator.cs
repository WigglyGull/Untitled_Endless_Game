using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class ExpBarAnimator {
    PlayerStats playerStats;
    bool isAnimating;

    int level;
    int experience;
    int nextLevel;

    public ExpBarAnimator(PlayerStats playerStats){
        SetPlayerStats(playerStats);

        FunctionUpdater.Create(() => Update());
    }

    public void SetPlayerStats(PlayerStats playerStats){
        this.playerStats = playerStats;

        level = playerStats.GetLevelNumber();
        experience = playerStats.GetExperience();
        nextLevel = playerStats.GetExperienceToNextLevel();

        playerStats.OnExprienceChanged += PlayerStats_OnExperienceChanged;
        playerStats.OnLevelChanged += PlayerStats_OnLevelChanged;
    }

    void PlayerStats_OnLevelChanged(object sender, System.EventArgs e){
        isAnimating = true;
    }

    void PlayerStats_OnExperienceChanged(object sender, System.EventArgs e){
        isAnimating = true;
    }

    void Update(){
        if(isAnimating){
            if(level < playerStats.GetLevelNumber()){
                AddExperience();
            }else{
                if(experience < playerStats.GetExperience()){
                    AddExperience();
                }else{
                    isAnimating = false;
                }
            }
        }
        Debug.Log(experience);
    }

    void AddExperience(){
        experience++;
        if(experience >= nextLevel){
            level++;
            experience = 0;
        }
    }
}
