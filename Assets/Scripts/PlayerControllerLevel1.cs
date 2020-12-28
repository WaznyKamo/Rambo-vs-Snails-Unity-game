using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerLevel1 : MonoBehaviour
{
	public float moveSpeed = 0.1f;
	public float jumpForce = 6f;
	private Rigidbody2D rigidBody;
	public LayerMask groundLayer;
	public Animator animator;
	public bool isWalking;
	private bool isFacingRight;
	private int score = 0;
	private int threshold = -10;
	private float killOffset = 1.3f;
	private Vector2 startPosition;
    

	void Awake()
	{
		rigidBody = GetComponent<Rigidbody2D>();
		startPosition = new Vector2 (this.transform.position.x, this.transform.position.y);
	}
    // Start is called before the first frame update
    void Start()
    {
        isFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
		if (GameManager.instance.currentGameState == GameManager.GameState.GS_GAME){
			if (transform.position.y < threshold){
				GameManager.instance.removeLives();
				if (GameManager.instance.lives <= 0) {
					Debug.Log("Game Over");
				}
				else {
					transform.position = startPosition;
					Debug.Log("Player's hurt. Lives: " + GameManager.instance.lives);
				}
			}
		
			if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            {
                if (transform.parent != null) Unlock();
                Jump();
            }


            isWalking = false;


            if ( Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
                if (transform.parent != null) Unlock();
				transform.Translate( moveSpeed, 0.0f, 0.0f, Space.World );
				isWalking = true;
				if (!isFacingRight)
					Flip();
			}
			else if ( Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){
                if (transform.parent != null) Unlock();
                transform.Translate( -moveSpeed, 0.0f, 0.0f, Space.World );
				isWalking = true;
				if (isFacingRight)
					Flip();
			}
			
			animator.SetBool("isGrounded", isGrounded());
			animator.SetBool("isWalking", isWalking);
		}
    }
	
	bool isGrounded() 
		{
			if (Physics2D.Raycast(this.transform.position, Vector2.down, 3f, groundLayer.value)){
				return true;
			}
			else
				return false;			 
		}
        
	void Jump()
	{
		if (isGrounded()) {
			rigidBody.AddForce(Vector2.up * jumpForce);
		}
	}
	void Flip(){
		isFacingRight = !isFacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	void OnTriggerEnter2D(Collider2D other){
		
		
		
		if(other.CompareTag("Coin")){
			GameManager.instance.addCoins();
			Debug.Log("Score: " + score);
			other.gameObject.SetActive (false);
		}
		else if (other.CompareTag("EndGame")){
			if (GameManager.instance.keysCompleted == true){
				other.gameObject.SetActive (false);
				Debug.Log("WIN!!!");
				GameManager.instance.win();
			}
			else {
				Debug.Log("You need more keys!");
			}
		}
		else if (other.CompareTag("Enemy")){
			if (other.gameObject.transform.position.y + killOffset < this.transform.position.y){
				score += 10;
				Debug.Log("Killed an enemy! Score: " + score);
				GameManager.instance.addKills();
			}
			else{
				GameManager.instance.removeLives();
				Debug.Log("Player's hurt. Lives: " + GameManager.instance.lives);
				if (GameManager.instance.lives <=0) Debug.Log("Game Over");
				this.transform.position = startPosition;
			}
		}
		else if (other.CompareTag("Key")){
			GameManager.instance.addKeys();
			other.gameObject.SetActive (false);
			
			
		}
		else if (other.CompareTag("Heart")){
			other.gameObject.SetActive (false);
			GameManager.instance.addLives();
			Debug.Log("Heart found! Lives: " + GameManager.instance.lives);
		}
        else if (other.CompareTag("MovingPlatform"))
        {
            rigidBody.isKinematic = true;
            transform.parent = other.transform;
        }     
    }

    void Unlock()
    {
        rigidBody.isKinematic = false;
        transform.parent = null;
    }

	
}
