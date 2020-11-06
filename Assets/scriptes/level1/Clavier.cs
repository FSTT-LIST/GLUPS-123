//this is the game controller Script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Clavier : MonoBehaviour

{
   // the Text where the answer will show
    public Text result;
    // the help text 
    public Text help;
    // the fix number on the scene (the number of the table)
    public Text num;
    // the Text where the answer will show
    public Text scoreText;
    // the Text where the actual Player name will show
    public Text PlayeName;
    // this text show the final result of this table (passed or failed)
    public Text ShowMsgResult;
    //the final score of the table
    public Text finalScore;

    
   // bool to know if the answer is correct if it's true the answer is correct if not so there is no answer yet
    public bool is_true = false;
    // bool to know if the answer is incorrect if it's true the answer is incorrect if not so there is no answer yet
    public bool is_false = false;
    //this bool to block the number buttons (clavier) if he put 2 numbers
    private bool is_blocked = false;
    //when the player finish all the cards of this table the value of is_finished will be true
    public bool is_finished = false;
    //has_help will be true when the player need help
    public bool has_help;
    //when is_muted is true there will be no song in the game 
    public bool is_muted;



    // x is the value of the fix number (table)
    public int x;
    // y is the value of the card
    public int y;
    //z is the value of multiplying x by y
    private int z;
    //tis variable to stock the score value
    public int score;
    // the index of the level (for exemple 1 for level1)
    public int levelIndex;
    //the number of the attempts
    private int essaieNumber;
    //in this variable we will stock the name of the player
    public string Player;


    
    //Boy is the mal avatar / Girl is the femal avatar
    public GameObject Boy;
    public GameObject Girl;
    //img is the player icon it can be mal icon or femal icon
    public GameObject img;
    //the panel where the finalscore and show message result will show when the player finish the table 
    public GameObject panel;
    public GameObject instructionsImg;
    public GameObject instructionsCanvas;
       public GameObject bigStar;
     public GameObject bigStarWhite;


    
    // this audio clip will play when the answer is incorrect
    public AudioClip loseAudio;
    // this audio clip will play when the answer is correct
    public AudioClip winAudio;
    //this audio clip will play when the player passed in the table 
    public AudioClip loser;
     //this audio clip will play when the player failed in the table 
    public AudioClip winner;

    
    //the song of the game 
    public AudioSource song;
    AudioSource AudioAnswer;

    public Animation eraser;
    
    // Animator of the avatar girl/boy
    public Animator girl;
    public Animator boy;

    //the image of girlicon 
    public Sprite girlIcon;
    



    void Start()
    {
        //PlayerPrefs used to Store and accesses player preferences between game sessions. 
        if(PlayerPrefs.GetInt("mute")==1)
        //to cut the music sound
        song.Pause();
        //stocking the name of the Player
        Player= PlayerPrefs.GetString("Player");
        //set text of playename
        PlayeName.text=PlayerPrefs.GetString("name:"+Player);
        
        //stock the number of the table
        x=PlayerPrefs.GetInt("x:");
        num.text = x.ToString();

        //to get the animator component 
        girl = girl.GetComponent<Animator>();
        boy = boy.GetComponent<Animator>();

        // to get the audioSource component
        AudioAnswer = GetComponent<AudioSource>();
        eraser = eraser.GetComponent<Animation>();

        //we get the sex of the player to set the player icon 
        if(PlayerPrefs.GetString("sex"+Player)=="femme")
        img.gameObject.GetComponent<Image>().sprite=girlIcon;

        if(PlayerPrefs.GetInt("Player"+Player+"scoreToatal")==0 & PlayerPrefs.GetInt("Player"+Player+"lev"+levelIndex+"x"+x+"score")==0){
            instructionsImg.gameObject.SetActive(true);
            instructionsCanvas.gameObject.SetActive(true);
              result.text="0";
      
        }

        
    }



    void Update()
    {
        if (result.text == "?")
        {
            is_false = false;
            is_true = false;
            is_blocked = false;
            StopAllCoroutines();
            //we stop the eraser animation
             eraser.Stop();
        }

       


if(PlayerPrefs.GetString("sex"+Player)=="femme"){
      //we use the girl avatar
      Girl.SetActive(true);
      //we set the animator variables
      girl.SetBool("isTrue", is_true);
      girl.SetBool("isFalse", is_false);

  }

  if(PlayerPrefs.GetString("sex"+Player)=="homme"){
    //we use the boy avatar
       Boy.SetActive(true);
       //we set the animator variables
       boy.SetBool("isTrue", is_true);
       boy.SetBool("isFalse", is_false);

}



    }



public void saisir(string number)
    {
        z = x * y;


        if (result.text == "?")
        {
            result.text = number;
            StartCoroutine(TestCoroutine());
        }


        else
        {


            if (is_blocked == false)
            {
                result.text = result.text + number;
                //we block the input because he is already put 2 numbers
                is_blocked = true;
                StartCoroutine(TestCoroutine());

            }

        }

       if (is_blocked & has_help)
       {

       	helpme(number);
      } 

        

    }

       

    public void delete()
    {
        if (is_true == false & has_help == false)
            result.text = "?";
           
    }


    private void helpme(string number)
    {

    	//here we will compare the numbers given by the player with the help Text in order to make the player input only the correct answer
        if(help.text[0]==number[0]){ result.text=number; }
        // in the case if the answer contain 2 numbers
      	if(help.text.Length==2)
      	{
      	if(help.text[1]==number[0]){ result.text=result.text+number; }
        }
      	if(result.text == help.text)
      	{
           // A coroutine is a function that can suspend its execution (yield) until the given YieldInstruction finishes.
            StartCoroutine(TestCoroutine());
      	 help.gameObject.SetActive(false);
      	}
    }


      IEnumerator TestCoroutine()
{

    
    if (result.text == z.ToString() | result.text == "0" + z.ToString())
    {   //if the answer is correct we will not wait to say it's correct 
        yield return new WaitForSeconds(0);
        CheckAnswer();
    }

    else
    {
         //if the answer is incorrect we will wait 2sec to say it's incorrect 
        yield return new WaitForSeconds(2);
        CheckAnswer();
    }
}



    void CheckAnswer()
    { 

        //is_true must be false to not execute these lines more than one time

        if ((result.text == z.ToString() | result.text == "0" + z.ToString()) & is_true == false)
        {

            if (essaieNumber < 3 & y != 0)
            {
                score++;
            }

            y++;



            essaieNumber = 0;


            // y here begin from 0 to 10
            if (y == 11)
            {

                is_finished = true;

            }

            scoreText.text = score.ToString();

          
            Debug.Log("bravo");
            AudioAnswer.clip = winAudio;
            //play the audio just once
            AudioAnswer.PlayOneShot(AudioAnswer.clip);
          
            is_true = true;
            is_blocked = true;
            has_help = false;
            //we use this coroutine to be able to show the answer and the animation of the avatar
            StartCoroutine(wait1sec());

        } 
        

        else
        {
            if (is_true == false & is_false == false & result.text != "?"){
                is_false = true;
                checkEssaieNumber();
            }
        }
    }





    IEnumerator wait1sec()
{
    yield return new WaitForSeconds(1);
   
    result.text = "?";
    storing();
   
    if(is_finished)
    {
    	levelUp();
    }

}

public void checkEssaieNumber()
    {
        if (essaieNumber < 2)
        {
            
            Debug.Log("ressayer");
            eraser.Play();
      
    }

        if (essaieNumber == 2)
        {

           //in this case we will show the help text
        	result.text="";
        	is_blocked=true;
        	help.text=z.ToString();
        	help.gameObject.SetActive(true);
        	has_help = true;


        }
          AudioAnswer.clip = loseAudio;
        AudioAnswer.PlayOneShot(AudioAnswer.clip);
        essaieNumber++;

    }


public void storing(){
       //if there is an other score for this scene then we compare this score with the old one in order to stock the better score
    if(score >  PlayerPrefs.GetInt("Player"+Player+"lev"+levelIndex+"x"+x+"score"))
 { 
      /* PlayerPrefs.SetInt("Player"+Player+"lev"+levelIndex+"x"+x, ? ) in the place of ? we put the value of the varible 
       we use "1" if the player win "-1" if he failed "0" if he don't play this scene yet */
      if(score > 8){
    
    PlayerPrefs.SetString("Player"+Player+"lev"+levelIndex+"x"+x,"1");
    PlayerPrefs.SetInt("Player"+Player+"lev"+levelIndex+"x"+x+"score",score);

   
    }

      else{
    PlayerPrefs.SetString("Player"+Player+"lev"+levelIndex+"x"+x,"-1");
    PlayerPrefs.SetInt("Player"+Player+"lev"+levelIndex+"x"+x+"score",score);
   
    }

 }

}


public void levelUp(){
 
 
   panel.gameObject.SetActive(true);
   song.Pause();


       if(score > 8){
   ShowMsgResult.text="ﺖﻨﺴﺣأ";
    AudioAnswer.PlayOneShot(winner);}
   else{
    ShowMsgResult.text="ﺔﻴﻧﺎﺛ لوﺎﺣ";
     AudioAnswer.PlayOneShot(loser);
}

   finalScore.text=score.ToString() +"  : ﺔﺠﻴﺘﻨﻟا";
     ShowStar();
 
}


    public void ShowStar(){
        int count=0;

        for(int i=0 ; i<9 ; i++)
            {
                int j = i+1;
                if(PlayerPrefs.GetString("Player"+Player+"lev"+levelIndex+"x"+j)=="1"){

                count++;
            }
         }

           if ((count == 3 | count ==6 | count==9) & PlayerPrefs.GetInt("Player"+Player+"lev"+levelIndex+"star"+count)==0)
        {
            bigStar.gameObject.SetActive(true);
            PlayerPrefs.SetInt("Player"+Player+"lev"+levelIndex+"star"+count,1);

        }

           if ((count == 1 | count ==4 | count==7) & PlayerPrefs.GetInt("Player"+Player+"lev"+levelIndex+"star"+count)==0)
        {
            bigStarWhite.gameObject.SetActive(true);
            PlayerPrefs.SetInt("Player"+Player+"lev"+levelIndex+"star"+count,1);

        }

    }

public void BackButton()
{

	//we use this function to load an other scene in this case the name of the scene is "map"
    SceneManager.LoadScene("map");

}

public void Homebutton()
{
      
	SceneManager.LoadScene("mainMenu");

}


public void restartbutton()
{
    
SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}



}



