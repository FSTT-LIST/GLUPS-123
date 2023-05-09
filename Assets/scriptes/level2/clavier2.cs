using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class clavier2 : MonoBehaviour

{

    public Text result;
     public Text help;
    public Text num;
    public Text scoreText;
    public Text PlayeName;
      public Text ShowMsgResult;
    public Text finalScore;
    public int score;
    public int x;
    public int y;
    private int z;
    public bool is_true = false;
    private bool is_false = false;
    private bool is_blocked = false;
    public bool is_finished = false;
    public bool has_help;
    public bool is_muted;
    public int levelIndex;
   public string Player;

   
    private int essaieNumber;
       public Animator girl;
    public Animator boy;
    public GameObject Boy;
    public GameObject Girl;
    public AudioClip loseAudio;
    public AudioClip winAudio;
     public AudioClip loser;
    public AudioClip winner;
    public Animation eraser;
     public AudioSource song;

      public GameObject img;
    public Sprite girlIcon;
    public GameObject panel;
    public GameObject instructionsImg;
    public GameObject instructionsCanvas;
    public GameObject card;
/*       public GameObject bigStar;
     public GameObject bigStarWhite;*/

    AudioSource AudioAnswer;
    
    void Start(){


        if(PlayerPrefs.GetInt("mute")==1)
        song.Pause();

        Player= PlayerPrefs.GetString("Player");
        PlayeName.text=PlayeName.text+PlayerPrefs.GetString("name:"+Player);

         x=PlayerPrefs.GetInt("x:");
         num.text = x.ToString();

        girl = girl.GetComponent<Animator>();
        boy = boy.GetComponent<Animator>();

        AudioAnswer = GetComponent<AudioSource>();
        eraser = eraser.GetComponent<Animation>();

        if(PlayerPrefs.GetString("sex"+Player)=="femme")
        img.gameObject.GetComponent<Image>().sprite=girlIcon;

        if(PlayerPrefs.GetInt("Player"+Player+"scoreToatal")==0 & PlayerPrefs.GetInt("Player"+Player+"lev"+levelIndex+"x"+x+"score")==0){
            instructionsImg.gameObject.SetActive(true);
            instructionsCanvas.gameObject.SetActive(true);
            card.gameObject.SetActive(true);
             result.text="0";
        }
    }


    void Update(){
        if (result.text == "?"){
            is_false = false;
            is_true = false;
            is_blocked = false;
            StopAllCoroutines();
             eraser.Stop();}



if(PlayerPrefs.GetString("sex"+Player)=="femme"){
      Girl.SetActive(true);
      girl.SetBool("isTrue", is_true);
      girl.SetBool("isFalse", is_false);

  }

  if(PlayerPrefs.GetString("sex"+Player)=="homme"){
       Boy.SetActive(true);
       boy.SetBool("isTrue", is_true);
       boy.SetBool("isFalse", is_false);
}
        
         
    }

    
    public void saisir(string number)
    {
        z =x*y;

        if (result.text == "?"){
            result.text =number;
            StartCoroutine(TestCoroutine());
            }
           
        
        else{


            if(is_blocked==false){
             result.text=result.text + number;
             is_blocked=true;
             StartCoroutine(TestCoroutine());
             
            }   
             
             if (is_blocked & has_help)
                  {

                  helpme(number);
                 } 


        }
        
    }



    public void delete()
    {
       if (is_true == false & has_help == false)
            result.text = "?";
    }


    private void helpme(string number)
    {
        if(help.text[0]==number[0]){ result.text=number; }
        if(help.text.Length==2)
        {
        if(help.text[1]==number[0]){ result.text=result.text+number; }
        }
        if(result.text == help.text)
        {StartCoroutine(TestCoroutine());
         help.gameObject.SetActive(false);
        }
    }




    void CheckAnswer(){
            
     if((result.text==z.ToString() | result.text=="0"+z.ToString() ) & is_true == false )
        
        {

             if(essaieNumber<2 & y != 0){
                score++;
             }

            y--;
           
            essaieNumber=0;
         
            if(y== -1){
                

                is_finished = true;
                
            }

         scoreText.text=score.ToString();
         
        Debug.Log("bravo");
        AudioAnswer.clip = winAudio;
        AudioAnswer.PlayOneShot(AudioAnswer.clip);

         is_true = true;
         is_blocked = true;
          has_help = false;
         StartCoroutine(wait1sec());
            
        }

     else{
            if(is_true == false & is_false==false & result.text != "?"){
                is_false = true;
                checkEssaieNumber();
               }
        }

    }




    IEnumerator TestCoroutine()
    {

         if(result.text==z.ToString() | result.text=="0"+z.ToString() ){
        yield return new WaitForSeconds(0);
        CheckAnswer();
        }

        else {
        yield return new WaitForSeconds(2);
        CheckAnswer();
         }
       
    }




    IEnumerator wait1sec()
    {
        yield return new WaitForSeconds(1);
        result.text="?";
        storing();
       
        if(is_finished)
    {
        levelUp();
    }
       
    }

public void storing(){
       if(score >  PlayerPrefs.GetInt("Player"+Player+"lev"+levelIndex+"x"+x+"score"))
 { 
      
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

    



    public void checkEssaieNumber(){

        if(essaieNumber<1){
        
         Debug.Log("ressayer");
         eraser.Play();
        }

        if(essaieNumber==1){

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

public void BackButton()
{
    SceneManager.LoadScene("map");

}


public void levelUp(){


 

     panel.gameObject.SetActive(true);
     song.Pause();
           if(score > 8){
   ShowMsgResult.text="ﺖﻨﺴﺣأ";
    AudioAnswer.PlayOneShot(winner);}
   else
   {
    ShowMsgResult.text="ﺔﻴﻧﺎﺛ لوﺎﺣ";
     AudioAnswer.PlayOneShot(loser);}


   finalScore.text=score.ToString() +"  : ﺔﺠﻴﺘﻨﻟا";
     //ShowStar();
}


/*    public void ShowStar(){
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

    }*/

public void Homebutton()
{
    SceneManager.LoadScene("mainMenu");

}


public void restartbutton()
{
SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}

  
    
}