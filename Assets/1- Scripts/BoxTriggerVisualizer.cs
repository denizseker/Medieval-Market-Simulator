using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BoxTriggerVisualizer : MonoBehaviour
{
    private BoxCollider boxCollider;
    private Material lineMaterial;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();

        // Basit bir shader oluþtur
        Shader shader = Shader.Find("Hidden/Internal-Colored");
        lineMaterial = new Material(shader)
        {
            hideFlags = HideFlags.HideAndDontSave
        };
        lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
        lineMaterial.SetInt("_ZWrite", 0);
    }

    void OnRenderObject()
    {
        if (boxCollider == null) return;

        // BoxCollider'ýn merkezini ve boyutlarýný dünya koordinatlarýna göre hesapla
        Vector3 center = boxCollider.center;
        Vector3 size = boxCollider.size;


        // BoxCollider'ýn yerel dönüþüm matrisini al
        Matrix4x4 localToWorld = transform.localToWorldMatrix;

        // Gizmos rengini ve transparanlýðýný ayarla
        lineMaterial.SetPass(0);
        GL.PushMatrix();
        GL.MultMatrix(localToWorld);
        GL.Begin(GL.QUADS);
        GL.Color(new Color(0, 1, 0, 0.2f)); // Yeþil renk, %80 transparan

        // BoxCollider'ýn alanýný çiz
        DrawCube(center, size);

        GL.End();
        GL.PopMatrix();
    }

    void DrawCube(Vector3 center, Vector3 size)
    {
        Vector3 halfSize = size * 0.5f;

        // Alt yüzey
        GL.Vertex(center + new Vector3(-halfSize.x, -halfSize.y, -halfSize.z));
        GL.Vertex(center + new Vector3(halfSize.x, -halfSize.y, -halfSize.z));
        GL.Vertex(center + new Vector3(halfSize.x, -halfSize.y, halfSize.z));
        GL.Vertex(center + new Vector3(-halfSize.x, -halfSize.y, halfSize.z));

        // Üst yüzey
        GL.Vertex(center + new Vector3(-halfSize.x, halfSize.y, -halfSize.z));
        GL.Vertex(center + new Vector3(halfSize.x, halfSize.y, -halfSize.z));
        GL.Vertex(center + new Vector3(halfSize.x, halfSize.y, halfSize.z));
        GL.Vertex(center + new Vector3(-halfSize.x, halfSize.y, halfSize.z));

        // Ön yüzey
        GL.Vertex(center + new Vector3(-halfSize.x, -halfSize.y, halfSize.z));
        GL.Vertex(center + new Vector3(halfSize.x, -halfSize.y, halfSize.z));
        GL.Vertex(center + new Vector3(halfSize.x, halfSize.y, halfSize.z));
        GL.Vertex(center + new Vector3(-halfSize.x, halfSize.y, halfSize.z));

        // Arka yüzey
        GL.Vertex(center + new Vector3(-halfSize.x, -halfSize.y, -halfSize.z));
        GL.Vertex(center + new Vector3(halfSize.x, -halfSize.y, -halfSize.z));
        GL.Vertex(center + new Vector3(halfSize.x, halfSize.y, -halfSize.z));
        GL.Vertex(center + new Vector3(-halfSize.x, halfSize.y, -halfSize.z));

        // Sol yüzey
        GL.Vertex(center + new Vector3(-halfSize.x, -halfSize.y, -halfSize.z));
        GL.Vertex(center + new Vector3(-halfSize.x, -halfSize.y, halfSize.z));
        GL.Vertex(center + new Vector3(-halfSize.x, halfSize.y, halfSize.z));
        GL.Vertex(center + new Vector3(-halfSize.x, halfSize.y, -halfSize.z));

        // Sað yüzey
        GL.Vertex(center + new Vector3(halfSize.x, -halfSize.y, -halfSize.z));
        GL.Vertex(center + new Vector3(halfSize.x, -halfSize.y, halfSize.z));
        GL.Vertex(center + new Vector3(halfSize.x, halfSize.y, halfSize.z));
        GL.Vertex(center + new Vector3(halfSize.x, halfSize.y, -halfSize.z));
    }
}
