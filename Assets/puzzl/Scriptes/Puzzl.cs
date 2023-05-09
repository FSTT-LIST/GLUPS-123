using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Puzzl : MonoBehaviour
{
    public GameObject[] puzzles;
    public GameObject puzzlePiece;
    public int[] ordre1 ;
    public int[] ordre2 ;
    public int[] ordre3 ;
    public int[] ordre4;
    public int[][] ordre = new int[4][];
    private string player;
    private int score;


    void Start()
    {
        player = PlayerPrefs.GetString("Player");
        ordre[0] = ordre1;
        ordre[1] = ordre2;
        ordre[2] = ordre3;
        ordre[3] = ordre4;

        calculateScore();
        if (score >= 10)
        {
            int x = score / 10;
            int y = new int();
            if (PlayerPrefs.GetInt("Player" + player + "scoreToatal") != 0)
            {
                if (x >= 1 && x <= 9)
                { y = 0;}
                else if (x >= 10 && x <= 18)
                { y = 1; x = x - 9; }
                else if (x >= 19 && x <= 27)
                { y = 2; x = x - 18; }
                else
                { y = 3; x = x - 27; }
            }
        
            if (PlayerPrefs.GetInt("Player" + player + "puzzle" + y + "piece" + x) == 0)
            {
                PlayerPrefs.SetInt("Player" + player + "puzzle" + y + "piece" + x, 1);
                puzzlePiece.transform.GetChild(0).GetComponent<Image>().sprite = puzzles[y].transform.GetChild(ordre[y][x-1]).GetComponent<Image>().sprite;
                puzzlePiece.SetActive(true);
                StartCoroutine("hidePiece");
            }
        }

    }
    void Update()
    {
        calculateScore();
    }

    IEnumerator hidePiece()
    {
        yield return new WaitForSeconds(3);
        puzzlePiece.SetActive(false);
    }

    private void calculateScore()
    {
        score = 0;
        for (int i = 1; i < 5; i++)
        {
            for (int j = 1; j < 10; j++)
            {
                score = score + PlayerPrefs.GetInt("Player" + PlayerPrefs.GetString("Player") + "lev" + i + "x" + j + "score");
            }
        }
    }
    
    /*    public void ShowPuzzl()
    {
        player = PlayerPrefs.GetString("Player");
        score = PlayerPrefs.GetInt("Player" + PlayerPrefs.GetString("Player") + "scoreToatal");
        int x = score / 10;
        int y = new int();
        if (PlayerPrefs.GetString("Player" + "scoreToatal").Length != 0)
        {
            if (x >= 0 && x <= 8)
            { y = 0; }
            else if (x > 9 && x <= 17)
            { y = 1; }
            else if (x > 18 && x <= 26)
            { y = 2; }
            else
            { y = 3; }
        }
        for (int i=0; i<9; i++)
        {
            puzzles[y].transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < puzzles.Length; i++)
        {
            if (i == y)
                puzzles[i].SetActive(true);
            else
                puzzles[i].SetActive(false);
        }



        CheckPuzzl();
    }

    public void CheckPuzzl()
    {
        calculateScore();
        
        score = PlayerPrefs.GetInt("Player" + PlayerPrefs.GetString("Player") + "scoreToatal");
        int x = score / 10;

        if (score >= 10 && score < 91)
        {
            for(int i=0; i<x; i++)
            {
                puzzles[0].transform.GetChild(ordre[0][i]).gameObject.SetActive(true);
            }
        } else if (score >= 100 && score < 181)
        {
            x = x-9;
            for (int i = 0; i < 9; i++)
            {
                puzzles[0].transform.GetChild(ordre[0][i]).gameObject.SetActive(true);
            }
            for (int j = 0; j < x; j++)
            {
                puzzles[1].transform.GetChild(ordre[1][j]).gameObject.SetActive(true);
            }
        } else if (score >= 190 && score < 271)
        {
            x = x - 18;
            for (int i = 0; i < 9; i++)
            {
                puzzles[0].transform.GetChild(ordre[0][i]).gameObject.SetActive(true);
            }
            for (int j = 0; j < 9; j++)
            {
                puzzles[1].transform.GetChild(ordre[1][j]).gameObject.SetActive(true);
            }
            for (int j = 0; j < x; j++)
            {
                puzzles[2].transform.GetChild(ordre[2][j]).gameObject.SetActive(true);
            }
        } else if (score >= 280 && score < 361)
        {
            x = x - 27;
            for (int i = 0; i < 9; i++)
            {
                puzzles[0].transform.GetChild(ordre[0][i]).gameObject.SetActive(true);
            }
            for (int i = 0; i < 9; i++)
            {
                puzzles[1].transform.GetChild(ordre[1][i]).gameObject.SetActive(true);
            }
            for (int i = 0; i < 9; i++)
            {
                puzzles[2].transform.GetChild(ordre[2][i]).gameObject.SetActive(true);
            }
            for (int i = 0; i < x; i++)
            {
                puzzles[3].transform.GetChild(ordre[3][i]).gameObject.SetActive(true);
            }
        }
    }
    public void ShowOwnPuzzle1()
    {
        for (int i = 0; i < 9; i++)
        {
            puzzles[1].transform.GetChild(i).gameObject.SetActive(false);
            puzzles[2].transform.GetChild(i).gameObject.SetActive(false);
            puzzles[3].transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < puzzles.Length; i++)
        {
            if (i == 0)
                puzzles[i].SetActive(true);
            else
                puzzles[i].SetActive(false);
        }
        CheckPuzzl();
    }
    public void ShowOwnPuzzle2()
    {
        for (int i = 0; i < 9; i++)
        {
            puzzles[0].transform.GetChild(i).gameObject.SetActive(false);
            puzzles[2].transform.GetChild(i).gameObject.SetActive(false);
            puzzles[3].transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < puzzles.Length; i++)
        {
            if (i == 1)
                puzzles[i].SetActive(true);
            else
                puzzles[i].SetActive(false);
        }
        CheckPuzzl();
    }
    public void ShowOwnPuzzle3()
    {
        for (int i = 0; i < 9; i++)
        {
            puzzles[0].transform.GetChild(i).gameObject.SetActive(false);
            puzzles[1].transform.GetChild(i).gameObject.SetActive(false);
            puzzles[3].transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < puzzles.Length; i++)
        {
            if (i == 2)
                puzzles[i].SetActive(true);
            else
                puzzles[i].SetActive(false);
        }
        CheckPuzzl();
    }
    public void ShowOwnPuzzle4()
    {
        for (int i = 0; i < 9; i++)
        {
            puzzles[0].transform.GetChild(i).gameObject.SetActive(false);
            puzzles[1].transform.GetChild(i).gameObject.SetActive(false);
            puzzles[2].transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < puzzles.Length; i++)
        {
            if (i == 3)
                puzzles[i].SetActive(true);
            else
                puzzles[i].SetActive(false);
        }
        CheckPuzzl();
    }*/
}