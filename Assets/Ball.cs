using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    float speed;

    float radius;
    Vector2 direction;
    public Vector2 velocity { get; private set;}

    private GameManager gameManager;
    private bool pointFlag;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        direction = Vector2.one.normalized; //direction is (1,1) normalized
        radius = transform.localScale.x / 2; //half the width
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate (direction * speed * Time.deltaTime);

        velocity = direction * speed;

        // Bounce off top and bottom

        if(transform.position.y < GameManager.bottomLeft.y + radius && direction.y < 0){
            direction.y = -direction.y;
        }
        if(transform.position.y > GameManager.topRight.y - radius && direction.y > 0){
            direction.y = -direction.y;
        }
        //ame over
        if(transform.position.x < GameManager.bottomLeft.x + radius && direction.x < 0){
            Debug.Log("Right player wins");

            //for now, freeze time
            //Time.timeScale = 0;
            //enabled = false; // stop updating script
            if (!pointFlag)
            {
                pointFlag = true;
                gameManager.RightPoint();
            }
            
        }

        if(transform.position.x > GameManager.topRight.x - radius && direction.x > 0){
            Debug.Log("Left player wins");
            //Time.timeScale = 0;
            //enabled = false; // stop updating script
            if (!pointFlag)
            {
                pointFlag = true;
                gameManager.LeftPoint();
            }
            
        }
        
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Paddle"){
            bool isRight = other.GetComponent<Paddle> ().isRight;

            FindObjectOfType<AudioManager>().Play("paddleHit");

            //if hitting right paddle and moving right, flip direction

            if(isRight == true && direction.x > 0){
                direction.x = -direction.x;
            }
            if(isRight == false && direction.x < 0){
                direction.x = -direction.x;
            }

        }
    }
}
