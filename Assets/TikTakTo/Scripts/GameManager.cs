using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] UIManager UIManager = null;

    char[] board = new char[9];
    bool playerTurn = false;
    bool gameOver = false;
    int turns = 0;

    public static Action OnPlayerClick { get; internal set; }
    public static Action OnPlayerLose { get; internal set; }

    void Start()
    {
        InitBoard();
        ShowBoard();
        UIManager.SendUIMessage("Use Number Keys");
    }


    void InitBoard()
    {
        for (int i = 0; i < board.Length; i++)
        {
            board[i] = '_';
        }
    }

    void ShowBoard()
    {
        string boardStr = "";
        for (int i = 0; i < board.Length; i+=3)
        {
            boardStr += board[i] + " " + 
                board[i + 1] + " " + board[i + 2] + "\n";
        }
        Debug.Log(boardStr);
        UIManager?.ShowBoard(board);
    }

    void Update()
    {
        if (gameOver)
        {
            return;
        }

        if (playerTurn)
        {
            for (int i = 0; i < board.Length; i++)
            {
                HandleKeyPress(i+1);                
            }
        } 
        else //computer turn
        {
            if (turns <3)
            {
                FillFreeSpot();
            }
            else
            {
                BeAmazing();
            }
        }
    }

    private void BeAmazing()
    {
        int bestscore = -100;
        int bestmove = 0;
        for (int spot = 0; spot < board.Length; spot++)
        {
            if (board[spot] == '_')
            {
                board[spot] = 'O';
                int score = MinMax(board, 0, false);
                board[spot] = '_';
                if (score > bestscore)
                {
                    bestscore = score;
                    bestmove = spot;
                    Debug.Log($"bestscore: {bestscore}");
                }
            }
        }
        board[bestmove] = 'O';
        ShowBoard();
        if (CheckWinner(board) == Participants.AI)
        {
            UIManager.SendUIMessage("Computer Won!!!!");
            gameOver = true;
        }
        if (isFullBoard())
        {
            UIManager.SendUIMessage("Game Over, no one Won...");
            gameOver = true;
        }
        turns++;
        playerTurn = true;
    }

    private int MinMax(char[] newboard, int depth, bool isMax)
    {
        //Debug.Log($"MinMax in {depth} depth ({isMax}) - ");
        var result = CheckWinner(newboard);
        if (result != Participants.None)
        {
            if (result == Participants.Player)
            {
                return -1;
            }
            else if (result == Participants.AI)
            {
                return 1;
            }
            return 0;
        }

        if (isMax)
        {
            var bestScore = -100;
            for (var i = 0; i < newboard.Length; i++)
            {
                if (newboard[i] == '_')
                {
                    newboard[i] = 'O';
                    var score = MinMax(newboard, depth + 1, false);
                    newboard[i] = '_';
                    bestScore = Mathf.Max(score, bestScore);
                }
            }
            ShowDebugBoard(board);
            return bestScore;
        }
        else
        {
            var bestScore = 100;
            for (var i = 0; i < newboard.Length; i++)
            {
                if (newboard[i] == '_')
                {
                    newboard[i] = 'X';
                    var score = MinMax(newboard, depth + 1, true);
                    newboard[i] = '_';
                    bestScore = Mathf.Min(score, bestScore);
                }
            }
            ShowDebugBoard(board);
            return bestScore;
        }
    }

    private void ShowDebugBoard(char[] bbb)
    {
        string boardStr = "";
        for (int i = 0; i < bbb.Length; i += 3)
        {
            boardStr += bbb[i] + " " +
                bbb[i + 1] + " " + bbb[i + 2] + "\n";
        }
        Debug.Log(boardStr);
    }

    void HandleKeyPress(int key)
    {        
        if (Input.GetKeyDown(key.ToString()) &&
            board[key - 1] == '_')
        {
            OnPlayerClick?.Invoke();
            board[key-1] = 'X';
            ShowBoard();
            if (CheckWinner(board) == Participants.Player)
            {
                UIManager.SendUIMessage("Player Won!!!!");
                gameOver = true;
            }
            else
            if (isFullBoard())
            {
                UIManager.SendUIMessage("Game Over, no one Won...");
                gameOver = true;
            }
            turns++;
            playerTurn = false;
        }
    }

    void FillFreeSpot()
    {
        int attempts = 0;
        bool done = false;
        while (!done && attempts < 9)
        {
            attempts++;
            int spot = UnityEngine.Random.Range(0, board.Length);
            Debug.Log("spot: " + spot);
            if (board[spot] == '_')
            {
                board[spot] = 'O';
                done = true;                
                ShowBoard();
                if (CheckWinner(board) == Participants.AI)
                {
                    UIManager.SendUIMessage("Computer Won!!!!");
                    gameOver = true;
                }
                turns++;
                playerTurn = true;
            }
        }
    }

    Participants CheckWinner(char[] tmpboard)
    {
        var winner = IsWinning(tmpboard);
        if (winner == 'O')
        {
            return Participants.AI;
        }
        else if (winner == 'X')
        {
            return Participants.Player;
        }
        return Participants.None;
    }

    private char IsWinning(char[] tmpboard)
    { 
        //row
        for (int i = 0; i < tmpboard.Length; i += 3)
        {
            if (tmpboard[i] == tmpboard[i + 1] && tmpboard[i + 1] == tmpboard[i + 2] && tmpboard[i] != '_')
            {
                return tmpboard[i];
            }
        }
        //column
        for (int i = 0; i < 3; i++)
        {
            if (tmpboard[i] == tmpboard[i + 3] && tmpboard[i + 3] == tmpboard[i + 6] && tmpboard[i] != '_')
            {
                return tmpboard[i];
            }
        }

        if (tmpboard[0] == tmpboard[4] && tmpboard[4] == tmpboard[8] && tmpboard[0] != '_')
        {
            return tmpboard[0];
        }

        if (tmpboard[2] == tmpboard[4] && tmpboard[4] == tmpboard[6] && tmpboard[2] != '_')
        {
            return tmpboard[2];
        }
        return '_';
    }

    bool isFullBoard()
    {
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] == '_')
            {
                return false;
            }
        }
        return true;
    }
}

public enum Participants
{
    None,
    Player,
    AI
}