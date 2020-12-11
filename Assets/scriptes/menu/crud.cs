using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class crud : MonoBehaviour
{
    // namePlace is the panel where we will get the informations of the player
    // item gameobject is like a model we will use it to do some copies of it making a list of this item
    //itemParent is the parent of the items that we will create using the item gameobject 
	public GameObject itemParent, item , namePlace;
    //in this text we will show a message when the input name is not valid
	public Text  msg;
     //in this text we will show a message when the sex is empty
    public Text  msg2;

    // in this list we will stock the names of all the players
	 List<string> names = new List<string>();
     // to calcul the number of players
	   public int calcul;
       public Sprite girlIcon;
    public Image homme;
    public Image femme;
    public Sprite check;
    public Sprite empty;
    // Start is called before the first frame update
    void Start()
    {
        read();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void read()
    {
        //PlayerPrefs used to Store and accesses player preferences between game sessions. 
    	int count = PlayerPrefs.GetInt("count");



    	for(int i=0 ; i<= count ; i++){
            
             string id = PlayerPrefs.GetString("id:"+ i);
             if(id!="")
              calcul++;
         }



         
    	for(int i=0 ; i< itemParent.transform.childCount ; i++){
    		Destroy(itemParent.transform.GetChild(i).gameObject);
    	}


    	names = new List<string>(new string[calcul]);
    	int number = 0;
    	for(int i=0 ; i<= count ; i++){
    		number++;
    		string id = PlayerPrefs.GetString("id:"+ i);
    		string name = PlayerPrefs.GetString("name:"+ i);

    	if(id!=""){
            // here we create a new item
    		GameObject tmp_item = Instantiate (item , itemParent.transform);
            // the item name will be the value of 'i' like the id value 
    		tmp_item.name= i.ToString();
            if(PlayerPrefs.GetString("sex"+i)=="femme")
    		tmp_item.transform.GetChild(0).GetComponent<Image>().sprite=girlIcon;
    		tmp_item.transform.GetChild(1).GetComponent<Text>().text=name;
    		names[number-1]=name;

    	}
    	else { number --;}

    }
    calcul=0;
}



// this function will call when we save a new player
    public void stockname(){
    	int count = PlayerPrefs.GetInt("count");
    	if(namePlace.transform.GetChild(1).GetChild(0).GetComponent<Text>().text=="")
    	{
            //this is the case where the name is empty
    		msg.GetComponent<ArabicText>().Text="* أدخل إسمك";

    	}
    	else if(names.Contains(namePlace.transform.GetChild(1).GetChild(0).GetComponent<Text>().text))
    	{
             //this is the case where the name is already exist
            msg.GetComponent<ArabicText>().Text="*هذا الإسم موجود";}

        else if(PlayerPrefs.GetString("sex")=="")
        {
             //this is the case where the sex is not selected
         
         msg2.GetComponent<ArabicText>().Text="*ما جنسك؟";
         msg.GetComponent<ArabicText>().Text="";
        }
    	else{

    		count++;
            //here we will set the variables of the new player(storing player informations) 
    		PlayerPrefs.SetString("id:"+count, count.ToString());
             PlayerPrefs.SetString("name:"+count,namePlace.transform.GetChild(1).GetChild(0).GetComponent<Text>().text);
             PlayerPrefs.SetString("sex"+count,PlayerPrefs.GetString("sex"));
             PlayerPrefs.SetInt("count",count);
             PlayerPrefs.SetString("Player",count.ToString());
             read();
    
            namePlace.SetActive(false);
            cancel();
        }
}

//this function will call when we select a player 
 public void selectplayer(GameObject Item){
	string id_choice = Item.name;
	Debug.Log(PlayerPrefs.GetString("name:"+id_choice));
	PlayerPrefs.SetString("Player",id_choice);
	

}

public void delete( GameObject Item){

	string id_choice = Item.name;
	if(PlayerPrefs.GetString("Player")==PlayerPrefs.GetString("id:"+id_choice))
	PlayerPrefs.DeleteKey("Player");

	PlayerPrefs.DeleteKey("id:"+id_choice);
	PlayerPrefs.DeleteKey("name:"+id_choice);
	

	read();


	if(names.Count==0){
        //in this case the list of the players is empty
	PlayerPrefs.DeleteKey("vide");
}
	else
    PlayerPrefs.SetInt("vide",1);



}


// this function will call when the player select his sex
public void getGender(string gender){

    PlayerPrefs.SetString("sex",gender);
    if(PlayerPrefs.GetString("sex")=="homme")
        {
            homme.sprite = check;
            femme.sprite = empty;
        }
    else
        {
            homme.sprite = empty;
            femme.sprite = check;
        }
}

    public void cancel()
    {
        namePlace.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "";
        PlayerPrefs.DeleteKey("sex");
        homme.sprite = empty;
        femme.sprite = empty;
        msg2.GetComponent<ArabicText>().Text = "";
        msg.GetComponent<ArabicText>().Text = "";

    }


}
