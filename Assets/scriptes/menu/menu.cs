using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class menu : MonoBehaviour
{

    public GameObject panel;
    public GameObject addpanel;
    public GameObject sound;
    public GameObject music;

     public bool is_muted;
     public bool no_music;

     public Sprite nosound;
     public Sprite nomusic;
     public Sprite activesound;
     public Sprite activemusic;
    // Start is called before the first frame update
    void Start()
    {
        // we will use this variable to hide the panel of the numbers of tables when we passed from the mainMenu to the map
        PlayerPrefs.SetInt("hide",1);

        is_muted = AudioListener.pause;
    }

    // Update is called once per frame
    void Update()
    {
        //here we change the sprite of buttons depending to its status
        if(is_muted)
        sound.gameObject.GetComponent<Image>().sprite=nosound;
        else
        sound.gameObject.GetComponent<Image>().sprite=activesound;


        if(PlayerPrefs.GetInt("mute")==1){

           no_music=true;
            music.gameObject.GetComponent<Image>().sprite=nomusic;


        }
        else{
            no_music = false;
            music.gameObject.GetComponent<Image>().sprite=activemusic;
        }

    }

    public void pressStart(){


        if(PlayerPrefs.GetString("Player")==""){
            //in this case no player is selected
            panel.SetActive(true);
        if(PlayerPrefs.GetInt("vide")==0){
            //in this case the list of players is empty
            addpanel.SetActive(true);
        }

        }
        else
    	SceneManager.LoadScene("map");

    }



  public void QuitGame () {
 Application.Quit ();

 }


    
    public void Soundbutton()
  {
     is_muted = !is_muted;
     AudioListener.pause = is_muted; 
   }
 

    public void musicbutton()
  {
     no_music = !no_music;
      if(no_music){

            PlayerPrefs.SetInt("mute",1);
 }
        else{
            PlayerPrefs.SetInt("mute",0);
            
        }

   }
}
