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
    private int lev;


    private void Start()
    {
        player = PlayerPrefs.GetString("Player");
        lev = int.Parse(PlayerPrefs.GetString("level")[5].ToString());

        ordre[0] = ordre1;
        ordre[1] = ordre2;
        ordre[2] = ordre3;
        ordre[3] = ordre4;

        calculateScore();

        if (score >= 10)
        {
            int x = score / 10;

            if(PlayerPrefs.GetInt("Player" + player + "lev" + lev + "piece" + x) == 0)
            {
                PlayerPrefs.SetInt("Player" + player + "lev" + lev + "piece" + x, 1);
                puzzlePiece.transform.GetChild(0).GetComponent<Image>().sprite = puzzles[lev - 1].transform.GetChild(ordre[lev - 1][x-1]).GetComponent<Image>().sprite;
                puzzlePiece.SetActive(true);
                StartCoroutine("hidePiece");
                    
            }
        }
    }

    IEnumerator hidePiece()
    {
        yield return new WaitForSeconds(3);
        puzzlePiece.SetActive(false);
    }


    public void ShowPuzzl()
    {
        player = PlayerPrefs.GetString("Player");
        lev = int.Parse(PlayerPrefs.GetString("level")[5].ToString()); 

        for (int i=0; i<9; i++)
        {
            puzzles[lev - 1].transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < puzzles.Length; i++)
        {
            if (i == lev - 1)
                puzzles[i].SetActive(true);
            else
                puzzles[i].SetActive(false);
        }



        CheckPuzzl();
    }

    public void CheckPuzzl()
    {
        calculateScore();


        if (score >= 10)
        {
            int x = score / 10;
            for(int i=0; i<x; i++)
            {
                puzzles[lev - 1].transform.GetChild(ordre[lev-1][i]).gameObject.SetActive(true);
            }

           
        }
    }

    private void calculateScore()
    {
        score = 0;

        for (int i = 0; i < 9; i++)
        {
            int j = i + 1;
            score += PlayerPrefs.GetInt("Player" + player + "lev" + lev + "x" + j + "score");
        }
    }
}
