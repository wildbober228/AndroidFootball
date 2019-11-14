using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovePath : MonoBehaviour
{
    private GameObject[] nodes;
    private List<Vector3> nodesVector3  = new List<Vector3>();
	private AnimationCurve animationCurvePosX  = new AnimationCurve();
    private AnimationCurve animationCurvePosY  = new AnimationCurve();
    private AnimationCurve animationCurvePosZ  = new AnimationCurve();
    private GameObject _gameObject ;
	private string gameObjectPath  ;
	public Animation _animation ;
	public AnimationClip clip ;
	private float movingSpeed  = 1.0f;
	
	public void importNodes(GameObject firstNode, GameObject[] arr)
    {
        nodes = arr;
        //Debug.Log(nodes.length);

        this.animationCurvePosX = new AnimationCurve();
        this.animationCurvePosY = new AnimationCurve();
        this.animationCurvePosZ = new AnimationCurve();

        var f = 0; //frame

        this.animationCurvePosX.AddKey(new Keyframe(f, firstNode.transform.localPosition.x));
        this.animationCurvePosY.AddKey(new Keyframe(f, firstNode.transform.localPosition.y));
        this.animationCurvePosZ.AddKey(new Keyframe(f, firstNode.transform.localPosition.z));

        animationCurvePosX.SmoothTangents(f, 0.0f);
        animationCurvePosY.SmoothTangents(f, 0.0f);
        animationCurvePosZ.SmoothTangents(f, 0.0f);

        foreach (GameObject g in arr)
        {
            f++;

            this.animationCurvePosX.AddKey(new Keyframe(f, g.transform.localPosition.x));
            this.animationCurvePosY.AddKey(new Keyframe(f, g.transform.localPosition.y));
            this.animationCurvePosZ.AddKey(new Keyframe(f, g.transform.localPosition.z));

            animationCurvePosX.SmoothTangents(f, 0.0f);
            animationCurvePosY.SmoothTangents(f, 0.0f);
            animationCurvePosZ.SmoothTangents(f, 0.0f);
        }
    }

    public  void importNodesVector3(Vector3 firstNode , List<Vector3> arr )
    {
        nodesVector3 = arr;
        //Debug.Log(nodes.length);

        this.animationCurvePosX = new AnimationCurve();
        this.animationCurvePosY = new AnimationCurve();
        this.animationCurvePosZ = new AnimationCurve();

        var f = 0; //frame

        this.animationCurvePosX.AddKey(new Keyframe(f, firstNode.x));
        this.animationCurvePosY.AddKey(new Keyframe(f, firstNode.y));
        this.animationCurvePosZ.AddKey(new Keyframe(f, firstNode.z));

        animationCurvePosX.SmoothTangents(f, 0.0f);
        animationCurvePosY.SmoothTangents(f, 0.0f);
        animationCurvePosZ.SmoothTangents(f, 0.0f);

        foreach (Vector3 g in arr)
        {
            f++;

            this.animationCurvePosX.AddKey(new Keyframe(f, g.x));
            this.animationCurvePosY.AddKey(new Keyframe(f, g.y));
            this.animationCurvePosZ.AddKey(new Keyframe(f, g.z));

            animationCurvePosX.SmoothTangents(f, 0.0f);
            animationCurvePosY.SmoothTangents(f, 0.0f);
            animationCurvePosZ.SmoothTangents(f, 0.0f);
        }
    }

    public void setGameObject(GameObject g)
    {
        this._gameObject = g;
        this.gameObjectPath = ""; //so lassen!
        this._animation = g.GetComponent<Animation>();
        //this.animation = g.animation;
    }

    public void setMovingSpeed(float speed)
    { 
        this.movingSpeed = speed;
    }

    public void applyAnimation()
    {
        this.clip = new AnimationClip();
        clip.legacy = true;
        this.clip.SetCurve(this.gameObjectPath, typeof(Transform) , "localPosition.x", this.animationCurvePosX);
        this.clip.SetCurve(this.gameObjectPath, typeof(Transform), "localPosition.y", this.animationCurvePosY);
        this.clip.SetCurve(this.gameObjectPath, typeof(Transform), "localPosition.z", this.animationCurvePosZ);
        this.clip.wrapMode = WrapMode.Once;
       
        this.clip.name = "movepath";
        this._animation.AddClip(this.clip, this.clip.name);
        this._animation[this.clip.name].speed = this.movingSpeed;
        this._animation[this.clip.name].wrapMode = WrapMode.Once;
        this._animation.Play(this.clip.name);
    }

    public bool hasFinished()
    {
        return !this._animation[this.clip.name].enabled;
    }

    public float getNextKeyframe()
    {
        return Mathf.Round(this._animation[this.clip.name].time);
    }

    public GameObject getNextKeyframeObject()
    {
        if (Mathf.Round(this._animation[this.clip.name].time) < nodes.Length && nodes[Mathf.RoundToInt(this._animation[this.clip.name].time)] != null)
        {
            return nodes[Mathf.RoundToInt(this._animation[this.clip.name].time)];
        }
        else
        {
            return nodes[nodes.Length - 1];
        }
    }

    public  Vector3 getNextKeyframeObjectVector3()
    {
        if (Mathf.Round(this._animation[this.clip.name].time) < nodesVector3.Count
        && nodesVector3[Mathf.RoundToInt(this._animation[this.clip.name].time)] != null)
        {
            return nodesVector3[Mathf.RoundToInt(this._animation[this.clip.name].time)];
        }
        else
        {
            return nodesVector3[nodesVector3.Count - 1];
        }
    }
}