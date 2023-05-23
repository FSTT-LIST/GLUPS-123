using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class etoileController : MonoBehaviour
{
    public Clavier clav;
    // table of coloring BarFill (4 BarFill)
    public GameObject[] BarFill;
    public Sprite starSprite;
    public Sprite whitestarSprite;


    // Start is called before the first frame update
    void Start()
    {
        clav = FindObjectOfType<Clavier>();
        UpdateStars();

    }


    private void UpdateStars()
    {  // we use count variable to count the number of passed tables
        int count = 0;

        for (int i = 0; i < 9; i++)
        {
            int j = i + 1;

            //PlayerPrefs used to Store and accesses player preferences between game sessions. 
            // clav.Player is the id of the actual player
            //clav.levelIndex is the number of the actual level
            // and j is the number of the table
            /* the value of ("Player"+clav.Player+"lev"+clav.levelIndex+"x"+j) equal to 1 that's means 
            this table of this level played by this Player is passed , if it's equal to 0 or (-1) so it's not passed yet 
            or the player was failed */
            if (PlayerPrefs.GetString("Player" + clav.Player + "lev" + clav.levelIndex + "x" + j) == "1")
            {

                count++;

            }

        }
        // if the player pass 3 tables correctly he will win one star, the first star gameObject will be active 
        if (count > 2)
        { BarFill[0].gameObject.GetComponent<SpriteRenderer>().sprite = starSprite; }
        else
            BarFill[0].gameObject.GetComponent<SpriteRenderer>().sprite = null;

        // if the player pass 6 tables correctly he will win two BarFill, the second star gameObject will be active 
        if (count > 5)
        { BarFill[1].gameObject.GetComponent<SpriteRenderer>().sprite = starSprite; }
        else
            BarFill[1].gameObject.GetComponent<SpriteRenderer>().sprite = null;

        // if the player pass 9 tables correctly he will win three BarFill, the third star gameObject will be active 
        if (count > 8)
        { BarFill[2].gameObject.GetComponent<SpriteRenderer>().sprite = starSprite; }
        else
            BarFill[2].gameObject.GetComponent<SpriteRenderer>().sprite = null;



        if (count == 1 | count == 2)
        { BarFill[0].gameObject.GetComponent<SpriteRenderer>().sprite = whitestarSprite; }




        if (count == 4 | count == 5)
        { BarFill[1].gameObject.GetComponent<SpriteRenderer>().sprite = whitestarSprite; }




        if (count == 7 | count == 8)
        { BarFill[2].gameObject.GetComponent<SpriteRenderer>().sprite = whitestarSprite; }



    }

}