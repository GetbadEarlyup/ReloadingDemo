using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinePlayer : MonoBehaviour
{
    public GameObject[] bodys;
    //更多请访问：CSDN作孽就得先起床
    void Start()
    {
        //获取当前对象所有骨骼信息
        Transform[] allbones = GetComponentsInChildren<Transform>();
        //把对象存入字典
        Dictionary<string, Transform> directory = new Dictionary<string, Transform>();
        //以名称的方式将对象存入字典
        for (int i = 0; i < allbones.Length; i++)
        {
            directory.Add(allbones[i].name, allbones[i]);
        }
        //保存骨骼信息
        List<Transform> bones = new List<Transform>();
        //合并网格材质球
        List<CombineInstance> combines = new List<CombineInstance>();
        //材质球存储
        List<Material> materials = new List<Material>();
        //便利所有组件对象
        for (int i = 0; i < bodys.Length; i++)
        {
            //新建 组合网格
            CombineInstance combine = new CombineInstance();
            //获取蒙皮属性 在网格中放入骨头
            combine.mesh = bodys[i].GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh;
            //强制蒙皮网格在渲染时重新计算其矩阵
            combine.transform = bodys[i].transform.localToWorldMatrix;
            //放入合并网格中
            combines.Add(combine);
            //获取蒙皮的网格 放入材质
            materials.Add(bodys[i].GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial);
            //获取组件骨骼
            Transform[] bodybones = bodys[i].GetComponentInChildren<SkinnedMeshRenderer>().bones;
            //遍历骨骼 筛选对象放入
            for (int j = 0; j < bodybones.Length; j++)
            {
                bones.Add(directory[bodybones[j].name]);
            }
        }

        Mesh mesh = new Mesh();
        //进行重新修改渲染
        mesh.CombineMeshes(combines.ToArray(), false);
        gameObject.AddComponent<SkinnedMeshRenderer>().sharedMesh=mesh;
        GetComponent<SkinnedMeshRenderer>().materials = materials.ToArray();
        GetComponent<SkinnedMeshRenderer>().bones = bones.ToArray();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
