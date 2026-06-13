using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movespeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        animator= this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * movespeed;     
    }

    public void Move(InputAction.CallbackContext context)
    {
        animator.SetBool("IsWalking", true);

        if (context.canceled)//stop walking
        {
            animator.SetBool("IsWalking", false);
            animator.SetFloat("LastX", moveInput.x); 
            animator.SetFloat("LastY", moveInput.y);
        }

        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("CurrentX", moveInput.x);
        animator.SetFloat("CurrentY", moveInput.y);
    }
}
