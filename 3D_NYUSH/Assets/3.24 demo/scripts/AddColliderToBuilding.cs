using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddColliderToBuilding : MonoBehaviour
{
    void Start()
    {
        // 获取模型的 Mesh
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        // 创建碰撞器
        MeshCollider collider = gameObject.AddComponent<MeshCollider>();
        collider.sharedMesh = mesh;
    }
}
