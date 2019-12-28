using UnityEngine;

public class NewPlayer : MonoBehaviour{

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    public float wallStickTime = .25f;
    float timeToUnstick;


    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
    public float wallSlideLimit = 3;
	float accelerationTimeAirborne = .1f;
	float accelerationTimeGrounded = .05f;
	float moveSpeed = 2f;

	float gravity;
	float maxJumpVelocity;
    float minJumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

	Controller2d controller;

    Vector2 directionalInput;
    bool wallSliding;
    int wallDirX;


	void Start() {
		controller = GetComponent<Controller2d>();

		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
	}

	void Update() {
        SetVelocity();
        HandleWallSliding();
        
        if(controller.collisions.faceDirection == 1){
            transform.localScale = new Vector2(1, transform.localScale.y);
        }else if(controller.collisions.faceDirection == -1){
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }
		
		controller.Move (velocity * Time.deltaTime);
        if (controller.collisions.above || controller.collisions.below) velocity.y = 0;
	}

    public void SetInput(Vector2 input){
        directionalInput = input;
    }

    public void JumpInputDown(){
        if(wallSliding){
            if(wallDirX == directionalInput.x){
                velocity.x = -wallDirX * wallJumpClimb.x;
                velocity.y = wallJumpClimb.y;
            }else if(directionalInput.x == 0){
                velocity.x = -wallDirX * wallJumpOff.x;
                velocity.y = wallJumpOff.y;
            }else{
                velocity.x = -wallDirX * wallLeap.x;
                velocity.y = wallLeap.y;
            }
        }
        if(controller.collisions.below) velocity.y = maxJumpVelocity;
    }

    public void JumpInputUp(){
        if(velocity.y < minJumpVelocity) return;
        velocity.y = minJumpVelocity;
    }

    void HandleWallSliding(){
        wallDirX = (controller.collisions.left)?-1:1;
        wallSliding = false;
        if ((controller.collisions.right || controller.collisions.left) && !controller.collisions.below && velocity.y <= 0){
            wallSliding = true;

            if(velocity.y < -wallSlideLimit) velocity.y = -wallSlideLimit;

            if(timeToUnstick > 0){
                velocityXSmoothing = 0;
                velocity.x = 0;

                if(directionalInput.x != wallDirX && directionalInput.x != 0){
                    timeToUnstick -= Time.deltaTime;
                }else{
                    timeToUnstick = wallStickTime;
                }
            }else{
                timeToUnstick = wallStickTime;
            }
        }
    }

    void SetVelocity(){
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
    }
}