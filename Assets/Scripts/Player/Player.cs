using UnityEngine;

public class Player : MonoBehaviour{
    Vector2 velocity;
    Vector2 box;
    public BoxCollider2D balanceBoxs;
    Vector2 pos;

    public GameObject breath, jumpSmoke;

    public Transform breathSpawn;
    public Transform[] jumpSmokeSpawn;

    public LayerMask groundLayerMask;
    PlayerStats playerStats;

    public Animator anim;
    Rigidbody2D rb;
    BoxCollider2D box2D;

    public float speed;
    public float jumpVelocity;
    public float fallVelocity;
    public float lowJump;
    float normalSpeed;
    float moveInput;
    float directMoveInput;
    float breathTime; 
    float coyoteTime;
    float fallTime;

    Quaternion rotation;
    public ParticleSystem ps;
    public CameraShake cs;
    public TextPop tp;

    [HideInInspector] public bool walk, idle, jump, fall, crouch, landed, earlyJump, grounded, sit, balance;

    bool closeToChair;
    bool left;
    
    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        box2D = GetComponent<BoxCollider2D>();
    }

    void Start(){
        normalSpeed = speed;
        breathTime = Random.Range(1.2f, 1.5f);  
        rotation = transform.rotation;
    }

    void Update(){
        HandleAnimations();
        grounded = IsGrounded();

        if(jump || fall){
            walk = false;
        }

        if(!IsBalance()){
            balance = true;
        } else{
            balance = false;
        }

        if(sit){
            speed = 0;
            if(Input.GetButtonDown("MoveButtons") || Input.GetButtonDown("Jump")){
                sit = false;
                speed = normalSpeed;
            }
        }

        if(Input.GetButtonDown("MoveButtons") && !jump && !crouch){
            if(moveInput < 0 && left) return;
            if(moveInput > 0 && !left) return;
            anim.SetTrigger("Turn");
        }

        RotationMess();

        if(rb.velocity.y == 0){
            jump = false;
        }

        if(idle && !crouch){
            if(breathTime <= 0){
                GameObject _breath = Instantiate(breath, breathSpawn.position, transform.localRotation);
                _breath.transform.parent = gameObject.transform;
                breathTime = Random.Range(1.2f, 1.5f);
            }else{
                breathTime -= Time.deltaTime;
            }
        }else{
            breathTime = Random.Range(1.2f, 1.5f);
        }
        
        if(grounded && !landed && fall){
            Land();
        }

        if(earlyJump) EarlyJump();

        HandleSitting();

        anim.SetBool("Crouch", crouch);
        anim.SetBool("Walk", walk);
        anim.SetBool("Idle", idle);
        anim.SetBool("JumpUp", jump);
        anim.SetBool("JumpDown", fall);
        anim.SetBool("Sit", sit);
    }

    public void SetPlayerStats(PlayerStats playerStats){
        this.playerStats = playerStats;

        playerStats.OnLevelChanged += PlayerStats_OnLevelChanged;
    }

    void PlayerStats_OnLevelChanged(object sender, System.EventArgs e){
        if(IsGrounded()){
            anim.SetTrigger("LevelUp");
            cs.Shake(0.3f, 0.02f);
        }else{
            cs.Shake(0.3f, 0.05f);
        }
        ps.Emit(100);
        
        tp.Pop();
    }

    void FixedUpdate(){
        moveInput = Input.GetAxis("Horizontal");
        directMoveInput = Input.GetAxisRaw("Horizontal");
        velocity.Set(moveInput * (speed * Time.fixedDeltaTime), rb.velocity.y);
        rb.velocity = velocity;

        if(!grounded){
            crouch = false;
            coyoteTime-=Time.fixedDeltaTime;
            if(0 < coyoteTime){
                if(Input.GetButtonDown("Jump")){
                    if(!fall) return;
                    if(jump) return;
                    fall = false;
                    jump = false;
                    Jump();
                }
            }
        }

        if(fall){
            rb.gravityScale = fallVelocity;
            fallTime += Time.fixedDeltaTime;
            if(Input.GetButtonDown("Jump")) earlyJump = true;
        }else if(rb.velocity.y > 0 && Input.GetButton("Jump")){
            rb.gravityScale = lowJump;
        }else{
            rb.gravityScale = 1;
            if(sit) rb.gravityScale = 0;
        }
    }

    void EarlyJump(){
        if(0 < coyoteTime) earlyJump = false;;
        if(grounded){
            Jump();
            earlyJump = false;
        }
    }

    void Jump(){
        rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
        fallTime = 0;
        landed = false;
        jump = true;
    }

    void RotationMess(){
        if(directMoveInput < 0) {
            rotation.y = 180;
            transform.localRotation = rotation;
            jumpSmokeSpawn[0].localRotation = rotation;
            rotation.y = 0;
            jumpSmokeSpawn[1].localRotation = rotation;
            left = true;
        }else if(directMoveInput > 0){
            rotation.y = 0;
            rotation.x = 0;
            transform.localRotation = rotation;
            jumpSmokeSpawn[0].localRotation = rotation;
            rotation.y = 180;
            jumpSmokeSpawn[1].localRotation = rotation;
            left = false;
        }
    }

    void Land(){
        fall = false;
        jump = false;
        landed = true;
        if(!crouch){
            if(fallTime > 0.3f){
                anim.SetTrigger("Squish");
                Instantiate(jumpSmoke, jumpSmokeSpawn[0].position, jumpSmokeSpawn[0].localRotation);
                Instantiate(jumpSmoke, jumpSmokeSpawn[1].position, jumpSmokeSpawn[1].localRotation);
            }
        }
        coyoteTime = 0.2f;
    }

    public bool IsGrounded(){
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(box2D.bounds.center, box2D.bounds.size, 0f, Vector2.down, 0.05f, groundLayerMask);
        return raycastHit2d.collider != null;
    }

    public bool IsBalance(){
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(balanceBoxs.bounds.center, balanceBoxs.bounds.size, 0f, Vector2.down, 0.05f, groundLayerMask);
        return raycastHit2d.collider != null;
    }

    void HandleAnimations(){
        idle = true;

        if(moveInput > 0 || moveInput < 0){     
            walk = true;
            idle = false;
        }else{
            walk = false;
        }

        if(Input.GetButtonDown("Jump")){
            if(crouch) return;
            if(!grounded) return;
            Jump();
        }

        if(rb.velocity.y < 0 && !grounded){
            jump = false;
            landed = false;
            if(!fall) fallTime = 0;
            fall = true;
        }

        if(balance && idle){
            anim.SetBool("Balance", true);
            idle = false;
        }
        if(!balance) anim.SetBool("Balance", false);
    }

    void OnTriggerStay2D(Collider2D other)  {
        if(other.tag == "Platform" && IsGrounded() && !jump && !fall){
            crouch = true;
        }
        if(other.tag == "Bench"){
            closeToChair = true;
        }
    }

    void HandleSitting(){
        if(closeToChair){
            if(Input.GetButtonDown("Interact") && !sit){
                sit = true;
                Vector2 pos = transform.position;
                pos.y += 0.05f;
                transform.position = pos;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Platform"){
            crouch = false;
        }   
        if(other.tag == "Bench"){
            closeToChair = false;
        }   
    }
}