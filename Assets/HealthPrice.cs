using UnityEngine;
using UnityEngine.UI;
public class HealthPrice : MonoBehaviour
{
    public static int health;
    public PriceTotal total;
    public Text healthText;
    // Use this for initialization
    void Start()
    {
        HealthPrice.health = 0;
    }
    public void addItem()
    {
        if (health <= 5000)
        {
            health += 100;
            total.total += 100;
        }
    }
    // Update is called once per frame
    void Update()
    {
        healthText.text = "Price: " + health;
    }
}
