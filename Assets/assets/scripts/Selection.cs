using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selection : MonoBehaviour
{
    private GameManagerScript gm;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    public void WriteX()
    {
        GetComponentInChildren<Text>().text = "X";
        GetComponent<Button>().interactable = false;
        gm.Tab[int.Parse(gameObject.name)] = "X";

        if (gm.isWinner("X"))
        {
            gm.GameOver("X");
        }
        else if (gm.ArrayIsFull())
        {
            gm.Draw();
        }
        else
        {
            gm.ComputerPlay();
        }
    }
}
