#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class MarkerPlacer : MonoBehaviour
{
    [Header("Cài đặt")]
    public int markerCount = 10;
    public float radius = 1.5f;
    public float cubeScale = 0.03f;
    public float startAngle = 0f; // lệch góc bắt đầu (theo độ)
    public bool clearOld = true;

    [ContextMenu("Place Markers")]
    void Place()
    {
        if (clearOld)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < markerCount; i++)
        {
            float angle = startAngle + i * (360f / markerCount); // 👈 góc lệch thêm ở đây
            float rad = angle * Mathf.Deg2Rad;

            Vector3 localPos = new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad)) * radius;

            GameObject marker = new GameObject("Marker_" + i);
            marker.transform.SetParent(this.transform);
            marker.transform.localPosition = localPos;
            marker.transform.localRotation = Quaternion.identity;

            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.SetParent(marker.transform);
            cube.transform.localPosition = Vector3.zero;
            cube.transform.localScale = Vector3.one * cubeScale;
            cube.name = "Visual";

            DestroyImmediate(cube.GetComponent<Collider>());
        }

        Debug.Log($"✅ Đã tạo {markerCount} marker quanh tâm, bắt đầu lệch {startAngle}°");
    }
}
#endif