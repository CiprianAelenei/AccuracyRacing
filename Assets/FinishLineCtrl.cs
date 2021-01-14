using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineCtrl : MonoBehaviour
{
    public int playerLaps = 0;
    public int opp1Laps = 0;
    public int opp2Laps = 0;
    public GameObject panouWinner;
    public TMPro.TMP_Text Place1;
    public TMPro.TMP_Text Place2;
    public TMPro.TMP_Text Place3;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerLyr"))
        {
            playerLaps++;
        } else if (other.gameObject.layer == LayerMask.NameToLayer("Opp1Lyr"))
        {
            opp1Laps++;
        } else if (other.gameObject.layer == LayerMask.NameToLayer("Opp2Lyr"))
        {
            opp2Laps++;
        }
        if (playerLaps == 3)
        {
            panouWinner.SetActive(true);
            Place1.text = "You";
            if (opp1Laps > opp2Laps)
            {
                Place2.text = "Player1";
                Place3.text = "Player2";
            }
            else
            {
                Place2.text = "Player2";
                Place3.text = "Player1";
            }
            
        }else if (opp1Laps == 3)
        {
            panouWinner.SetActive(true);
            Place1.text = "Player1";
            if (playerLaps >= opp2Laps)
            {
                Place2.text = "You";
                Place3.text = "Player2";
            }
            else
            {
                Place2.text = "Player2";
                Place3.text = "You";
            }
        }
        else if (opp2Laps == 3)
        {
            panouWinner.SetActive(true);
            Place1.text = "Player2";
            if (playerLaps >= opp1Laps)
            {
                Place2.text = "You";
                Place3.text = "Player1";
            }
            else
            {
                Place2.text = "Player1";
                Place3.text = "You";
            } 
        }
    }

}
