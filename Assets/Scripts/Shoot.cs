using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public GameObject shoot;
	public GameObject player;
	private int timer;

	void Start () {
		Random.seed = (int)(transform.position.x*100);
		int n = Random.Range (0, 50);
		timer = 100 + n;
	}

	void Update () {
		if (timer > 90) {
			timer--;
		} else if (timer == 90) {
			shoot.SetActive (true);
			shoot.transform.position = transform.position;
			Quaternion rotation = Quaternion.LookRotation (player.transform.position - transform.position, transform.TransformDirection (Vector3.forward));
			shoot.transform.rotation = new Quaternion (0, 0, rotation.z, rotation.w);
//			shoot.transform.localScale = new Vector3 (0.1f, 0.01f, 1);
			timer--;
		} else if (timer > 0) {
			shoot.transform.position = transform.position;
			Quaternion rotation = Quaternion.LookRotation (player.transform.position - transform.position, transform.TransformDirection (Vector3.forward));
			shoot.transform.rotation = new Quaternion (0, 0, rotation.z, rotation.w);
//			shoot.transform.localScale += new Vector3 (0.001f, 0.001f, 0);
			timer--;
		} else if (timer == 0) {
			float vX = player.transform.position.x - shoot.transform.position.x;
			float vY = player.transform.position.y - shoot.transform.position.y;
			Vector2 movement = new Vector2 (vX, vY);
			movement.Normalize ();
			shoot.GetComponent<Rigidbody2D> ().velocity = movement;
			timer--;
		} else if (!shoot.activeSelf) {
			Random.seed = (int)(transform.position.x * 100);
			int n = Random.Range (0, 50);
			timer = 100 + n;
		} else if (timer == -180) {
			shoot.SetActive (false);
		} else if (timer < 0) {
			timer--;
		}
	}
}
