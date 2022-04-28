using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrawRadar : MonoBehaviour
{
    private float ThetaScale = 0.25f;
    private float Radius = 0.5f;
    private int Size;
    private LineRenderer LineDrawer;
    private float Theta = 0f;
    private List<Vector3> point = new List<Vector3>();

    public void Init(float thetaScale, float radius ) {
        ThetaScale = thetaScale;
        Radius = radius;
        LineDrawer = GetComponent<LineRenderer>();
        PosUpdate();
    }

    void PosUpdate() {
        Theta = 0f;
        Size = (int)((1f / ThetaScale) + 1f);
        LineDrawer.SetVertexCount(Size);
        for (int i = 0; i < Size; i++) {
            Theta += (2.0f * Mathf.PI * ThetaScale);
            float x = Radius * Mathf.Cos(Theta);
            float y = Radius * Mathf.Sin(Theta);
            point.Add(new Vector3(x, 0, y));
            LineDrawer.SetPosition(i, point[i]);
        }
    }

    public List<Vector3> GetRandomArrayPoint() {
        
        //끝에 포인트는 같은 위치라 지움
        point.RemoveAt(point.Count - 1);

        for (int i = point.Count - 1;  i > 0; i--) {
            int rnd = UnityEngine.Random.Range(0, i);
            Vector3 temp = point[i];
            point[i] = point[rnd];
            point[rnd] = temp;
        }

        return point;
    }
}
