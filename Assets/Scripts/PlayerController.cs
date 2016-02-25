using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	private int speed;
	private Rigidbody2D rb2d;
	private int count;
	public Text countText;
	private int hp;
	private int at;
	private int def;
	public Text hpText;


	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		count = 0;
		hp = 80;
		at = 100;
		def = 10;
		speed = 2;
		SetCountText ();
		SethpText ();

	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		rb2d.AddForce (movement * speed);
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.CompareTag("Zombie"))
		{
			other.GetComponent<Zombie> ().Attack (true);
			other.GetComponent<Zombie> ().takeDmg (at);
		}

		if (other.gameObject.CompareTag("Claw"))
		{
			other.gameObject.SetActive(false);
			hp = hp - 200 / def;
			SethpText ();
		}

		if (other.gameObject.CompareTag("Coin"))
		{
			other.gameObject.SetActive(false);
			count = count + 100;
			SetCountText ();
		}

		if (other.gameObject.CompareTag("Carrot"))
		{
			other.gameObject.SetActive(false);
			if (hp < 100) 
			{
				hp = hp + 10;
			}
			SethpText ();
		}

		if (other.gameObject.CompareTag("Weapon"))
		{
			other.gameObject.SetActive(false);
			at = at + 100;
			SethpText ();
		}

		if (other.gameObject.CompareTag("Shield"))
		{
			other.gameObject.SetActive(false);
			def = def + 10;
			SethpText ();
		}

		if (other.gameObject.CompareTag("Shoe"))
		{
			other.gameObject.SetActive(false);
			speed = speed + 2;
			SethpText ();
		}


	}

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString ();
	}

	void SethpText()
	{
		hpText.text = "HP: " + hp.ToString () + "  " + "AT: " + at.ToString () + "  " + "DEF: " + def.ToString () + "  " + "SPEED:" + speed.ToString ();
	}



}
