using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour
{

    public Rigidbody projectile;
    public Transform shotPos;
    public float shotForce = 1000000f;
    public float moveSpeed = 100f;
    public float jumpHeight = 1000f;

    public float dragSpeed = 2;
    private Vector3 dragOrigin;

    Rigidbody rgbd;

    void Start()
    {
        rgbd = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float v = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        float jump = 0.0f;
        if (Input.GetButtonDown("Jump"))
        {
            jump = jumpHeight * Time.deltaTime * moveSpeed;
            rgbd.AddForce(new Vector3(0, jump, 0));
        }
        transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.deltaTime);
        transform.Translate(new Vector3(h, 0, v));

        // Create some bullets
        if (Input.GetButtonUp("Fire1"))
        {
            Rigidbody shot = Instantiate(projectile, shotPos.position, shotPos.rotation) as Rigidbody;
            shot.AddForce(shotPos.forward * shotForce);
        }

        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(1)) return;

        
        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(-pos.y * dragSpeed, pos.x * dragSpeed, 0);

        transform.Rotate(move,Space.World);

    }
}
