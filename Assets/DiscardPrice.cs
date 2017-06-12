using UnityEngine;
using UnityEngine.UI;
public class DiscardPrice : MonoBehaviour {

    public static int discard;
    public PriceTotal total;
    public Text discardText;
    // Use this for initialization
    void Start()
    {
        DiscardPrice.discard = 0;
    }
    public void addItem()
    {
        if (discard >= 0)
        {
            discard = 0;
            total.total = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        discardText.text = "Price: " + discard;
    }
}
