using UnityEngine;
using UnityEngine.UI;
public class PriceTotal : MonoBehaviour {

    // Use this for initialization
    public int total;
    private ItemPrice item;
    private HealthPrice health;
    Text text;
    void Awake () {
        text = GetComponent<Text>();
        total = 0;
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "Total: " + total;
	}
}
