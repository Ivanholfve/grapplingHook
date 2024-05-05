using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    private LineRenderer lr;
    private Vector2 grapplePoint;
    public string whatIsGrappleable;
    public Transform gunTip, camera, player;
    public Vector2 mousePos;
    private float maxDistance = 100f;
    private SpringJoint2D joint;
    Vector2 aimdDirection;
    RaycastHit2D hit;

    public bool isGrapple;

    //variabler som representerar linerendern, grapplepunkter samt vad som kan grapplas:3
    void Start()
    {

        lr = GetComponent<LineRenderer>();
        isGrapple = false;
        //hämtar linerenderen från unity och attachar den på variabeln lr:3
        lr.positionCount = 2;

    }

    // Update is called once per frame
    void Update()
    {
        aimdDirection = mousePos - (Vector2)gunTip.position;
        // aimdirection är musens position minus guntipen
        mousePos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //gör musens position till en position i unity
        Debug.DrawLine(transform.position, mousePos);
        //drar linjen
        Physics2D.Raycast(gunTip.position,aimdDirection);
        hit = Physics2D.Raycast(gunTip.position, aimdDirection); 
            
            //hit.point kalla på hit
            //när springjointen skapas så blir en punkt "hit" och den andra gunTip.position


        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
        DrawRope();
        if (isGrapple == true)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                joint.distance -= 0.03f;

            }
            if (Input.GetKey(KeyCode.E))
            {
                joint.distance += 0.03f;
            }
        }
        

    }
    void StartGrapple()
    {    

        {
            lr.positionCount = 2;
            if (Physics2D.Raycast(gunTip.position, aimdDirection,maxDistance))
            {
                if (hit.collider.gameObject.CompareTag("Platform")){
                isGrapple = true;

                grapplePoint = hit.point;
                joint = player.gameObject.AddComponent<SpringJoint2D>();
                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = grapplePoint;
                joint.enableCollision=true;

                float distanceFromPoint = Vector2.Distance(a: player.position, b: grapplePoint);

                joint.distance = distanceFromPoint ;
                joint.dampingRatio = 1;
                joint.frequency = 900000;
                }
                

                

            }
        }
    }
    //call when grapple:3
    void StopGrapple()
    {
        lr.positionCount = 0;
        DestroyImmediate(joint);
        isGrapple = false;
    }
    //call when stop grapple:3
    void DrawRope()
    {
        if (!joint) return;
        lr.SetPosition(index: 0, gunTip.position);
        lr.SetPosition(index: 1, grapplePoint);
    }
}
