using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button[] Cells = null;
    [SerializeField] Text MsgTxt = null;
    internal void ShowBoard(char[] board)
    {
        for (int i = 0; i < board.Length; i++)
        {
            var txt = Cells[i].GetComponentInChildren<Text>();
            if (board[i] == '_')
            {
                txt.text = (i+1).ToString();
            }
            else
            {
                Cells[i].GetComponent<Image>().color = Color.yellow;
                txt.text = board[i].ToString();
            }
        }
    }

    public void OnReset()
    {
        SceneManager.LoadScene(0);
    }

    public void SendUIMessage(string msg)
    {
        MsgTxt.text = msg;
        Debug.Log(msg);
    }
}
