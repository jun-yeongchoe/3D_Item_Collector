using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask ground;
    [SerializeField] Transform groundCheck;
    private float groundCheckRadius = 1f;
    private bool isGrounded;


    public bool GroundCheck()
    {
        var hits = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, ground);
        isGrounded = hits.Length > 0;
        return isGrounded;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
