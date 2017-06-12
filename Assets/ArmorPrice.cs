using UnityEngine;
using UnityEngine.UI;
public class ArmorPrice : MonoBehaviour
{
    public static int armor;
    public PriceTotal total;
    public Text armorText;
    // Use this for initialization
    void Start()
    {
        ArmorPrice.armor = 0;
    }
    public void addItem()
    {
        if (armor <= 5000)
        {
            armor += 500;
            total.total += 500;
        }
    }
    // Update is called once per frame
    void Update()
    {
        armorText.text = "Price: " + armor;
    }
}

