using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats{
    public event EventHandler OnExprienceChanged;
    public event EventHandler OnLevelChanged;

    int level;
    int experience;
    int nextLevel;

    public PlayerStats(){
        level = 0;
        experience = 0;
        nextLevel = 100;
    }

    public void AddExperience(int amount){
        experience += amount;
        while (experience >= nextLevel){
            level++;
            experience -= nextLevel;
            if(OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
        }
        if(OnExprienceChanged != null) OnExprienceChanged(this, EventArgs.Empty);
    }

    public int GetLevelNumber(){
        return level;
    }

    public int GetExperience(){
        return experience;
    }

    public float GetExperienceNormalized(){
        return (float)experience / nextLevel;
    }

    public int GetExperienceToNextLevel(){
        return nextLevel;
    }
}
