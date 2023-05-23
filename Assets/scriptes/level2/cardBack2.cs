using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardBack2 : MonoBehaviour

{

    public Animation anim9;
    public Animation anim8;
    public Animation anim7;
    public Animation anim6;
    public Animation anim5;
    public Animation anim4;
    public Animation anim3;
    public Animation anim2;
    public Animation anim1;
    public Animation anim0;
    private Animation anim;
    public clavier2 clav;

    private bool is_played;

    // Start is called before the first frame update
    void Start()
    {


        anim9 = anim9.GetComponent<Animation>();
        anim8 = anim8.GetComponent<Animation>();
        anim7 = anim7.GetComponent<Animation>();
        anim6 = anim6.GetComponent<Animation>();
        anim5 = anim5.GetComponent<Animation>();
        anim4 = anim4.GetComponent<Animation>();
        anim3 = anim3.GetComponent<Animation>();
        anim2 = anim2.GetComponent<Animation>();
        anim1 = anim1.GetComponent<Animation>();
        anim0 = anim0.GetComponent<Animation>();
        anim = GetComponent<Animation>();



        clav = FindObjectOfType<clavier2>();


    }

    // Update is called once per frame
    void Update()
    {
        if (clav.is_true == false)
        {
            if (clav.y == 9)
            {

                if (is_played == false)
                {
                    anim9.Play();
                    is_played = true;
                }




            }
            if (clav.y == 8)
            {

                if (is_played == true)
                {
                    anim8.Play();
                    is_played = false;
                }
            }
            if (clav.y == 7)
            {

                if (is_played == false)
                {
                    anim7.Play();
                    is_played = true;
                }
            }
            if (clav.y == 6)
            {

                if (is_played == true)
                {
                    anim6.Play();
                    is_played = false;
                }
            }
            if (clav.y == 5)
            {

                if (is_played == false)
                {
                    anim5.Play();
                    is_played = true;
                }
            }
            if (clav.y == 4)
            {

                if (is_played == true)
                {
                    anim4.Play();
                    is_played = false;
                }
            }
            if (clav.y == 3)
            {

                if (is_played == false)
                {
                    anim3.Play();
                    is_played = true;
                }
            }
            if (clav.y == 2)
            {

                if (is_played == true)
                {
                    anim2.Play();
                    is_played = false;
                }
            }
            if (clav.y == 1)
            {

                if (is_played == false)
                {
                    anim1.Play();
                    is_played = true;
                }
            }
            if (clav.y == 0)
            {

                if (is_played == true)
                {
                    anim0.Play();
                    is_played = false;
                }
            }

        }
    }
}
