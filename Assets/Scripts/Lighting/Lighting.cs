using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour{
    public float viewRadius;
    float viewAngle = 360;

    public LayerMask obstacleMask;

    [HideInInspector] public List<Transform> visibleTargets = new List<Transform>();

    public float meshResolution;
    public float edgeItterations;
    public float edgeDistance;

    public MeshFilter viewMeshFilter;
    MeshRenderer mr;
    Mesh viewMesh;

    public Color lightOff;
    public Color lightOn;

    BackgroundManager bm;

    void Start() {
        bm = FindObjectOfType<BackgroundManager>();
        mr = GetComponent<MeshRenderer>();
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
    }

    void Update() {
        DrawFieldOfView(); 
    }

    void SetMat(){
        if(bm.day)
            mr.material.color = Color.Lerp(mr.material.color, lightOff, 1 * Time.deltaTime);
        else
            mr.material.color = Color.Lerp(mr.material.color, lightOn, 1 * Time.deltaTime);
    }

    void DrawFieldOfView(){
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector2> viewPoints = new List<Vector2>();
        ViewCastInfo oldViewCast = new ViewCastInfo();

        for(int i = 0; i <= stepCount; i++){
            float angle = transform.eulerAngles.y - viewAngle/2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);

            if(i > 0){
                bool edgeDistanceExceeded = Mathf.Abs(oldViewCast.dist - newViewCast.dist) > edgeDistance;
                if(oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDistanceExceeded)){
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                    if(edge.pointA != Vector2.zero){
                        viewPoints.Add(edge.pointA);
                    }
                    if(edge.pointB != Vector2.zero){
                        viewPoints.Add(edge.pointB);
                    }
                }
            }

            viewPoints.Add(newViewCast.point);
            oldViewCast = newViewCast;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount-2) * 3];

        vertices[0] = Vector2.zero;
        for(int i=0; i<vertexCount-1; i++){
            vertices[i+1] = transform.InverseTransformPoint(viewPoints[i]);

            if(i<vertexCount-2){
                triangles[i*3] = 0;
                triangles[i*3+1] = i+1;
                triangles[i*3+2] = i+2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();

        SetMat();
    }

    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast){
        Vector2 minPoint = Vector2.zero;
        Vector2 maxPoint = Vector2.zero;

        for (int i = 0; i < edgeItterations; i++){
            float angle = (minViewCast.angle + maxViewCast.angle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);
            bool edgeDistanceExceeded = Mathf.Abs(minViewCast.dist - newViewCast.dist) > edgeDistance;

            if(newViewCast.hit == minViewCast.hit && !edgeDistanceExceeded){
                minViewCast.angle = angle;
                minPoint = newViewCast.point;
            }else{
                maxViewCast.angle = angle;
                maxPoint = newViewCast.point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }

    ViewCastInfo ViewCast(float globalAngle){
        Vector2 dir = DirFromAngle(globalAngle, true);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, viewRadius, obstacleMask);

        if(hit){
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }else{
            return new ViewCastInfo(false, new Vector2(transform.position.x, transform.position.y) + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    public Vector2 DirFromAngle(float angle, bool global){
        if(!global) angle += transform.eulerAngles.y;
        return new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    public struct ViewCastInfo{
        public bool hit;
        public Vector2 point;
        public float dist;
        public float angle;

        public ViewCastInfo(bool _hit, Vector2 _point, float _dist, float _angle){
            hit = _hit;
            point = _point;
            dist = _dist;
            angle = _angle;
        }
    }

    public struct EdgeInfo{
        public Vector2 pointA;
        public Vector2 pointB;

        public EdgeInfo(Vector2 _pointA, Vector2 _pointB){
            pointA = _pointA;
            pointB = _pointB;
        }
    }
}