using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class CreatNewModule : MonoBehaviour
{
    //更多请访问：CSDN作孽就得先起床
    public int touid = 0;
    public int yifuid = 0;
    public int uiid = 0;

    public GameObject[] bodys;


    void Start()
    {
        gameObject.AddComponent<SkinnedMeshRenderer>();
        Change();
    }


    public void Change() 
    {
        //加载切换的模型
        bodys[0] = Resources.Load<GameObject>("Player/tou_" + touid);
        bodys[1] = Resources.Load<GameObject>("Player/tui_" + yifuid);
        bodys[2] = Resources.Load<GameObject>("Player/shen_" + uiid);

        //以下代码合成模型
        Transform[] allbones = GetComponentsInChildren<Transform>();

        Dictionary<string, Transform> directory = new Dictionary<string, Transform>();

        for (int i = 0; i < allbones.Length; i++)
        {
            directory.Add(allbones[i].name, allbones[i]);
        }

        List<Transform> bones = new List<Transform>();

        List<CombineInstance> combines = new List<CombineInstance>();
        List<Material> materials = new List<Material>();
        for (int i = 0; i < bodys.Length; i++)
        {
            CombineInstance combine = new CombineInstance();
            combine.mesh = bodys[i].GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh;
            combine.transform = bodys[i].transform.localToWorldMatrix;
            combines.Add(combine);

            materials.Add(bodys[i].GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial);

            Transform[] bodybones = bodys[i].GetComponentInChildren<SkinnedMeshRenderer>().bones;

            for (int j = 0; j < bodybones.Length; j++)
            {
                bones.Add(directory[bodybones[j].name]);
            }
        }

        Mesh mesh = new Mesh();
        mesh.CombineMeshes(combines.ToArray(), false);
        GetComponent<SkinnedMeshRenderer>().sharedMesh = mesh;
        GetComponent<SkinnedMeshRenderer>().materials = materials.ToArray();
        GetComponent<SkinnedMeshRenderer>().bones = bones.ToArray();
    }
    
    void Update()
    {
        
    }
}
