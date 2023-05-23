using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardController4 : MonoBehaviour
{
    public Sprite number0;
    public Sprite number1;
    public Sprite number2;
    public Sprite number3;
    public Sprite number4;
    public Sprite number5;
    public Sprite number6;
    public Sprite number7;
    public Sprite number8;
    public Sprite number9;
    public Sprite number10;
    public Clavier4 clav;
    private SpriteRenderer cardRender;
    // Start is called before the first frame update
    void Start()
    {
        clav = FindObjectOfType<Clavier4>();
        cardRender = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (clav.is_true == false)
        {
            if (clav.y == 0)
                cardRender.sprite = number0;
            if (clav.y == 1)
                cardRender.sprite = number1;
            if (clav.y == 2)
                cardRender.sprite = number2;
            if (clav.y == 3)
                cardRender.sprite = number3;
            if (clav.y == 4)
                cardRender.sprite = number4;
            if (clav.y == 5)
                cardRender.sprite = number5;
            if (clav.y == 6)
                cardRender.sprite = number6;
            if (clav.y == 7)
                cardRender.sprite = number7;
            if (clav.y == 8)
                cardRender.sprite = number8;
            if (clav.y == 9)
                cardRender.sprite = number9;
            if (clav.y == 10)
                cardRender.sprite = number10;
        }

    }
}
