using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardBack : MonoBehaviour

{
   //all cards animations
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

    // Clavier script is the script controller
    public Clavier clav;
   // this bool used for playing animation one time 
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
        AnimationCard =GetComponent<Animation>();

        clav = FindObjectOfType<Clavier>();


    }

    // Update is called once per frame
    void Update()
    {
    	/*when the answer is true the variable is_true of clavier_script will be true in this moment tuteur animation played 
    	for (1sec) then the value of is_true become false and the card animation will played
    	and also we used the y variable to know which card will moved*/

        if(clav.is_true == false){

        if (clav.y == 1)
        {
           
            if (is_played == false)
            {
                Animation1.Play();
               
                is_played = true;
            }
        }

        if (clav.y == 2)
        {
           
            if (is_played == true)
            {
                Animation2.Play();
                is_played = false;
            }
        }

        if (clav.y == 3)
        {
           
            if (is_played == false)
            {
                Animation3.Play();
                is_played = true;
            }
        }


        if (clav.y == 4)
        {
          
            if (is_played == true)
            {
                Animation4.Play();
                is_played = false;
            }
        }

        if (clav.y == 5)
        {
           
            if (is_played == false)
            {
                Animation5.Play();
                is_played = true;
            }
        }

        if (clav.y == 6)
        {
           
            if (is_played == true)
            {
                Animation6.Play();
                is_played = false;
            }
        }

        if (clav.y == 7)
        {
           
            if (is_played == false)
            {
                Animation7.Play();
                is_played = true;
            }
        }

        if (clav.y == 8)
        {
            
            if (is_played == true)
            {
                Animation8.Play();
                is_played = false;
            }
        }

        if (clav.y == 9)
        {
           
            if (is_played == false)
            {
                Animation9.Play();
                is_played = true;
            }
        }

        if (clav.y == 10)
        {
           
            if(is_played == true)
            {
                Animation10.Play();
                is_played = false;
            }
        }

      
       
        
        
        
       
        
        }
    }
}