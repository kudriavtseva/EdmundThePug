using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPug : MonoBehaviour
{
    public float health = 1;
    public float speed = 1;
    Rigidbody2D myBody = null;
	Transform heroParent = null;
    // Use this for initialization
    public bool isGrounded = false;
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


    public void removeHealth(int number)
    {

        this.health -= number;
        if (this.health < 0)
        {
            this.health = 0;
        }
        this.onHealthChange();

    }
    public void resetHealth()
    {
        this.health = 1;
        this.onHealthChange();
    }
    void onHealthChange()
    {
		UILabel health_count = GameObject.Find ("health_count").GetComponent<UILabel> ();

        if (this.health == 1)
        {
			health_count.text = "2/3";
            this.transform.localScale = Vector3.one;
        }
        else if (this.health == 2)
        {
			health_count.text = "3/3";
            this.transform.localScale = Vector3.one * 1.2f;
        }
        else if (this.health == 0)
        {
			health_count.text = "1/3";
            LevelController.current.onPugDeath(this);

        }
    }


    void FixedUpdate()
    {
		float value = Input.GetAxis ("Horizontal");
        if (Mathf.Abs(value) > 0)
        {
            Vector2 vel = myBody.velocity;
            vel.x = value * speed;
            myBody.velocity = vel;
        }
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (value < 0)
        {
            sr.flipX = true;
        }
        else if (value > 0)
        {
            sr.flipX = false;
        }
        Animator animator = GetComponent<Animator> ();
        if (this.isGrounded)
        {
            animator.SetBool("jump", false);
        }
        else
        {
            animator.SetBool("jump", true);
        }
        if (Mathf.Abs(value) > 0)
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }

        Vector3 from = transform.position + Vector3.up * 2.0f;
        Vector3 to = transform.position + Vector3.down * 2.5f;
        int layer_id = 1 << LayerMask.NameToLayer("Ground");
        //Перевіряємо чи проходить лінія через Collider з шаром Ground
        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
        if (hit)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        Debug.DrawLine (from, to, Color.red);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            this.JumpActive = true;
        }
        if (this.JumpActive)
        {
            //Якщо кнопку ще тримають
            if (Input.GetButton("Jump"))
            {
                this.JumpTime += Time.deltaTime;
                if (this.JumpTime < this.MaxJumpTime)
                {
                    Vector2 vel = myBody.velocity;
                    vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
                    myBody.velocity = vel;
                }
            }
            else
            {
                this.JumpActive = false;
                this.JumpTime = 0;
                animator.SetBool("jump", false);
            }
        }




		if(hit) {

			isGrounded = true;
			if(hit.transform != null
			&& hit.transform.GetComponent<MovingPlatform>() != null){
				//Приліпаємо до платформи
				SetNewParent(this.transform, hit.transform);
			}
		}
        else
        {
            //Ми в повітрі відліпаємо під платформи
            SetNewParent(this.transform, this.heroParent);
        }
    

        if (Input.GetButtonDown("Jump") && isGrounded) {
			this.JumpActive = true;
			animator.SetBool ("jump", true);
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