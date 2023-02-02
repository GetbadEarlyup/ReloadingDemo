using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Rotationchart : MonoBehaviour,IDragHandler
{
    //更多请访问：CSDN作孽就得先起床
    public CreatNewModule combine;
    public Transform player;
    public Image prefab;
    public int num;
    public float spacing;

    float c;//宽
    float r;//半径
    float ang;//每个角的弧度
    float dis = 0;//拖动的距离
    float max = 1;//缩放最大值   到正面（近大远小）
    float min = 0.5f;//缩放最小值 到背面

    List<GameObject> list= new List<GameObject>();
    List<Transform> sorts= new List<Transform>();

    void Start()
    {
        c = (prefab.rectTransform.rect.width + spacing) * num;
        r = c / (2*Mathf.PI);
        ang = (2 * Mathf.PI) / num;
        player.transform.localScale = new Vector3(1, 1, (max - min) * 0.5f + min);//改变了他的z值让他原地不动
        sorts.Add(player);
        Move();

    }

    public void Move() 
    {
        float moveang = dis / r;

        for (int i = 0; i < num; i++)
        {
            float x = Mathf.Sin(i * ang + moveang) * r;//正弦 返回  x？
            float z = Mathf.Cos(i * ang + moveang) * r;//余弦 返回  y？

            float p = (z + r) / (r+r);

            p = 1 - p;

            float scale = (max - min) * p + min;



            if (list.Count<=i) 
            {
                GameObject image = Instantiate(prefab.gameObject,transform);
                image.GetComponent<Image>().sprite=Resources.Load<Sprite>("Image_"+i);
                int n = i;
                //轮转图切换
                image.GetComponent<Button>().onClick.AddListener(() => 
                {
                    
                        switch (n / 2)
                        {

                            case 0:
                                {
                                    combine.touid = n % 2;
                                
                                    break;
                                }
                            case 1:
                                {
                                    combine.yifuid = n % 2;
                                    break;
                                }
                            case 2:
                                {
                                    combine.uiid = n % 2;

                                    break;
                                }
                            default: break;


                        }
                        combine.Change();
                    
                   
                  
                });

                image.name=i.ToString();
                list.Add(image);
                sorts.Add(image.transform);
            }

            list[i].transform.localPosition = Vector3.right * x;//控制位置
            list[i].transform.localScale=Vector3.one*scale;//控制缩放

        }
       
        sorts.Sort((a, b) => //以x/z排序
        {
            if (a.localScale.z < b.localScale.z)
            {
                return -1;
            }
            else if (a.localScale.z == b.localScale.z)
            {
                return 0;
            }
            else 
            {
                return 1;
            }

        });
        for (int i = 0; i < sorts.Count; i++)//调换顺序
        {
            sorts[i].SetSiblingIndex(i);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void OnDrag(PointerEventData eventData)
    {
        dis -= eventData.delta.x;//变化位置信息 更换拖动距离
        Move();
    }

    
}
