using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController : MonoBehaviour
{
    Vector3 playerMovment;

    public static Scoring scoringRef;
    private GameObject MenuManagerObj;

    //private const float SLOPESPEED = 2.5f;
    //private const float AIRSPEED = 3.25f;

    //private Vector3 StartGravity = new Vector3(0.0f, -9.8f, 0.0f);
    //private Vector3 SlopeGravity = new Vector3(0.0f, -100f, 0.0f);

    public float speed;
    private float moveSpeed;
    public float rotateSpeed;
    public float groundCheckLength;

    public float jumpTimeSpan = 0.3f;
    private float jumptime;
    private float jumpAniTimer;

    public float jumpForce;
    float doublejumpForce;
    public float fallForce;

    private float pickupRespawnTimer;
    private float fuelRegenTimer;

    private float deathDelay;
    float gotHitTimer;

    private string _type;

    //float maxSlopeHeight = 70f;
    //float minSlopeHeight = 90f;

    public LayerMask groundLayer;

    //public GameObject SlopeDetect;
    public GameObject camPivot;
    private GameObject fuelCan;

    Animator ani;
    Rigidbody rb;
    CapsuleCollider col;
  
    public bool canDoubleJump;
    public bool dead;
    public bool hitMe;
    bool pickupCountDown;
    bool ableToMoveBool;
    bool respawning;
    public bool doubleJumped;


    void Start()
    {
        col = GetComponent<CapsuleCollider>();
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        MenuManagerObj = GameObject.Find("MenuManager");
        scoringRef = MenuManagerObj.GetComponent<Scoring>();

        deathDelay = 2;
        pickupRespawnTimer = 5;
        fuelRegenTimer = 35;
        gotHitTimer = 1;
        jumpAniTimer = 0.5f;

        doublejumpForce = jumpForce / 1.25f;

        ableToMoveBool = true;
        hitMe = false;
    }

    void FixedUpdate()
    {
        //RaycastHit hitSlopeUp;

        //RaycastHit hitSlopeDown;

        ////Finding upwards slopes 
        //if (Physics.Raycast(gameObject.transform.position, Vector3.forward, out hitSlopeUp, rayLength))
        //{
        //    if (hitSlopeUp.collider.gameObject.layer == 0)
        //    {
        //        float slopeAngleUp = Vector3.Angle(hitSlopeUp.normal, Vector3.up);

        //        if(slopeAngleUp >= maxSlopeHeight)
        //        {
        //            speed = SLOPESPEED;
        //            Physics.gravity = SlopeGravity;
        //        }
        //        else
        //        {
        //            speed = STARTSPEED;
        //            Physics.gravity = StartGravity;
        //        }

        //    }
        //}

        //Finding downwards slopes
        //if (Physics.Raycast(SlopeDetect.transform.position, Vector3.forward, out hitSlopeDown, rayLength))
        //{
        //    if (hitSlopeDown.collider.gameObject.layer == 0)
        //    {
        //        float slopeAngleDown = Vector3.Angle(hitSlopeDown.normal, Vector3.down);

        //        if (slopeAngleDown <= minSlopeHeight)
        //        {
        //            speed = SLOPESPEED;
        //            Physics.gravity = SlopeGravity;
        //        }
        //        else
        //        {
        //            speed = STARTSPEED;
        //            Physics.gravity = StartGravity;
        //        }
        //    }
        //}
        
        if (!dead)
        {
            if (hitMe)
            {
                ani.SetTrigger("Hit");
                ableToMoveBool = false;
                hitMeSet();
            }
            else
            {                 
                if (ableToMoveBool)
                {
                    movementManager();
                }
            }
        }
    }

    public void ableToMove()
    {       
        ableToMoveBool = true;   
    }

    public void hitMeSet()
    {
        hitMe = false;
        ableToMoveBool = true;
    }

    void Update()
    {
        if (!dead)
        {
            if (!hitMe)
            {
                AirManager();

                if (doubleJumped)
                {
                    jumpAniTimer -= Time.deltaTime;
                    if (jumpAniTimer < 0)
                    {
                        jumpAniSet();
                    }
                }
            }
        }
        else
        {
            //RESPAWN TIMER
            deathDelay -= Time.deltaTime;
            if(deathDelay < 0)
            {
                respawn();
            }
        }

        if (pickupCountDown)
        {
            pickupRespawnTimer -= Time.deltaTime;
            if (pickupRespawnTimer < 0)
            {
                respawnFuel();
            }
        }

        if(scoringRef.FuelCount < 6)
        {
            fuelRegenTimer -= Time.deltaTime;
            if (fuelRegenTimer < 0)
            {
                scoringRef.FuelRegen();
                fuelRegenTimer = 35;
            }
        }


    }

    void OnTriggerEnter(Collider other)
    {

        _type = other.gameObject.tag;

        switch (_type)
        {
        //colliding with collectibles
            case "Sapphire":
                other.gameObject.SetActive(false);
                scoringRef.Sapphirecount++;
                scoringRef.SetCountText();
                scoringRef.playSoundEffect();
                break;
            case "Ruby":
                other.gameObject.SetActive(false);
                scoringRef.Rubycount++;
                scoringRef.SetCountText();
                scoringRef.playSoundEffect();
                break;
            case "Emerald":
                other.gameObject.SetActive(false);
                scoringRef.Emeraldcount++;
                scoringRef.SetCountText();
                scoringRef.playSoundEffect();
                break;
            case "Ameythyst":
                other.gameObject.SetActive(false);
                scoringRef.Ameythystcount++;
                scoringRef.SetCountText();
                scoringRef.playSoundEffect();
                break;
            case "Diamond":
                other.gameObject.SetActive(false);
                scoringRef.Diamondcount++;
                scoringRef.SetCountText();
                scoringRef.playSoundEffect();
                break;
            /*case "Gold":
                other.gameObject.SetActive(false);
                scoringRef.Goldcount++;
                scoringRef.SetCountText();
                scoringRef.playSoundEffect();
                break;
            case "Quartz":
                other.gameObject.SetActive(false);
                scoringRef.Quartzcount++;
                scoringRef.SetCountText();
                scoringRef.playSoundEffect();
                break;*/
        //colliding with health/fuel pick-up
            case "Steak":
                if (!scoringRef.Health3.enabled)
                {
                    other.gameObject.SetActive(false);
                    scoringRef.HealthCount++;
                    scoringRef.SetCountText();
                }

                //check to see which health display needs to be re-enabled
                if (scoringRef.Health2.enabled == false)
                {
                    scoringRef.Health2.enabled = true;
                }
                else
                if (scoringRef.Health3.enabled == false)
                {
                    scoringRef.Health3.enabled = true;
                }
                break;
            case "Fuel":
                fuelCan = other.gameObject;
                other.gameObject.GetComponent<MeshRenderer>().enabled = false;
                scoringRef.FuelCount += 6;
                scoringRef.FuelUpdate();
                pickupCountDown = true;
                break;
        //if player hit the kill box
            case "KillBox":
                scoringRef.HealthCount = 0;
                scoringRef.Health1.enabled = false;
                scoringRef.Health2.enabled = false;
                scoringRef.Health3.enabled = false;
                scoringRef.KillPlayer();
                if (scoringRef.LivesCount > 0)
                {
                    scoringRef.ResetHealth();
                }
                break;
        }






    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Saw")
        {
            hitMe = true;
            float force = 20;
            Vector3 dir = c.contacts[0].point - transform.position;
            dir = -dir.normalized;

            rb.AddForce(dir * force, ForceMode.Impulse);            

            scoringRef.playerTakeDamage(); // Removes health

            //if the player has no more health
            if ((scoringRef.HealthCount == 0) && (scoringRef.LivesCount > 0))
            {
                Dead();
            }
            
            scoringRef.KillPlayer(); //Checks if player is dead or not
        }
       
    }

    void respawnFuel()
    {
        fuelCan.GetComponent<MeshRenderer>().enabled = true;
        pickupCountDown = false;
        pickupRespawnTimer = 5;
    }

    void movementManager()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        //MOVEMENT HAPPENING  
        playerMovment = (camPivot.transform.forward * ver * speed * Time.deltaTime) + (camPivot.transform.right * hor * speed * Time.deltaTime);
        Vector3.Normalize(playerMovment * Time.deltaTime);
        transform.Translate(playerMovment, Space.World);

        //Rotate player when moving 
        if (hor != 0 || ver != 0)
        {
            transform.rotation = Quaternion.Euler(0.0f, gameObject.transform.rotation.eulerAngles.y, 0.0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(playerMovment.x, 0f, playerMovment.z));
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            moveSpeed = 1;

        }
        //Not Moving, for the animation trigger
        else
        {
            moveSpeed = 0;
        }        

        if (IsGround())
        {
            //---------------------------------------------------Animation sets
            ani.SetBool("isGrounded", true);

            ani.SetFloat("moveSpeed", moveSpeed);
        }
        else
        {
            //  IN AIR
            ani.SetBool("isGrounded", false);
            //speed = AIRSPEED;
        }
    }

    void AirManager()
    {

        if ((IsGround()) && (scoringRef.FuelCount > 0))
        {
            canDoubleJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGround())
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            else
            {
                if (canDoubleJump)
                {
                    rb.AddForce(Vector3.up * doublejumpForce, ForceMode.Impulse);
                    canDoubleJump = false;
                    doubleJumped = true;
                    ani.SetBool("DoubleJump", true);
                    scoringRef.FuelCount--;
                    scoringRef.FuelUpdate();
                }
            }
        }
   
    }

    public void jumpAniSet()
    {
        doubleJumped = false;
        ani.SetBool("DoubleJump", false);
        jumpAniTimer = 0.5f;
    }

    void Dead()
    {
        ani.SetTrigger("Dead");
        dead = true;
    }

    void respawn()
    {
        dead = false;
        scoringRef.ResetHealth();
        gameObject.transform.localPosition = scoringRef.Respawn.transform.localPosition;
        deathDelay = 2;
    }

    public bool IsGround()
    {
        RaycastHit hit;
        return Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, groundCheckLength, groundLayer);
        
    }
   
}

   
 





