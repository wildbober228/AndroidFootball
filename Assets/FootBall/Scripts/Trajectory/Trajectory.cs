using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Trajectory : MonoBehaviour
{
    private List<Vector3> curve  = new List<Vector3>();
	private int maxHit ;
	private int currentHit ;
	private int resolution ;
	private RaycastHit targetHit;
    [SerializeField]
    string gate_tag = "Gate";

    public List<Vector3> calc(Transform start  , float maxLength  , int maxHit , int resolution  , float decrement  , Collider collider  )
    {
        this.maxHit = maxHit;
        this.currentHit = 0;
        this.resolution = resolution;

        var pos = start.position;
        var fwd = start.forward;

        this.curve = new List<Vector3>();
        this.curve.Add(pos);
        subCalc(pos, fwd, maxLength / this.resolution, decrement, collider);

        return this.curve;
    }

    private void subCalc(Vector3 pos , Vector3 fwd , float maxLength, float  decrement, Collider collider)
    {
        RaycastHit hit ;
        Ray ray ;

        if (this.currentHit <= this.maxHit)
        {
            ray = new Ray(pos, fwd);
            if (!Physics.SphereCast(ray, collider.bounds.extents.x,out hit, maxLength) || hit.collider.tag == gate_tag)
            {
                this.curve.Add(ray.GetPoint(maxLength));
                maxLength -= 0.001f / this.resolution;
                fwd.y -= decrement / this.resolution;
                if (this.curve[this.curve.Count - 1].y >= 0 && maxLength > 0)
                {
                    subCalc(this.curve[this.curve.Count - 1], fwd, maxLength, decrement, collider);
                }
            }
            else
            {
                if (hit.collider != collider
                && !collider.bounds.Intersects(hit.collider.bounds) && hit.transform.tag != gate_tag)
                {
                    this.currentHit++;
                    //this.curve.Add(ray.GetPoint(maxLength));
                    targetHit = hit;
                    fwd.y += 1.0f / this.resolution;
                    subCalc(this.curve[this.curve.Count - 1], Vector3.Reflect(fwd, hit.normal), maxLength, decrement, collider);
                }
            }
        }
    }
    public RaycastHit getTarget()
    {
        return targetHit;
    }
}