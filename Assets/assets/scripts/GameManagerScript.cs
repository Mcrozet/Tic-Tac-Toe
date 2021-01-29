using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManagerScript : MonoBehaviour
{
    public string[] Tab = new string[9];
    [SerializeField] List<int> choice = new List<int>();
    [SerializeField] private GameObject panelGameOver;

    private void Start()
    {
        var turnChoice = Random.Range(0, 2);
        if(turnChoice == 0) ComputerPlay();
    }

    public void ComputerPlay()
    {
        choice.Clear();

        for (var i = 0; i < Tab.Length; i++)
        {
            if(Tab[i] == string.Empty) choice.Add(i);
        }
        
        var button = choice[Random.Range(0, choice.Count)];

        Button btn = GameObject.Find(button.ToString()).GetComponent<Button>();

        btn.interactable = false;

        btn.GetComponentInChildren<Text>().text = "O";

        Tab[button] = "O";

        if (isWinner("O"))
        {
            GameOver("O");
            return;
        }

        if (!ArrayIsFull()) return;
        Draw();
        return;

    }
    
    // Check if someone win game and color buttons
    public bool isWinner(string p)
    {
        var s1 = Tab[0] == p && Tab[1] == p && Tab[2] == p;
        var s2 = Tab[3] == p && Tab[4] == p && Tab[5] == p;
        var s3 = Tab[6] == p && Tab[7] == p && Tab[8] == p;
        
        var s4 = Tab[0] == p && Tab[3] == p && Tab[6] == p;
        var s5 = Tab[1] == p && Tab[4] == p && Tab[7] == p;
        var s6 = Tab[2] == p && Tab[5] == p && Tab[8] == p;
        
        var s7 = Tab[0] == p && Tab[4] == p && Tab[8] == p;
        var s8 = Tab[2] == p && Tab[4] == p && Tab[6] == p;

        var solutions = new[] {s1, s2, s3, s4, s5, s6, s7, s8};
        var buttonsToColor = new string[]
        {
            "0,1,2", 
            "3,4,5", 
            "6,7,8",
            "0,3,6",
            "1,4,7", 
            "2,5,8", 
            "0,4,8", 
            "2,4,6"
        };

        for (var index = 0; index < solutions.Length; index++)
        {
            if (solutions[index])
            {
                if (p == "X")
                {
                    colorButtons(buttonsToColor[index], new Color(1f, 0.52f, 0.09f));
                    panelGameOver.GetComponentInChildren<Text>().text = "X\nWin";
                }
                else
                {
                    colorButtons(buttonsToColor[index], new Color(0.19f, 1f, 0.2f));
                    panelGameOver.GetComponentInChildren<Text>().text = "O\nwin";
                }
                return true;
            }
        }
        return false;
    }

    // array full = draw
    public bool ArrayIsFull()
    {
        foreach (var t in Tab)
        {
            if (t == string.Empty) return false;
        }
        return true;
    }

    public void GameOver(string winner)
    {
        Debug.Log(winner + "win");
        panelGameOver.SetActive(true);
    }
    
    public void Draw()
    {
        colorButtons("0,1,2,3,4,5,6,7,8", new Color(0.24f, 0.61f, 1f));
        panelGameOver.SetActive(true);
        panelGameOver.GetComponentInChildren<Text>().text = "Draw";
    }

    private void colorButtons(string numbers, Color colorPlayer)
    {
        string[] number = numbers.Split(',');
        for (int i = 0; i < number.Length; i++)
        {
            GameObject.Find(number[i]).GetComponent<Button>().GetComponent<Image>().color = colorPlayer;
        }
    }
}
