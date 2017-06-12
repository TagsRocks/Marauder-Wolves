using UnityEngine;
using System.Collections;

public class health : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
		
    }

   void OnTriggerEnter(Collider Grunt_crawler) 
	{
		
		if (Grunt_crawler.gameObject.CompareTag ("Enemy"))
		{
			
			Grunt_crawler.gameObject.SetActive (false);
		}
	}
}