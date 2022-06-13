using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    Block activeBlock;
    Spawn spawn;
    Board board;

    [SerializeField]
    private float dropInterval = 0.25f;

    float nextDropTimer;

    [SerializeField]
    private GameObject gamePanel;

    bool gameOver;
   
     float nextKeyDownTimer, nextKeyRightLeftTimer , nextKeyRotateTimer;

    [SerializeField]
    private float nextDownInterval, nextRightLeftInterval, nextRotateInterval;

    private void Start()
    {
        spawn = GameObject.FindObjectOfType<Spawn>();
        board = GameObject.FindObjectOfType<Board>();



        spawn.transform.position = Rounding.Round(spawn.transform.position);

        nextKeyDownTimer = Time.time + nextDownInterval;
        nextKeyRightLeftTimer = Time.time + nextRightLeftInterval;
        nextKeyRotateTimer = Time.time + nextRotateInterval;


        if (!activeBlock)
        {
        activeBlock = spawn.SpawnBlock();

        }

        if (gamePanel.activeInHierarchy)
        {
        gamePanel.SetActive(false);

        }

    }

    private void Update()
    {

        if (gameOver)
        {
            return;
        }
        KeyInput();



    }

    void KeyInput()
    {
        if (Input.GetKey(KeyCode.D) && Time.time > nextKeyRightLeftTimer || Input.GetKeyDown(KeyCode.D))
        {
            activeBlock.MoveRight();

            nextKeyRightLeftTimer = Time.time + nextRightLeftInterval;

            if (!board.CheckPosition(activeBlock))
            {
                activeBlock.MoveLeft();
            }
        } else if (Input.GetKey(KeyCode.A) && Time.time > nextKeyRightLeftTimer || Input.GetKeyDown(KeyCode.A))
        {
            activeBlock.MoveLeft();

            nextKeyRightLeftTimer = Time.time + nextRightLeftInterval;

            if (!board.CheckPosition(activeBlock))
            {
                activeBlock.MoveRight();
            }
        } else if (Input.GetKey(KeyCode.E) && Time.time > nextKeyRotateTimer)
        {
            activeBlock.RotateRight();

            nextKeyRotateTimer = Time.time + nextRotateInterval;

            if (!board.CheckPosition(activeBlock))
            {
                activeBlock.RotateLeft();
            }
        }  else if (Input.GetKey(KeyCode.S) && Time.time > nextKeyDownTimer || (Time.time > nextDropTimer))
        {
            activeBlock.MoveDown();

            nextKeyDownTimer = Time.time + nextDownInterval;
            nextDropTimer = Time.time + dropInterval;

            if (!board.CheckPosition(activeBlock))
            {
                if (board.OverLimit(activeBlock))
                {
                    GameOver();
                } else
                {
                    BottomBoard();
                }

             
            }
        }
    }

    void BottomBoard()
    {
        activeBlock.MoveUp();
        board.SaveInGrid(activeBlock);
        activeBlock = spawn.SpawnBlock();

        nextKeyDownTimer = Time.time;
        nextKeyRightLeftTimer = Time.time;
        nextKeyRotateTimer = Time.time;

        board.clearAllRows();
    }

    void GameOver()
    {

        activeBlock.MoveUp();

        if (!gamePanel.activeInHierarchy)
        {
            gamePanel.SetActive(true);

        }

        gameOver = true;
    }


    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
