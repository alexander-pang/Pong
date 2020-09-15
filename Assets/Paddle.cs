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

    // Start is called before the first frame update
    void Start()
    {
        height = transform.localScale.y;
    }

    public void Init(bool isRightPaddle){

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
        }

        transform.position = pos;

        transform.name = input;

    }
    // Update is called once per frame
    void Update()
    {
        //Now let's move the paddle!

        //Get Axis is a number between -1 to 1 (-1 for down, 1 for up)
        float move = Input.GetAxis(input) * Time.deltaTime * speed;

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
}
