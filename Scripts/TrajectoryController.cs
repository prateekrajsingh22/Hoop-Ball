using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrajectoryController : MonoBehaviour
{
    [SerializeField]
    public LineRenderer _lineRenderer;

    //[SerializeField]
    //[Range(3, 30)]
    private int _lineSegmentCount = 20;

    private List<Vector3> _linePoints = new List<Vector3>();

    #region Singleton

    public static TrajectoryController Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public void UpdateTrajectory(Vector3 forceVector, Rigidbody rigidBody, Vector3 startinrPoint)
    {
        Vector3 velocity = (forceVector / rigidBody.mass) * Time.fixedDeltaTime;
        float flightDuration = (2 * velocity.y) / Physics.gravity.y;
        float stepTime = flightDuration / _lineSegmentCount;
        _linePoints.Clear();

        for (int i=0; i<30; i++)
        {
            float stepTimePassed = stepTime * i;
            Vector3 MovementVector = new Vector3(
                velocity.x * stepTimePassed,
                velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
                velocity.z * stepTimePassed
                );

            /*RaycastHit hit;
            if (Physics.Raycast (origin: startinrPoint, direction: -MovementVector, out hit, MovementVector.magnitude))
            {
                break;
            }*/
            _linePoints.Add(startinrPoint - MovementVector);
        }

        _lineRenderer.positionCount = _linePoints.Count;
        _lineRenderer.SetPositions(_linePoints.ToArray());

    }
}