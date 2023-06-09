using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 30;
    private PlayerController playerControllerScript;
    private float leftBound = -15;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerControllerScript.gameOver == false)
       {
          transform.Translate(Vector3.left * Time.deltaTime * speed);
       }

       if (Input.GetKey(KeyCode.LeftControl))
        {
            speed = 30;
        }
        else
        {
            speed = 20;
        }


        
       if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
       {
            Destroy(gameObject);
             playerControllerScript.score += 1;
             Debug.Log("Score " + playerControllerScript.score);
       }

    }
}
