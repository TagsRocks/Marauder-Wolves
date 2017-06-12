using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinExchange : MonoBehaviour {
    //public static int coinexchange;
    public Coin_Score coins;
    public PriceTotal total;
    //public HealthPrice health;
    //public WeaponsPrice weapons;
    //public ArmorPrice armor;
    public Text coinexchangeText;
    public int newTotal;
	// Use this for initialization
	void Start () {
		coins = GameObject.Find ("Coin").GetComponent<Coin_Score> ();
		total = GameObject.Find ("Price").GetComponent<PriceTotal> ();

	}
	public void subCoins()
    {
		newTotal = coins.coins - total.total;
        PlayerPrefs.SetInt("coins", newTotal);
    }
	// Update is called once per frame
	void Update () {
		coinexchangeText.text = "Coins: " + coins.coins;
	}
}
