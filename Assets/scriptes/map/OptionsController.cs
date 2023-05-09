using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{

	 List<string> options = new List<string>();
	 //this is our dropdown
	 Dropdown dropi;
	 //calcul use to count the number of players
     public int calcul;

      string Player;
   
   // the text on the dropdown
   public Text label;
   public Text score;
   
   //the panel of tables 
   public GameObject numbers;
   // the player icon on the dropdown
   public GameObject img;

   public Sprite boyIcon;
   public Sprite girlIcon;
 
  





    // Start is called before the first frame update
    void Start()
    {
        
    	//this variable will equal to one if the previous scene is the mainMenu in this case we will hide the panel of the tables
        if(PlayerPrefs.GetInt("hide")==1)
        numbers.SetActive(false);
        //here the variable will equal to zero if the previous scene is a level in this case we will show the panel of the tables
        else
        numbers.SetActive(true);
        PlayerPrefs.SetInt("hide",0);
 
      
        // count is the last id Player number
         int count = PlayerPrefs.GetInt("count");
        
        for(int i=0 ; i<= count ; i++){
           
             string id = PlayerPrefs.GetString("id:"+ i);
             if(id!="")
              calcul++;
         }





       // create a list of players 
          options = new List<string>(new string[calcul]);
        int number = 0;
        for(int i=0 ; i<= count ; i++){
             number++;
            
            string id = PlayerPrefs.GetString("id:"+ i);
            string name = PlayerPrefs.GetString("name:"+ i);

        if(id!=""){
           
           options[number-1]=name;
          
        
         

        }
        else { number --;}

            ScoreCounter();
        }



    dropi = GetComponent<Dropdown>();
    dropi.ClearOptions();
    //add the options list to the dropdown options
    dropi.AddOptions(options);
    Player=PlayerPrefs.GetString("Player");

    for(int i=0 ; i<calcul; i++){
        if(PlayerPrefs.GetString("name:"+Player)==options[i])
        dropi.value=i;
      

    }
       if(PlayerPrefs.GetString("sex"+Player)=="femme")
        img.gameObject.GetComponent<Image>().sprite=girlIcon;

   
    }

    // Update is called once per frame
    void Update()
    {
      ScoreCounter();
        
    }


    public void changePlayer()
    {
    	/*when we select a player from the dropdown options we have to change the player variable of PlayerPrefs
    	 also the icon depending to the gender of the player*/

        int count = PlayerPrefs.GetInt("count");
        
        for(int i=0 ; i<= count ; i++){

            if(PlayerPrefs.GetString("name:"+i)==label.text){
                PlayerPrefs.SetString("Player",i.ToString());
                if(PlayerPrefs.GetString("sex"+i)=="femme")
                img.gameObject.GetComponent<Image>().sprite=girlIcon;
                else
                 img.gameObject.GetComponent<Image>().sprite=boyIcon;
            }
        }

    }


//we use this function to count the total score for the actual player and show it
    public void ScoreCounter(){
      int total = 0;


      for(int i=1 ; i<5 ; i++){
        for(int j=1 ; j<10 ; j++){

          total = total + PlayerPrefs.GetInt("Player"+PlayerPrefs.GetString("Player")+"lev"+i+"x"+j+"score");

        }
      }
score.text = total.ToString()+"  :عﻮﻤﺠﻤﻟا";
PlayerPrefs.SetInt("Player"+PlayerPrefs.GetString("Player")+"scoreToatal",total);

    }



}


