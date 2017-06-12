using UnityEngine;
using UnityEngine.UI;
public class WeaponsPrice : MonoBehaviour
{
    public static int weapons;
    public PriceTotal total;
    public Text weaponsText;
    // Use this for initialization
    void Start()
    {
        WeaponsPrice.weapons = 0;
    }
    public void addItem()
    {
        if (weapons <= 5000)
        {
            weapons += 300;
            total.total += 300;
        }
    }
    // Update is called once per frame
    void Update()
    {
        weaponsText.text = "Price: " + weapons;
    }
}
