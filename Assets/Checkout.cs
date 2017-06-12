using UnityEngine;
using UnityEngine.UI;
public class Checkout : MonoBehaviour {

    public static int checkout;
    public PriceTotal total;
    public Text checkoutText;
    // Use this for initialization
    void Start()
    {
        Checkout.checkout = 0;
    }
    public void addItem()
    {
        if (checkout >= 0)
        {
            checkout = 0;
            total.total = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        checkoutText.text = "Price: " + checkout;
    }
}
