using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour {

    private TextMeshProUGUI healthText;
    private Player player;
    private int tmpHealth;

    // Use this for initialization
    void Start () {
        healthText = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
    }
	
	// Update is called once per frame
	void Update () {
        tmpHealth = player.GetHealth();
        //healthText.text = (tmpHealth < 0) ? "0" : player.GetHealth().ToString();
        healthText.text = Mathf.Clamp(player.GetHealth(), 0, 200).ToString();
    }
}
