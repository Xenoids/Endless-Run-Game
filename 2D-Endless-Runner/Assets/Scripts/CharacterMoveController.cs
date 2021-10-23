using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveController : MonoBehaviour
{
    [Header("Movement")]
    public float Accel; // percepatan jalan
    public float maxSpeed;

    [Header("Jump")]
    public float jAccel; // percepatan lompat, jumppower

    private bool isJump;
    private bool isOnGround;

    [Header("Ground Raycast")]
    public float groundRcdistance;
    public LayerMask groundLM;

    [Header("Scoring")]
    public ScoreController score;
    public float scoringRatio;
    private float lastPosX;

    [Header("GameOver")]
    public GameObject gameOverScreen;
    public float fallposY;

    [Header("Camera")]
    public CameraMoveController gamecam;

   


    // Rigid body
    private Rigidbody2D rb;

    // anim
    private Animator anim;
   
   // sound
    private CharacterSoundController sound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sound = GetComponent<CharacterSoundController>();
    }

    private void Update()
    {
        // read input  
        if(Input.GetMouseButton(0))
        {
            // && isjump = false;
            if(isOnGround)
            {
                 isJump = true;
                 sound.PlayJump();
            }
        }

        // Change animation
        anim.SetBool("isOnGround",isOnGround);

        // calculate score
        int distancePassed = Mathf.FloorToInt(transform.position.x - lastPosX);
        int scoreIncrement = Mathf.FloorToInt(distancePassed / scoringRatio);

        if(scoreIncrement > 0)
        {
            score.Increasecurrscore(scoreIncrement);
            lastPosX += distancePassed;
        }

        // game over
        if(transform.position.y < fallposY)
        {
            GO();
        }
    }

    private void GO()
    {
        // set h score
        score.FinishScore();

        // stop cam Movement
        gamecam.enabled = false;

        // show gameOverScreen
        gameOverScreen.SetActive(true);

        // disable this too
        this.enabled = false;
    }

    private void FixedUpdate()
    {
        // raycast ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundRcdistance,groundLM);
        if(hit)
        {
            if(!isOnGround && rb.velocity.y <= 0)
            {
                isOnGround = true;
            }
        }
        else isOnGround = false;

        //Debug.Log(isOnGround);
        
        // hitung velocity vector
        Vector2 velocityV = rb.velocity;

        if(isJump)
        {
            velocityV.y += jAccel;
            isJump = false;
        }

        // Jalan
        velocityV.x = Mathf.Clamp(velocityV.x + Accel * Time.deltaTime,0.0f,maxSpeed);
        
        // rb diupdate dgn velocityv terbaru
        rb.velocity = velocityV;


    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position,transform.position + (Vector3.down * groundRcdistance),Color.white);
    }

   
}
