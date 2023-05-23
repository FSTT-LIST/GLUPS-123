using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tablesController : MonoBehaviour
{
    public GameObject[] marks;
    public Text[] score;
    public Sprite checkMark;
    public Sprite acrossMark;
    public Text scoreText;

    string Player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Player = PlayerPrefs.GetString("Player");
        showMarks();
        ShowLevelInfo();

    }
    /*in this function we will use the checkmark sprite in the tables that passed correctly and acrossmark in the tables 
        where the player failed also we will show the score for each table*/
    void showMarks()
    {
        //the number of the level is the fifth character ex (Level1)
        string lev = PlayerPrefs.GetString("level")[5].ToString();


        for (int i = 0; i < 9; i++)
        {
            int j = i + 1;
            if (PlayerPrefs.GetString("Player" + Player + "lev" + lev + "x" + j) == "1")
            {
                marks[i].gameObject.SetActive(true);
                marks[i].gameObject.GetComponent<Image>().sprite = checkMark;
                score[i].text = PlayerPrefs.GetInt("Player" + Player + "lev" + lev + "x" + j + "score") + "/10";
                score[i].color = new Color(66 / 255f, 140 / 255f, 1 / 255f);
            }

            else if (PlayerPrefs.GetString("Player" + Player + "lev" + lev + "x" + j) == "-1")
            {
                marks[i].gameObject.SetActive(true);
                marks[i].gameObject.GetComponent<Image>().sprite = acrossMark;
                score[i].text = PlayerPrefs.GetInt("Player" + Player + "lev" + lev + "x" + j + "score") + "/10";
                score[i].color = new Color(239 / 255f, 34 / 255f, 34 / 255f);
            }

            else
            {
                marks[i].gameObject.SetActive(false);
                score[i].text = "";

            }


        }
    }

    public void ShowLevelInfo()
    {

        int lev = int.Parse(PlayerPrefs.GetString("level")[5].ToString());
        int score = 0;

        for (int i = 0; i < 9; i++)
        {
            int j = i + 1;
            score += PlayerPrefs.GetInt("Player" + Player + "lev" + lev + "x" + j + "score");
        }

        scoreText.text = PlayerPrefs.GetInt("Player" + Player + "RewardScore").ToString() + " : عﻮﻤﺠﻤﻟﺍ";


    }
}
