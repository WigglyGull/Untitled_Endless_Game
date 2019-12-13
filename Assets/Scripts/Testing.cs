using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour{
    [SerializeField] Ui ui;

    void Awake() {
        PlayerStats playerStats = new PlayerStats();
        ui.SetLevel(playerStats);

        ExpBarAnimator expBar = new ExpBarAnimator(playerStats);
    }
}
