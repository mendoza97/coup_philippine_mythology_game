using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coup : MonoBehaviour
{

    public string theName;
    public GameObject inputField_no_of_players;
    public GameObject textDisplay;
    public GameObject textDisplay_deck;
    public GameObject textDisplay_deck_curse;
    public GameObject textDisplay_game_status;
    public GameObject button_harvest;
    public GameObject button_ritual;
    public GameObject button_curse;
    public GameObject button_sacrifice;
    public GameObject button_devour;
    public GameObject button_steal;
    public GameObject button_clairvoyance;
    public GameObject inputField_phase1;
    public GameObject button_card1;
    public GameObject button_card2;

    public GameObject button_op1;
    public GameObject button_op2;
    public GameObject button_op3;
    public GameObject button_op4;
    public GameObject inputField_phase2;
    public GameObject button_apply_phase2;

    const int MAX_COUNT_PLAYER = 10;
    public static int count = 0;
    [SerializeField]
    public static Players[] player_obj = new Players[] { new Players(), new Players(), new Players(), new Players(), new Players(), new Players(), new Players(), new Players(), new Players(), new Players() };

    public int kill_flag = -1;
    public static string[] copies = new string[] { "1", "2", "3", "4", "5" };
    public static string[] monster = new string[] { "diwata", "manananggal", "tambal", "kapre", "santilmo", "mangkukulam" };
    public static string[] curse = new string[] { "black", "green", "white", "red" };
    public static int no_of_players ;
    public static int deck_index=0;
    public static string output_text_display = "";
    public static int player_turn;
    public static int player_deciding;

    public List<string> deck_monster;
    public List<string> deck_curse;
    public List<int> player_decisions;
    public static int contest_mode=-1;



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("PROGRAM START!");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayCards()
    {
        //Get number of players
        theName = inputField_no_of_players.GetComponent<Text>().text;
        no_of_players = int.Parse(theName);
        //Generate benjie logic deck monster
        deck_monster = benjieDeck_monster(no_of_players);
        shuffle(deck_monster);
        //Genereate curse
        deck_curse = benjieDeck_curse(no_of_players);
        shuffle(deck_curse);
        coup_initialization(no_of_players);
        int start = Random.Range(1, no_of_players);
        player_obj[start].IsTurn = true;
    }

    public void coup_initialization(int no_of_players)
    {
        //deck_index = 0;
        Debug.Log("Entered coup initialization");
        //Debug.Log(no_of_players);
        for (int i=0; i < no_of_players; i++)
        {
            player_obj[i].Currency = 2;
            player_obj[i].Card1 = deck_monster[0];
            deck_monster.RemoveAt(0);
            player_obj[i].Card2 = deck_monster[0];
            deck_monster.RemoveAt(0);
            player_obj[i].IsAlive = true;
            player_obj[i].Curse_hand = "";
            player_obj[i].Curse_applied = "";
            player_obj[i].IsTurn = false;
        }
        player_turn = Random.Range(0, no_of_players);
        player_obj[player_turn].IsTurn = true;
        display_output_game_status("Player no. " + player_turn.ToString() +" turn." );
        display_output_text_player_status();
        display_output_deck_monster_status();
        display_output_deck_curse_status();
        phase1_button_control(phase1_moveset_available_analyzer(player_obj[player_turn]));
        choose_discard_button_control(-1);
    }

    public static List<string> benjieDeck_monster(int no_of_players)
    {
        //"set" counting logic
        //no_of_players = 10; //FOR TESTING PURPOSES ONLY. kapag may input na sa main game, tanggalin itong var na ito.
        int no_of_hand = no_of_players * 2;
        int set = 2;
        do
        {
            if (((6 * (set)) - (no_of_hand)) < 6)
                set++;
            else
                break;
        } while (true);
        //end "set" counting logic
        List<string> newDeck = new List<string>();

        int count_copies = 1;
        foreach (string s in copies)
        {
            if (count_copies <= set)
            {
                foreach (string v in monster)
                {
                    //newDeck.Add(s + v);   s for Debugging purposes. s represent set number.
                    newDeck.Add(v);
                }
                count_copies++;
            }
            else
                break;
        }
        //For debugging purposes
        //print("Number of set: ");
        //print(set);
        return newDeck;
    }
    public static List<string> benjieDeck_curse(int no_of_players)
    {
        //"set" counting logic
        int set = 2;
        for (int i = 0; ; i += 3)
        {
            if (no_of_players > (3 + i)) set++;
            else break;
        }
        //end "set" counting logic
        List<string> newDeck = new List<string>();

        int count_copies = 1;
        foreach (string s in copies)
        {
            if (count_copies <= set)
            {
                foreach (string v in curse)
                {
                    newDeck.Add(v);
                }
                count_copies++;
            }
            else
                break;
        }
        return newDeck;
    }

    void shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    public void trigger_noPlayers()
    {
        //theName = inputField_no_of_players.GetComponent<Text>().text;
        //textDisplay.GetComponent<Text>().text = theName;
        PlayCards();

    }
    public void display_output_text_player_status()
    {
        output_text_display = "";
        for (int i = 0; i < no_of_players; i++)
        {
            output_text_display += "Player no. " + i.ToString()+"\tIs Alive: "+ player_obj[i].IsAlive.ToString() + "\nCurrency: " + player_obj[i].Currency.ToString() + "\nCards: \t" + player_obj[i].Card1.ToString() + "   "+ player_obj[i].Card2.ToString()+"\nCurse hand: "+ player_obj[i].Curse_hand+" \tCurse appl: "+ player_obj[i].Curse_applied +"\n\n";
        }
        //Debug.Log(output_text_display);
        textDisplay.GetComponent<Text>().text = output_text_display;
    }

    public void display_output_deck_monster_status()
    {
        string temporary = "Deck status:\n\n";
        foreach (string card in deck_monster)
        {
            temporary += (card + "\n");
        }
        textDisplay_deck.GetComponent<Text>().text = temporary;
    }

    public void display_output_deck_curse_status()
    {   
        string temporary = "Deck status:\n\n";
        foreach (string card in deck_curse)
        {
            temporary += (card + "\n");
        }
        textDisplay_deck_curse.GetComponent<Text>().text = temporary;
    }

    public void display_output_game_status(string message = "")
    {
        textDisplay_game_status.GetComponent<Text>().text = message;
    }

    public static List<bool> phase1_moveset_available_analyzer(Players player)
    {
        //A list that will generate 4 boolean values of their current state to the player: [harvest, ritual, curse, sacrifice]
        List<bool> phase1 = new List<bool>();

        //HARVEST
        phase1.Add(true);

        //RITUAL
        if ((player.Currency >= 1) && (player.Curse_hand.Equals(""))) {
            //Will enter if player currency is greater than 1, and not holding any other curse.
            phase1.Add(true);
        }
        else
            phase1.Add(false);

        //CURSE
        if (player.Curse_hand.Equals("") == false)
            phase1.Add(true);
        else
            phase1.Add(false);

        //SACRIFICE
        if (player.Currency >= 7)
            phase1.Add(true);
        else
            phase1.Add(false);

        //DEVOUR
        if (player.Currency >= 3)
            phase1.Add(true);
        else
            phase1.Add(false);

        //STEAL
        phase1.Add(true);


        //CLAIRVOYANCE
        phase1.Add(true);

        return phase1;
    }

    public void phase1_button_control(List<bool> status, int code=0)
    {
        if (code == 0)
        {
            //REQUIRES 7 LIST BOOL VALUES OF VARIABLE "status" !!
            button_harvest.SetActive(status[0]);
            button_ritual.SetActive(status[1]);
            button_curse.SetActive(status[2]);
            button_sacrifice.SetActive(status[3]);
            button_devour.SetActive(status[4]);
            button_steal.SetActive(status[5]);
            button_clairvoyance.SetActive(status[6]);
        }
        else if (code == -1)
        {
            button_harvest.SetActive(false);
            button_ritual.SetActive(false);
            button_curse.SetActive(false);
            button_sacrifice.SetActive(false);
            button_devour.SetActive(false);
            button_steal.SetActive(false);
            button_clairvoyance.SetActive(false);
        }
    }

    public void phase2_button_control(List<string> btn_names, List<bool> states)
    {
        button_op1.SetActive(states[0]);
        button_op1.GetComponentInChildren<Text>().text = btn_names[0];
        button_op2.SetActive(states[1]);
        button_op2.GetComponentInChildren<Text>().text = btn_names[1];
        button_op3.SetActive(states[2]);
        button_op3.GetComponentInChildren<Text>().text = btn_names[2];
        button_op4.SetActive(states[3]);
        button_op4.GetComponentInChildren<Text>().text = btn_names[3];
    }

    public void choose_discard_button_control(int code=0)
    {
        bool state=false;
        if (code == 0)
            state = true;
        button_card1.SetActive(state);
        button_card2.SetActive(state);
    }

    public void end_turn(int player_number, int no_of_players)
    {
        show_game_status();
        if (is_there_a_winner() != -1)
        {
            int x = is_there_a_winner();
            //Debug.Log("winner");
            //Debug.Log(x);
            display_output_game_status("Player no. " + x.ToString() + " wins!");
        }
        else
        {
            do
            {
                player_number++;
                if (player_number == no_of_players)
                {
                    player_number = 0;
                }
                if (player_obj[player_number].IsAlive == true)
                {
                    player_turn = player_number;
                    display_output_game_status("Player no. " + player_turn.ToString() + " turn.");

                    //PREPARE THE NEXT PLAYER'S BUTTON
                    phase1_button_control(phase1_moveset_available_analyzer(player_obj[player_turn]));
                    choose_discard_button_control(-1);
                    break;
                }
            } while (true);
        }
    }

    public void show_game_status()
    {
        display_output_text_player_status();
        display_output_deck_monster_status();
        display_output_deck_curse_status();
    }

    public void trigger_harvest()
    {
        player_obj[player_turn].Currency += 1;
        end_turn(player_turn, no_of_players);
    }
    
    public void trigger_ritual()
    {
        player_obj[player_turn].Currency -= 1;
        player_obj[player_turn].Curse_hand = deck_curse[0];
        deck_curse.RemoveAt(0);
        show_game_status();
        end_turn(player_turn, no_of_players);
    }

    public void trigger_sacrifice()
    {
        kill_flag = -1;
        //Will be placing a parameter? maybe. depending on multiplayer functionalities.
        //this if statement is for error checking ng empty input field in our current protoype. Remove later pls!
        string temp_string = inputField_phase1.GetComponent<Text>().text;
        int player_to_be_discarded = int.Parse(temp_string);

        if ((temp_string.Equals("")==false) && (player_obj[player_to_be_discarded].IsAlive == true))
        {   
            //if player to be hand discarded has only one card
            if ((player_obj[player_to_be_discarded].Card1 == "") || (player_obj[player_to_be_discarded].Card2 == ""))
            {
                player_obj[player_to_be_discarded].Currency = 0;
                player_obj[player_to_be_discarded].Card1 = "";
                player_obj[player_to_be_discarded].Card2 = "";
                player_obj[player_to_be_discarded].IsAlive = false;

                end_turn(player_turn, no_of_players);
            }
            else
            {
                List<bool> _ = new List<bool>(); 
                kill_flag = 0;
                phase1_button_control(_, -1);
                choose_discard_button_control();
                display_output_game_status("Choose which card to discard player " + temp_string + ".");
            }
        }
        else
        {
            Debug.LogError("Error in input field. ");
        }
    }

    public void trigger_devour()
    {
        kill_flag = -1;
        //Will be placing a parameter? maybe. depending on multiplayer functionalities.
        //this if statement is for error checking ng empty input field in our current protoype. Remove later pls!
        string temp_string = inputField_phase1.GetComponent<Text>().text;
        int player_to_be_discarded = int.Parse(temp_string);

        if ((temp_string.Equals("") == false) && (player_obj[player_to_be_discarded].IsAlive == true))
        {
            contest_mode = 0;
            initialization_phase2(contest_mode);   
        }
        else
        {
            Debug.LogError("Error in input field. ");
        }
    }

    public void trigger_discard1()
    {
        discard_phase(1);
    }

    public void trigger_discard2()
    {
        discard_phase(2);
    }

    public void discard_phase(int card)
    {
        string temp_string = inputField_phase1.GetComponent<Text>().text;
        int player_to_be_discarded = int.Parse(temp_string);
        //kill_flag = 0 means sacrifice
        if (kill_flag == 0)
        {
            player_obj[player_turn].Currency -= 7; 
            if (card == 1)
            {
                player_obj[player_to_be_discarded].Card1 = "";
            }
            else if (card == 2)
            {
                player_obj[player_to_be_discarded].Card2 = "";
            }
            else
            {
                Debug.Log("discard what now?");
            }
            end_turn(player_turn, no_of_players);
        }
    }

    public int is_there_a_winner()
    {
        int count_alive = 0;
        int player_winner = -1;
        for(int i=0; i < no_of_players; i++)
        {
            if (player_obj[i].IsAlive == true) 
            {
                player_winner = i;
                count_alive += 1;
            }
        }
        if (count_alive > 1) return -1;
        else return player_winner;
    }

    public void initialization_phase2(int code = -1)
    {
        string temp_string = inputField_phase1.GetComponent<Text>().text;
        int player_to_be_discarded = int.Parse(temp_string);

        List<bool> _ = new List<bool>();
        //player_deciding = 0;
        phase1_button_control(_, -1);
        choose_discard_button_control(-1);

        for(int i = 0; i < no_of_players; i++)
        {
            player_decisions.Add(-1);
        }

        if(code == 0)
        {
            display_output_game_status("Player no." + player_turn.ToString() + " attempted to assassinate player no." + player_to_be_discarded.ToString());
            List<string> button_names = new List<string>() { "Allow", "Block with santilmo", "challenge", "" };
            List<bool> states_of_options = new List<bool>() { true, true, true, false };

            phase2_button_control(button_names, states_of_options);
        }

    }


    public void trigger_apply_phase2_inputField()
    {
        //GET PLAYER WHO DECIDED
        string temp_string = inputField_phase2.GetComponent<Text>().text;
        player_deciding = int.Parse(temp_string);
        if(player_deciding == player_turn)
        {
            //display_output_game_status("You attempted to devour " + inputField_phase1.Text());
        }
    }

    public void trigger_option1()
    {

    }

    public void trigger_option2()
    {

    }

    public void trigger_option3()
    {

    }

    public void trigger_option4()
    {

    }
}
