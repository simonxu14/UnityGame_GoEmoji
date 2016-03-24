using UnityEngine;
using System.Collections;

public class Golem : MonoBehaviour {

	private Animator animator;
	public Vector2 target;
	private Vector2 start;
	private Vector2 curTarget;
	private bool flag;
	private Rigidbody2D rb2d;
	private Vector2 flip;

	private int health;
	public GameObject claw;
	private int time;
	private bool isDie;

	void Start () {
		animator = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		start.x = transform.position.x;
		start.y = transform.position.y;
		flip = transform.localScale;
		curTarget = target;
		flag = true;
		health = 5000;
		time = 0;
		isDie = false;
	}

	public void Attack(bool temp) {
		if (temp) {
			animator.SetTrigger ("GolemWalkToAttack");
			claw.SetActive (true);
			claw.transform.position = transform.position;
		}
		if (!temp) {
			animator.SetTrigger ("GolemAttackToWalk");
		}
	}

	public void takeDmg(int val){
		if (health > val) {
			health -= val;
		} else {
			Die();
		}
	}

	public void Die() {
		animator.SetTrigger ("GolemDie");
		isDie = true;
		//		gameObject.SetActive(false);
	}

	void Update () {
		if (isDie == true) {
			time += 1;
			if (time == 100) {
				gameObject.SetActive (false);
			}
		}

		animator.SetTrigger ("GolemAttackToWalk");

		if (reach ()) {
			if (flag) {
				curTarget = start;
				flip.x *= -1;
				transform.localScale = flip;
			}
			else {
				curTarget = target;
				flip.x *= -1;
				transform.localScale = flip;
			}
			flag = !flag;
		}
	}

	void FixedUpdate () {
		transform.localRotation = Quaternion.identity;
		float moveH = (curTarget.x - transform.position.x);
		float moveV = (curTarget.y - transform.position.y);
		Vector2 movement = new Vector2 (moveH, moveV);
		movement.Normalize ();
		if(rb2d.velocity.magnitude<0.5)
			rb2d.AddForce (movement);
	}

	bool reach() {
		return (Mathf.Abs(transform.position.x - curTarget.x)<0.1)&&(Mathf.Abs(transform.position.y - curTarget.y)<0.1);
	}
}
