using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{

    [SerializeField]
    private string card1, card2;
    [SerializeField]
    private int currency;
    [SerializeField]
    private bool isAlive;
    private bool isTurn;
    private string curse_applied;
    private string curse_hand;

    public string Card1
    {
        get { return card1; }
        set { card1 = value; }
    }

    public string Card2
    {
        get { return card2; }
        set { card2 = value; }
    }

    public int Currency
    {
        get { return currency; }
        set { currency = value; }
    }

    public bool IsAlive
    {
        get { return isAlive; }
        set { isAlive = value; }
    }

    public string Curse_applied
    {
        get { return curse_applied; }
        set { curse_applied = value; }
    }

    public string Curse_hand
    {
        get { return curse_hand; }
        set { curse_hand = value; }
    }

    public bool IsTurn
    {
        get { return isTurn; }
        set { isTurn = value; }
    }

    public Players()
    {
        Card1 = null;
        Card2 = null;
        Currency = 0;
        IsAlive = false;
        curse_hand = "";
        curse_applied = null;
        IsTurn = false;
    }
}
