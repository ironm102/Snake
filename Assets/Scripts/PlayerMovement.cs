using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float moveRate = 0.25f;
    float timerHead;

    int appleCount = 0;

    SnakeList snakeList;

    public GameObject snakeTail;
    public GameObject snakeTailInstance;

    public GameObject gameManagerObj;
    public GameManager gameManagerScript;

    bool isMovable = true;

    Vector3 tempPos;

    private Vector3 gridMoveDirection;
    Vector3 desiredDirection;
    private Vector3 gridPosition;


    //gets the GameManager script from the gameManager object
    //Sets the start position for the snake
    //gets the snakeList script that handles the linklist functions
    private void Awake()
    {
        gameManagerScript = gameManagerObj.GetComponent<GameManager>();

        gridPosition = new Vector3(10, 10);

        snakeList = new SnakeList();
    }


    //initiates methods
    private void Update()
    {
        HandleInput();
        Movement();
        TailGrow();
        gameManagerScript.RestartGame();

        
    }

    //handles collision triggers for when you pick up apples and whne you crash and end the game
    //If you lose, it changes the state of isMovable to false, so you can't move the sanke after death
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Apple")
        {
            appleCount++;
        }

        if (collision.gameObject.tag == "Walls")
        {
            gameManagerScript.GameOver();
            isMovable = false;
        }

        if (collision.gameObject.tag == "Tail")
        {
            gameManagerScript.GameOver();
            isMovable = false;
        }
    }

    //handles the input keys.
    //checks if you can move with the isMovable bool
    //changes the vector for movedirection depending on button press
    //i.e. KeyCode.UpArrow changes to vector.up,which is (0,1) on gridMoveDirection
    //the !=x check is to not allow the snake move the same way it came from
    //Also changes the rotation of the head node, so the texture rotates when changing direction


        //Not used, only to understand Methods
    void InputKey(string button, Vector3 direction)
    {
        if (Input.GetButtonDown(button))
        {
            desiredDirection = direction;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
            gameManagerScript.StartGame();
        }
    }

    void HandleInput()
    {
        if (isMovable == true)
        {
            if (gridMoveDirection.y != -1)
            {
                if (Input.GetButtonDown("Up"))
                {
                    desiredDirection = Vector3.up;
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
                    gameManagerScript.StartGame();
                }
            }
            if (gridMoveDirection.y != 1)
            {
                if (Input.GetButtonDown("Down"))
                {
                    desiredDirection = Vector3.down;
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, -90);
                    gameManagerScript.StartGame();
                }
            }

            if (gridMoveDirection.x != 1)
            {
                if (Input.GetButtonDown("Left"))
                {
                    desiredDirection = Vector3.left;
                    gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                    gameManagerScript.StartGame();
                }
            }

            if (gridMoveDirection.x != -1)
            {
                if (Input.GetButtonDown("Right"))
                {
                    desiredDirection = Vector3.right;
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    gameManagerScript.StartGame();
                }
            }
        }
    }

    //handles the movement of the snake

    //sets the timer to tick up every frame
    //when the timer hits the maxTime,i.e moveRate, resets the timer and moves the head
    //Sets the direction to the desiredDirection first. that way, when the game starts, the snake has vector.zero and doesnt move
    //It's also a fix to not be able to turn twice quickly and move into the direction you came from. 
    //otherwise if you press quickly enough, you change the direction to hit yourself
    //runs the tailMovement() method, which moves the tail
    void Movement()
    {
        timerHead += Time.deltaTime;
        

        if (timerHead >= moveRate)
        {
            timerHead = 0;

            gridMoveDirection = desiredDirection;

            gridPosition += gridMoveDirection;

            tempPos = transform.position;
            transform.position = new Vector3(gridPosition.x, gridPosition.y);

            TailMovement();
        }
    }

    //Grows the Tail when picking up an apple

    //on collision with an apple, sets the appleCOunt to 1, therefore instantiates a new tail, which is then put into the linkedList
    //changes the speed of snake after picking up an apple by mutlipying it with the difficultyMultiplier
    //ends by reseting the appleCount to 0. it basically works as a bool
    void TailGrow()
    {
        if (appleCount > 0)
        {
            snakeTailInstance = GameObject.Instantiate(snakeTail, new Vector3(tempPos.x, tempPos.y), new Quaternion());
            snakeList.AddToStart(snakeTailInstance);


            moveRate *= gameManagerScript.difficultyMultiplier;
            appleCount--;
        }
    }

    //handles the tail Movement

    //Starts by checking if the linkedList has any object in it. if it does, runs the script.
    //this way prevents getting errors when script is trying to move a non-existent object
    //Adds a new variable Node which has the information from the last object in the list
    //proceeds to delete said object, to prevent the lsit from growing when we re-add it in the first position of the list
    //moves the position of the last tail object to have the same position as the last positio of the head.
    //Thus spawning behind the head
    //Then adds the new information to the front of the list.
    //And then it repeats, moving the last pieces to the front position

    void TailMovement()
    {

        if (snakeList.Count()>0)
        {
            Node temp = snakeList.GetLast(snakeList.headNode);

            snakeList.Remove(snakeList.GetLast(snakeList.headNode).data);
            temp.data.transform.position = tempPos;

            snakeList.AddToStart(temp.data);
        }
    }
}
