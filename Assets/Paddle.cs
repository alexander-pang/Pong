using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    float speed;
    
    
    float height;

    string input;
    public bool isRight;
    [SerializeField]
    public bool isBot;
    private Ball ball;
    private Vector2 forward;

    // Start is called before the first frame update
    void Start()
    {
        height = transform.localScale.y;
        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();
        if (!isRight)
        {
            forward = Vector2.right;
          
        }
        else
        {
            forward = Vector2.left;
        }
    }

    public void Init(bool isRightPaddle, bool bot){

        isRight = isRightPaddle;

        Vector2 pos = Vector2.zero;

        if(isRightPaddle){
            //right of screen
            pos = new Vector2(GameManager.topRight.x,0);
            pos -= Vector2.right * transform.localScale.x; //Move a bit to the left

            input = "PaddleRight";

        }else{
            // left of screen

            pos = new Vector2(GameManager.bottomLeft.x,0);
            pos += Vector2.right * transform.localScale.x; //Move a bit to the right

            input = "PaddleLeft";
            isBot = bot;
        }

        transform.position = pos;

        transform.name = input;

    }
    // Update is called once per frame
    void Update()
    {
        //Now let's move the paddle!

        //Get Axis is a number between -1 to 1 (-1 for down, 1 for up)
        float move = GetY();

        //Restrict paddle movement
        //stop paddle from moving too low
        if(transform.position.y < GameManager.bottomLeft.y + height / 2 && move < 0){
            move = 0;
        }

        if(transform.position.y > GameManager.topRight.y - height / 2 && move > 0){
            move = 0;
        }

        transform.Translate (move * Vector2.up);
    }

    private float GetY()
    {
        float pos = transform.position.y;
        if (isBot)
        {
            if (ApproachingBall())
            {
                pos = Mathf.MoveTowards(transform.position.y, ball.transform.position.y, speed);
            }
        }
        else
        {
            pos = Input.GetAxis(input) * Time.deltaTime * speed;
        }
        return pos;
    }

    private bool ApproachingBall()
    {
        float dotProduct = Vector2.Dot(ball.velocity, forward);
        return dotProduct < 0f;
    }
}
