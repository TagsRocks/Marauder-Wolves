using UnityEngine;
using UnityEngine.UI;
public class ItemPrice : MonoBehaviour {
    public static int item;
    public PriceTotal total;
    public Text itemText;
	// Use this for initialization
	void Start () {
        ItemPrice.item = 0;
	}
	public void addItem()
    {
        if (item <= 5000) {
            item += 100;
            total.total += 100;
        }
    }
	// Update is called once per frame
	void Update () {
        itemText.text = "Price: " + item;
    }
}
