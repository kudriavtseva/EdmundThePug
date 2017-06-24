using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPug : MonoBehaviour
{
    public float speed = 1;
    Rigidbody2D myBody = null;
	Transform heroParent = null;
    // Use this for initialization
    bool isGrounded = false;
    bool JumpActive = false;
    float JumpTime = 0f;
    public float MaxJumpTime = 2f;
    public float JumpSpeed = 2f;
	
    void Start()
    {
		this.heroParent = this.transform.parent;
        myBody = this.GetComponent<Rigidbody2D>();
		LevelController.current.setStartPosition (transform.position);
    }

    // Update is called once per frame
    void Update()
    {
		
		
    }

    void FixedUpdate()
    {
		float value = Input.GetAxis ("Horizontal");
		
		Animator animator = GetComponent<Animator> ();
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		Vector3 from = transform.position + Vector3.up * 0.6f;
		Vector3 to = transform.position + Vector3.down * 0.3f;
		int layer_id = 1 << LayerMask.NameToLayer ("Ground");
		RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
		
		//Debug.Log(layer_id, this);
		//Debug.Log(value, this);
		Debug.DrawLine (from, to, Color.red);
		
		if(value != 0) {
			animator.SetBool ("run", true);
			Vector2 vel = myBody.velocity;
			vel.x = value * speed;
			myBody.velocity = vel;
		} else {
			animator.SetBool ("run", false);
		}
	
		if(value < 0) {
			sr.flipX = true;
		} else if(value > 0) {
			sr.flipX = false;
		}
		
		if(this.isGrounded) {
			animator.SetBool ("jump", false);
		} else {
			animator.SetBool ("jump", true);
		}

		if(hit) {
			Debug.Log("Cyka blyat", this);
			isGrounded = true;
			if(hit.transform != null
			&& hit.transform.GetComponent<MovingPlatform>() != null){
				//Приліпаємо до платформи
				SetNewParent(this.transform, hit.transform);
			}
		} else {
			isGrounded = false;
		}
		
		if(Input.GetButtonDown("Jump") && !this.JumpActive) {
			this.JumpActive = true;
			animator.SetBool ("jump", true);
		}
		if(this.JumpActive) {
			//Якщо кнопку ще тримають
			if(Input.GetButton("Jump")) {
				this.JumpTime += Time.deltaTime;
				if (this.JumpTime < this.MaxJumpTime) {
					Vector2 vel = myBody.velocity;
					vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
					myBody.velocity = vel;
				}
			} else {
				this.JumpActive = false;
				this.JumpTime = 0;
				animator.SetBool ("jump", false);
			}
		}
    }
	
	static void SetNewParent(Transform obj, Transform new_parent) {
		if(obj.transform.parent != new_parent) {
			//Засікаємо позицію у Глобальних координатах
			Vector3 pos = obj.transform.position;
			//Встановлюємо нового батька
			obj.transform.parent = new_parent;
			//Після зміни батька координати кролика зміняться
			//Оскільки вони тепер відносно іншого об’єкта
			//повертаємо кролика в ті самі глобальні координати
			obj.transform.position = pos;
		}
	}
}