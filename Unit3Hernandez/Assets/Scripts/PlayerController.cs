using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public float jumpForce = 10;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;
    public bool doubleJump = true;
    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    
        
    }

    // Update is called once per frame
    void Update()
     {
        if(Input.GetKeyDown(KeyCode.Space) && (isOnGround || doubleJump) && !gameOver){
            doubleJump = isOnGround ? true : false;
            float updatedJumpForce = isOnGround ? jumpForce : jumpForce/2;

            playerRb.AddForce(Vector3.up * updatedJumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 0.1f);

             if (Input.GetKey(KeyCode.LeftControl))
        {
            playerAnim.speed = 2.5f;
        }
        else
        {
            playerAnim.speed = 1.5f;
        }

        }
    }

    private void OnCollisionEnter(Collision collision)
  {
     if(collision.gameObject.CompareTag("Ground"))
    {
        isOnGround = true;
        dirtParticle.Play();
    } else if(collision.gameObject.CompareTag("Obstacle"))
    {
        Debug.Log("Game Over");
        gameOver= true;
        playerAnim.SetBool("Death_b", true);
        playerAnim.SetInteger("DeathType_int", 1);
        explosionParticle.Play();
        dirtParticle.Stop();
        playerAudio.PlayOneShot(crashSound, 0.8f);
    } 
  }
}
