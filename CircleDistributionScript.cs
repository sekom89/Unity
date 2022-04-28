using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleDistributionScript : MonoBehaviour {

    public int CircleCount;
    public float Radius;
    private float thetaScale = 0.25f;
    
    public GameObject PlayerPrefab;
    public GameObject DrawPrefab;
    
    private GameObject basePoint;
    private List<GameObject> prefabList = new List<GameObject>();
    private List<DrawRadar> DrawRadarList = new List<DrawRadar>();
    
    void Start() {

        prefabList.Clear();
        DrawRadarList.Clear();
        CirlceDrawInit();
        PointInit();
    }

    private void CirlceDrawInit() {

        int count = (int) Mathf.Log(CircleCount, 2) - 1;
        if (count < 1) {
            count = 1;
        }
        

        for (int i = 0; i < count; i++) {
            var obj = GameObject.Instantiate(DrawPrefab);
            obj.transform.parent = this.transform;
            var radar = obj.GetComponent<DrawRadar>();
            
            radar.Init(thetaScale / (i+1), Radius * (i+1));
            DrawRadarList.Add(radar);
        }
    }

    private void PointInit() {
        

        if (CircleCount == 1) {
            var obj = GameObject.Instantiate(PlayerPrefab);
            basePoint = this.transform.Find("BasePoint").gameObject;
            obj.transform.position = basePoint.transform.position;
            prefabList.Add(obj);
            return;
        }
        
        List<Vector3> randomPointList = null;

        int prevCount = 0;
        randomPointList = DrawRadarList[prevCount].GetRandomArrayPoint();

        int Arrayindex = 0;
        for (int i = 0; i < CircleCount; i++) {
            var obj = GameObject.Instantiate(PlayerPrefab);
            
            if (randomPointList.Count == Arrayindex) {

                prevCount++;
                if (DrawRadarList.Count == prevCount) {
                    break;
                }
                randomPointList = DrawRadarList[prevCount].GetRandomArrayPoint();
                Arrayindex = 0;
            }
            
            Debug.Log($" CircleCount : {i}");
            obj.name = i.ToString();
            obj.transform.position = randomPointList[Arrayindex++];
            prefabList.Add(obj);
        }
    }
    
}

