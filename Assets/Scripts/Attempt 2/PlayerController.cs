using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    public GameObject root;
    public Transform BRFoot, BLFoot, FRFoot, FLFoot;
    public float offset = 1.38f;
    public float smoothness = 5f;
    public float speed = 12f;
    public float rotSpeed = 12f;
    private Vector3 lastBodyUp;

    private void Start()
    {
        lastBodyUp = root.transform.parent.forward;
    }

    // Update is called once per frame
    void Update()
    {

        float avgHeight = (BRFoot.position.y + BLFoot.position.y + FRFoot.position.y + FLFoot.position.y) / 4;
        root.transform.parent.position = new Vector3(root.transform.parent.position.x, avgHeight + offset, root.transform.parent.position.z);

        Vector3 v1 = FRFoot.position - BLFoot.position;
        Vector3 v2 = BRFoot.position - FLFoot.position;
        Vector3 normal = Vector3.Cross(v1, v2).normalized;
        Vector3 up = Vector3.Lerp(lastBodyUp, normal, 1f / (float)(smoothness + 1));
        root.transform.parent.forward = up;
        root.transform.parent.rotation = Quaternion.LookRotation(transform.forward, up);
        Debug.DrawRay(root.transform.parent.position, up * 100, Color.red);
        lastBodyUp = root.transform.parent.forward;

        float multiplier = 1f;
        if (Input.GetKey(KeyCode.LeftShift))
            multiplier = 2f;

        float valueY = Input.GetAxis("Vertical");
        float valueX = Input.GetAxis("Horizontal");

        Vector3 moveDir = Camera.main.transform.forward * valueY + Camera.main.transform.right * valueX;
        moveDir.y = 0;
        moveDir.Normalize();

        if(moveDir != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, rotSpeed * Time.deltaTime);
            transform.position += moveDir * speed * multiplier * Time.deltaTime;
        }
    }
}
