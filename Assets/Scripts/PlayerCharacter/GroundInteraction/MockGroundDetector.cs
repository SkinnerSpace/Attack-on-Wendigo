using UnityEngine;

public class MockGroundDetector : IGroundDetector
{
    public bool isGrounded;
    public MockGroundDetector(bool isGrounded) => this.isGrounded = isGrounded;
    public void SetState(bool isGrounded) => this.isGrounded = isGrounded;
    public bool Check(Vector3 position, float radius) => isGrounded; 
}
