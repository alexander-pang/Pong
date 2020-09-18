using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ball ball;
    public Paddle paddle;
    public Restart restart;
    public int winningScore = 2;

    public enum State
    {
        StartGame,
        Paused,
        EndGame,
    }

    public State state = State.StartGame;

    public static Vector2 bottomLeft;
    public static Vector2 topRight;

    private int leftScore;
    private int rightScore;
    private Display display;
    private GameObject displayCanvas;

    // Start is called before the first frame update
    void Start()
    {
        // Convert screen's pixel coordinate into game's coordinate (in this case 0,0)
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0,0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));
        Instantiate (ball);
        Instantiate(restart);
        Paddle paddle1 = Instantiate (paddle) as Paddle;
        Paddle paddle2 = Instantiate (paddle) as Paddle;
        paddle1.Init(true,false); //right paddle
        paddle2.Init(false,true); //left paddle

        displayCanvas = GameObject.Find("Display_Canvas");
        display = displayCanvas.GetComponent<Display>();

        display.leftWin.enabled = false;
        display.rightWin.enabled = false;
        display.pongTitle.enabled = false;
        

    }

    // Update is called once per fra

    public void LeftPoint()
    {
        leftScore++;
        display.leftPlayerScore.text = leftScore.ToString();
        validateWin();
        NewBall();
    }
    public void RightPoint()
    {
        rightScore++;
        display.rightPlayerScore.text = rightScore.ToString();
        validateWin();
        NewBall();
    }

    private void NewBall()
    {
        if (state == State.StartGame)
        {
            GameObject.Destroy(ball.gameObject);
            Instantiate(ball);
        }
    }

    void validateWin()
    {
        if(rightScore >= winningScore)
        {
            RightWins();
        }
        else if (leftScore >= winningScore)
        {
            LeftWins();
        }
    }
    private void RightWins()
    {
        display.rightWin.enabled = true;
        EndGame();
    }
    private void LeftWins()
    {
        display.leftWin.enabled = true;
        EndGame();
    }
    private void EndGame()
    {
        GameObject.Destroy(ball.gameObject);
        Time.timeScale = 0;
        display.pongTitle.enabled = true;
        state = State.EndGame;
    }
}

