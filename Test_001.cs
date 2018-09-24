using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_001 : MonoBehaviour
{

    //Ball Variables
    public Transform ball;
    float radius;


    //Paddles
    public Transform rPaddle;
    public Transform lPaddle;
    public float paddleSpeed = 5f;
    float paddleLength;

    //Extents
    float horizontalExtent;
    float verticalExtent;


    //Ball Variables
    float ballSpeed = 10f;
    Vector3 ballDirection;

    // Use this for initialization
    void Start()
    {
        //Random Ball Position Generator
        ResetBall();
        radius = ball.localScale.x / 2;

        //values for the Extents
        verticalExtent = Camera.main.orthographicSize;
        horizontalExtent = verticalExtent * Camera.main.aspect;

        paddleLength = rPaddle.localScale.y / 2;


        
    }

    // Update is called once per frame
    void Update()
    {

        BallMovement();
        CheckBallCollisions();
        PaddleMovement();

        if (Input.GetKey(KeyCode.Space))
        {
            ResetBall();
        }

    }
    void BallMovement()
    {
        ball.transform.position += ballDirection * ballSpeed * Time.deltaTime;
    }

    void CheckBallCollisions()
    {
        CheckBallCollisionWithWall();
        CheckBallCollisionWithRPaddle();
        CheckBallCollisionWithLPaddle();
        CheckBallCollisionsWithRWall();
        CheckBallCollisionsWithLWall();
    }
    //Ball Collisions
    void CheckBallCollisionWithWall()
    {
        if (ball.transform.position.y + radius > verticalExtent)
        {
            ballDirection.y *= -1;
        }
        if (ball.transform.position.y - radius < -verticalExtent)
        {
            ballDirection.y *= -1;
        }
    }
    void CheckBallCollisionWithRPaddle()
    {
        if (ball.position.x + radius > rPaddle.position.x && ball.position.y > rPaddle.position.y - paddleLength && ball.position.y < rPaddle.position.y + paddleLength)
        {
            ballDirection.x *= -1;
        }
    }
    void CheckBallCollisionWithLPaddle()
    {
        if (ball.position.x - radius < lPaddle.position.x && ball.position.y > lPaddle.position.y - paddleLength && ball.position.y < lPaddle.position.y + paddleLength)
        {
            ballDirection.x *= -1;
        }
    }
    void CheckBallCollisionsWithRWall()
    {
        if(ball.position.x+radius>horizontalExtent)
        {
            //Increment the Left player Score
            ResetBall();
        }
    }
    void CheckBallCollisionsWithLWall()
    {
        if (ball.position.x - radius < horizontalExtent)
        {
            //Increment the Right player Score
            ResetBall();
        }
    }
    void ResetBall()
    {
        ball.transform.position = Vector3.zero;
        ballDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;

    }


    //Paddle Movements
    void PaddleMovement()
    {
        Vector3 rPaddleDir;
        Vector3 lPaddleDir;


        rPaddle.Translate(new Vector3(0, Input.GetAxisRaw("Vertical_r"), 0) * paddleSpeed * Time.deltaTime);
        lPaddle.Translate(new Vector3(0, Input.GetAxisRaw("Vertical_l"), 0) * paddleSpeed * Time.deltaTime);

        rPaddleDir = rPaddle.transform.position;
        lPaddleDir = lPaddle.transform.position;

        rPaddleDir.y = Mathf.Clamp(rPaddleDir.y, -verticalExtent + rPaddle.localScale.y / 2, verticalExtent - rPaddle.localScale.y / 2);
        rPaddle.position = rPaddleDir;

        lPaddleDir.y = Mathf.Clamp(lPaddleDir.y, -verticalExtent + rPaddle.localScale.y / 2, verticalExtent - lPaddle.localScale.y / 2);
        lPaddle.position = lPaddleDir;

        /*rPaddle.position += rPaddlePos * paddleSpeed * Time.deltaTime;
        lPaddle.position += lPaddlePos * paddleSpeed * Time.deltaTime;*/

    }

    
}
