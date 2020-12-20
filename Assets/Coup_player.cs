using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coup_player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private string card1, card2;
    [SerializeField]
    private int currency;
    [SerializeField]
    private bool isAlive;

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
        set { isAlive = value;  }
    }
}
