using UnityEngine;

public class NewPlayerInput : MonoBehaviour{
    NewPlayer player;

    void Start(){
        player = GetComponent<NewPlayer>();
    }

    void Update(){
        Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        player.SetInput(directionalInput);
        if(Input.GetButtonDown("Jump")) player.JumpInputDown();
        if(Input.GetButtonUp("Jump")) player.JumpInputUp();
    }
}
