using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardBack3 : MonoBehaviour

{

    public Animation Animation1;
    public Animation Animation2;
    public Animation Animation3;
    public Animation Animation4;
    public Animation Animation5;
    public Animation Animation6;
    public Animation Animation7;
    public Animation Animation8;
    public Animation Animation9;
    public Animation Animation10;
    private Animation AnimationCard;


    public Clavier3 clav;

    private bool is_played;
    // Start is called before the first frame update
    void Start()
    {

        Animation1 = Animation1.GetComponent<Animation>();
        Animation2 = Animation2.GetComponent<Animation>();
        Animation3 = Animation3.GetComponent<Animation>();
        Animation4 = Animation4.GetComponent<Animation>();
        Animation5 = Animation5.GetComponent<Animation>();
        Animation6 = Animation6.GetComponent<Animation>();
        Animation7 = Animation7.GetComponent<Animation>();
        Animation8 = Animation8.GetComponent<Animation>();
        Animation9 = Animation9.GetComponent<Animation>();
        Animation10 = Animation10.GetComponent<Animation>();
        AnimationCard = GetComponent<Animation>();

        clav = FindObjectOfType<Clavier3>();


    }

    // Update is called once per frame
    void Update()
    {

        if (clav.is_true == false)
        {

            if (clav.i == 2)
            {

                if (is_played == false)
                {
                    Animation1.Play();

                    is_played = true;
                }
            }

            if (clav.i == 3)
            {

                if (is_played == true)
                {
                    Animation2.Play();
                    is_played = false;
                }
            }

            if (clav.i == 4)
            {

                if (is_played == false)
                {
                    Animation3.Play();
                    is_played = true;
                }
            }


            if (clav.i == 5)
            {

                if (is_played == true)
                {
                    Animation4.Play();
                    is_played = false;
                }
            }

            if (clav.i == 6)
            {

                if (is_played == false)
                {
                    Animation5.Play();
                    is_played = true;
                }
            }

            if (clav.i == 7)
            {

                if (is_played == true)
                {
                    Animation6.Play();
                    is_played = false;
                }
            }

            if (clav.i == 8)
            {

                if (is_played == false)
                {
                    Animation7.Play();
                    is_played = true;
                }
            }

            if (clav.i == 9)
            {

                if (is_played == true)
                {
                    Animation8.Play();
                    is_played = false;
                }
            }

            if (clav.i == 10)
            {

                if (is_played == false)
                {
                    Animation9.Play();
                    is_played = true;
                }
            }

            if (clav.i == 11)
            {

                if (is_played == true)
                {
                    Animation10.Play();
                    is_played = false;
                }
            }








        }
    }
}