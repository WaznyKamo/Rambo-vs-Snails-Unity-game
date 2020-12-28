using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    public float XMin = 4f;
    public float XMax = 4f;
    public Animator animator;
    public bool isMovingRight = true;
    public bool isFacingRight = true;
    private float  startPositionX;
	private Rigidbody2D rigidBody;
    private float killOffset = 1.3f;


    void Awake(){
        startPositionX = this.transform.position.x;
        rigidBody = GetComponent<Rigidbody2D>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingRight){
            if (this.transform.position.x < startPositionX + XMax){
                MoveRight();
            }
            else
            {
                isMovingRight = false;
                MoveLeft();
                Flip();
            }
        }
        else
        {
            if (this.transform.position.x > startPositionX - XMin){
                MoveLeft();
            }
            else
            {
                isMovingRight = true;
                MoveRight();
                Flip();
            }
        }
    }

    void MoveRight(){
        transform.Translate( moveSpeed, 0.0f, 0.0f, Space.World );
    }

    void MoveLeft(){
        transform.Translate( -moveSpeed, 0.0f, 0.0f, Space.World );
    }

	void Flip(){
		isFacingRight = !isFacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}    

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            if (other.gameObject.transform.position.y  > this.transform.position.y + killOffset){
                Debug.Log("Enemy's dead");
                animator.SetBool("isDead", true);
                StartCoroutine(KillOnAnimationEnd());
            }
            else {
                Debug.Log("Killed player");
            }
        }
    }

    private IEnumerator KillOnAnimationEnd(){
        yield return new WaitForSeconds (1.0f);
        this.gameObject.SetActive(false);
    }

}
