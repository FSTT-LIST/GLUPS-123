using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class etoileController3 : MonoBehaviour
{
    public Clavier3 clav;
    public GameObject[] stars;
    public Sprite starSprite;
    public Sprite whitestarSprite;


    // Start is called before the first frame update
    void Start()
    {
        clav = FindObjectOfType<Clavier3>();
        UpdateStars();

    }


    private void UpdateStars()
    {
        int count = 0;

        for (int i = 0; i < 9; i++)
        {
            int j = i + 1;
            if (PlayerPrefs.GetString("Player" + clav.Player + "lev" + clav.levelIndex + "x" + j) == "1")
            {

                count++;

            }

        }
        // if the player pass 3 tables correctly he will win one star, the first star gameObject will be active 
        if (count > 2)
        { stars[0].gameObject.GetComponent<SpriteRenderer>().sprite = starSprite; }
        else
            stars[0].gameObject.GetComponent<SpriteRenderer>().sprite = null;

        // if the player pass 6 tables correctly he will win two stars, the second star gameObject will be active 
        if (count > 5)
        { stars[1].gameObject.GetComponent<SpriteRenderer>().sprite = starSprite; }
        else
            stars[1].gameObject.GetComponent<SpriteRenderer>().sprite = null;

        // if the player pass 9 tables correctly he will win three stars, the third star gameObject will be active 
        if (count > 8)
        { stars[2].gameObject.GetComponent<SpriteRenderer>().sprite = starSprite; }
        else
            stars[2].gameObject.GetComponent<SpriteRenderer>().sprite = null;



        if (count == 1 | count == 2)
        { stars[0].gameObject.GetComponent<SpriteRenderer>().sprite = whitestarSprite; }




        if (count == 4 | count == 5)
        { stars[1].gameObject.GetComponent<SpriteRenderer>().sprite = whitestarSprite; }




        if (count == 7 | count == 8)
        { stars[2].gameObject.GetComponent<SpriteRenderer>().sprite = whitestarSprite; }


    }
    // Update is called once per frame
    void Update()
    {

    }
}