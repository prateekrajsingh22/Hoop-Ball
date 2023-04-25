using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shooting: MonoBehaviour
{
    
    [SerializeField] public LineRenderer dragLine;
    [SerializeField] private Transform touchPoint;
    //[SerializeField] private Transform effect;

    private Camera mainCamera;
    private int forceMultiplier = 20;
    public GameFailScreen gameFailScreen;
    private Vector3 mousePressDownPos;
    private Vector3 mouseReleasePos;
    private Vector3 mouseMovePos;
    private Rigidbody rb;
    private bool isShoot;

    // Exit-game function
    public void quit_func()
    {
        Application.Quit();
    }

    public void getMousePos()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo) )
        {
            dragLine.SetPosition(1, hitInfo.point);
            touchPoint.position = Input.mousePosition;
        }
    }

    //Setting position to aim
    public void OnMouseDrag()
    {
        Vector3 Force = Input.mousePosition - mousePressDownPos;
        Vector3 forceV = (new Vector3(Force.y, -Force.y * 0.5f, -Force.x) * forceMultiplier);
        if (!isShoot)
        {
            TrajectoryController.Instance._lineRenderer.gameObject.SetActive(true);
            TrajectoryController.Instance.UpdateTrajectory(forceVector: forceV, rb, startinrPoint: transform.position);
        }

        dragLine.gameObject.SetActive(true);
        touchPoint.gameObject.SetActive(true);

        dragLine.SetPosition(0, transform.position);
        getMousePos();

    }

    public void OnMouseDown()
    {
        mousePressDownPos = Input.mousePosition;
    }

    // Shoot, if there are remaining lives.
    public void OnMouseUp()
    {
        mouseReleasePos = Input.mousePosition;
        mouseMovePos = mouseReleasePos - mousePressDownPos;

        if (mouseMovePos == new Vector3(0, 0, 0))
        {
            Shoot(new Vector3(0, -20, 0));
        }
        else
        {
            Shoot(mouseMovePos);
        }

        dragLine.gameObject.SetActive(false);
        touchPoint.gameObject.SetActive(false);

        LifeMgmt.life--;
    }

    //Shoot (Adding throw-force
    public void Shoot(Vector3 Force)
    {
        if (LifeMgmt.life == 0)
        {
            return;
        }

        rb.AddForce(new Vector3(Force.y, -Force.y * 0.5f, -Force.x) * forceMultiplier);
        rb.AddTorque(new Vector3(Force.x, Force.y, 200));
        //effect.transform.gameObject.SetActive(true);
        isShoot = true;
    }

    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        mainCamera = FindAnyObjectByType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
