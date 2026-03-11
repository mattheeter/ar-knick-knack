using UnityEngine;

public class RingBox : MonoBehaviour
{
    public Transform lid;
    public Transform cube;
    public Transform cam;
    public float xAngle = -120;
    public float yAngle = 50;
    public float zAngle = -45;
    
    public float xLoc = 0.02383169f;
    public float yLoc = 0.02849094f;
    public float zLoc = -0.02633353f;
    public bool open = false;

    void Update()
    {
        // If the cube is moved close to the camera, open the box
        float distance = Vector3.Distance(lid.position, cam.position);
        if (distance < 5)
        {
            lid.localRotation = Quaternion.Euler(xAngle, yAngle, zAngle);
            lid.localPosition = new Vector3(xLoc, yLoc, zLoc);
        }
        else
        {
            lid.localRotation = Quaternion.Euler(0, -22.5f, 0);
            lid.localPosition = new Vector3(0.02170832f, 0.007613274f, 4.656613e-10f);
        }
    }
}