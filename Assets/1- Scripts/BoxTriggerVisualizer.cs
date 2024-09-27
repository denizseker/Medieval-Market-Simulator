using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BoxTriggerVisualizer : MonoBehaviour
{
    private BoxCollider boxCollider;
    private Material lineMaterial;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();

        // Basit bir shader olu�tur
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

        // BoxCollider'�n merkezini ve boyutlar�n� d�nya koordinatlar�na g�re hesapla
        Vector3 center = boxCollider.center;
        Vector3 size = boxCollider.size;


        // BoxCollider'�n yerel d�n���m matrisini al
        Matrix4x4 localToWorld = transform.localToWorldMatrix;

        // Gizmos rengini ve transparanl���n� ayarla
        lineMaterial.SetPass(0);
        GL.PushMatrix();
        GL.MultMatrix(localToWorld);
        GL.Begin(GL.QUADS);
        GL.Color(new Color(0, 1, 0, 0.2f)); // Ye�il renk, %80 transparan

        // BoxCollider'�n alan�n� �iz
        DrawCube(center, size);

        GL.End();
        GL.PopMatrix();
    }

    void DrawCube(Vector3 center, Vector3 size)
    {
        Vector3 halfSize = size * 0.5f;

        // Alt y�zey
        GL.Vertex(center + new Vector3(-halfSize.x, -halfSize.y, -halfSize.z));
        GL.Vertex(center + new Vector3(halfSize.x, -halfSize.y, -halfSize.z));
        GL.Vertex(center + new Vector3(halfSize.x, -halfSize.y, halfSize.z));
        GL.Vertex(center + new Vector3(-halfSize.x, -halfSize.y, halfSize.z));

        // �st y�zey
        GL.Vertex(center + new Vector3(-halfSize.x, halfSize.y, -halfSize.z));
        GL.Vertex(center + new Vector3(halfSize.x, halfSize.y, -halfSize.z));
        GL.Vertex(center + new Vector3(halfSize.x, halfSize.y, halfSize.z));
        GL.Vertex(center + new Vector3(-halfSize.x, halfSize.y, halfSize.z));

        // �n y�zey
        GL.Vertex(center + new Vector3(-halfSize.x, -halfSize.y, halfSize.z));
        GL.Vertex(center + new Vector3(halfSize.x, -halfSize.y, halfSize.z));
        GL.Vertex(center + new Vector3(halfSize.x, halfSize.y, halfSize.z));
        GL.Vertex(center + new Vector3(-halfSize.x, halfSize.y, halfSize.z));

        // Arka y�zey
        GL.Vertex(center + new Vector3(-halfSize.x, -halfSize.y, -halfSize.z));
        GL.Vertex(center + new Vector3(halfSize.x, -halfSize.y, -halfSize.z));
        GL.Vertex(center + new Vector3(halfSize.x, halfSize.y, -halfSize.z));
        GL.Vertex(center + new Vector3(-halfSize.x, halfSize.y, -halfSize.z));

        // Sol y�zey
        GL.Vertex(center + new Vector3(-halfSize.x, -halfSize.y, -halfSize.z));
        GL.Vertex(center + new Vector3(-halfSize.x, -halfSize.y, halfSize.z));
        GL.Vertex(center + new Vector3(-halfSize.x, halfSize.y, halfSize.z));
        GL.Vertex(center + new Vector3(-halfSize.x, halfSize.y, -halfSize.z));

        // Sa� y�zey
        GL.Vertex(center + new Vector3(halfSize.x, -halfSize.y, -halfSize.z));
        GL.Vertex(center + new Vector3(halfSize.x, -halfSize.y, halfSize.z));
        GL.Vertex(center + new Vector3(halfSize.x, halfSize.y, halfSize.z));
        GL.Vertex(center + new Vector3(halfSize.x, halfSize.y, -halfSize.z));
    }
}
