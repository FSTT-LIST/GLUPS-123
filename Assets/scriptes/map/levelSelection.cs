using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class levelSelection : MonoBehaviour
{



    public GameObject[] stars;

   
    public Text levelText;

    public Sprite starSprite;
    public Sprite videstarSprite;
    public Sprite whitestarSprite;
    public string level;
    string Player;



    // Start is called before the first frame update
    void Start()
    {

       
        if (PlayerPrefs.GetInt("x:") == 0)
        {
            PlayerPrefs.SetInt("x:", 1);

        }
        levelText.text = level;

    }

    // Update is called once per frame
    void Update()
    {
        //PlayerPrefs used to Store and accesses player preferences between game sessions. 
        Player = PlayerPrefs.GetString("Player");
        storing();


    }


    // we call this function to show the stars that the actual player has
    private void storing()
    {
        int lev = int.Parse(gameObject.name);
        int count = 0;

        for (int i = 0; i < 9; i++)
        {
            int j = i + 1;
            if (PlayerPrefs.GetString("Player" + Player + "lev" + lev + "x" + j) == "1")
            {
                count++;

            }

        }
        // if the player pass 3 tables correctly he will win one star, the first star gameObject will be active 
        if (count > 2)
        { stars[0].gameObject.GetComponent<Image>().sprite = starSprite; }
        else
            stars[0].gameObject.GetComponent<Image>().sprite = videstarSprite;

        // if the player pass 6 tables correctly he will win two stars, the second star gameObject will be active 
        if (count > 5)
        { stars[1].gameObject.GetComponent<Image>().sprite = starSprite; }
        else
            stars[1].gameObject.GetComponent<Image>().sprite = videstarSprite;

        // if the player pass 9 tables correctly he will win three stars, the third star gameObject will be active 
        if (count > 8)
        { stars[2].gameObject.GetComponent<Image>().sprite = starSprite; }
        else
            stars[2].gameObject.GetComponent<Image>().sprite = videstarSprite;





        if (count == 1 | count == 2)
        { stars[0].gameObject.GetComponent<Image>().sprite = whitestarSprite; }




        if (count == 4 | count == 5)
        { stars[1].gameObject.GetComponent<Image>().sprite = whitestarSprite; }




        if (count == 7 | count == 8)
        { stars[2].gameObject.GetComponent<Image>().sprite = whitestarSprite; }




    }

    public void pressSelection(string levelName)
    {

        PlayerPrefs.SetString("level", levelName);
        levelText.text = level;
        

    }



    public void BackButton()
    {
        SceneManager.LoadSceneAsync("mainMenu");

    }



    public void choisirX(int x)
    {
        // x is the number of the table selected
        PlayerPrefs.SetInt("x:", x);
        SceneManager.LoadScene(PlayerPrefs.GetString("level"));


    }

  


}