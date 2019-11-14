using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class T_Controller : MonoBehaviour
{
    

    [SerializeField]
    Transform trajectoryStart;
    [SerializeField]
    float trajectoryStrength = 1;
    [SerializeField]
    int trajectoryMaxCollisions = 10;
    [SerializeField]
    int trajectoryResolution = 5;
    [SerializeField]
    List<TrajectoryMode> trajectoryMode = new List<TrajectoryMode>();
    [SerializeField]
    List<Vector3> trajectoryCurve = new List<Vector3>();
    [SerializeField]
    GameObject throwThing;

    private bool isMovingToTarget = false;
    private bool isReachedTarget = false;
    [SerializeField]
    Trajectory traj = new Trajectory();
    public MovePath movePath;
    private float trajectoryDecrement;

    public GameObject debug_1;
    public GameObject debug_2;

    public Text debug;

    void Start()
    {
        SetTrajectoryMode(0);
    }

    void Update()
    {
        if (!isMovingToTarget)
        {
            Show(trajectoryStrength, trajectoryMaxCollisions, throwThing.GetComponent<Collider>());
        }
        else
        {
            hasReachedTarget();
        }
    }

    

    void  SetTrajectoryMode(int index)
    {

        try
        {
            trajectoryDecrement = trajectoryMode[index].decrement;
            trajectoryStart.localEulerAngles =
            transform.localEulerAngles = new Vector3(
             trajectoryMode[index].startAngle,
             transform.localEulerAngles.y,
             transform.localEulerAngles.z
         );
        }
        catch (System.Exception ex)
        {
            if(debug != null)
            debug.text += ex;
        }

    }

    void Show(float strength,int bounces , Collider collider)
    {
        trajectoryStrength = strength;
        trajectoryMaxCollisions = bounces;
        trajectoryCurve = traj.calc(trajectoryStart, trajectoryStrength, trajectoryMaxCollisions, trajectoryResolution, trajectoryDecrement, collider);

        GetComponent<LineRenderer>().SetVertexCount(trajectoryCurve.Count);
        int i = 0;
        foreach (Vector3 vec in trajectoryCurve)
        {
            GetComponent<LineRenderer>().SetPosition(i, vec);
            i++;
        }
    }

    public void Throw()
    {
        //movePath = new MovePath();
        debug_1.SetActive(true);
        movePath.importNodesVector3(trajectoryCurve[0], trajectoryCurve);
        movePath.setGameObject(throwThing);
        movePath.setMovingSpeed(150.0f);
        movePath.applyAnimation();
        isMovingToTarget = true;
        isReachedTarget = false;
        debug_2.SetActive(true);
    }

    

    public void RotateLeft()
    {
        trajectoryStart.transform.RotateAround(trajectoryStart.position,Vector3.up,-0.5f);
        Debug.Log("RotateLeft");
    }

    public void RotateRight()
    {
        trajectoryStart.transform.RotateAround(trajectoryStart.position, Vector3.up, 0.5f);
        Debug.Log("RotateRight");
    }

    RaycastHit getTarget()
    {
        return traj.getTarget();
    }

    bool hasReachedTarget()
    {

        if (isMovingToTarget)
        {
            if (movePath.hasFinished())
            {
                Debug.Log("hits target");
                isMovingToTarget = false;
                isReachedTarget = true;
                throwThing.transform.position = trajectoryStart.position;
                //Destroy(throwThing, 0);
                //Hide();
            }
        }

        return isReachedTarget;
    }

    void Hide()
    {
        trajectoryCurve = new List<Vector3> ();
        GetComponent<LineRenderer>().SetVertexCount(0);
    }
}
