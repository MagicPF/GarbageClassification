using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_rubbish : MonoBehaviour
{
    bool isDrage = false;
    bool isMe = false;
    private GameObject obj;
    // Start is called before the first frame update
    void OnMouseDrag(){
        if (Input.GetMouseButton(0))
            isDrage = true;
        else{
            isDrage = false;
            isMe = false;
            Global.currentRub = "null";
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit)){
            obj = hit.collider.gameObject;
            if (Global.currentRub.Equals("null") && obj.layer.Equals(8))
                Global.currentRub = obj.name;
            if (obj.name.Equals(gameObject.name) && obj.name.Equals(Global.currentRub))
                isMe = true;
        }
        if (isDrage && isMe){
            //获取到鼠标的位置(鼠标水平的输入和竖直的输入以及距离)
            float bin_z = 220;
            float rub_y = Input.mousePosition.y > 320 ? Input.mousePosition.y : 320;
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, rub_y, bin_z);
            //Debug.Log(Input.mousePosition.y);
            //物体的位置，屏幕坐标转换为世界坐标
            Vector3 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            //把鼠标位置传给物体
            transform.position = objectPosition;
        }
    }

    void Start(){
        Global.currentRub = "null";
    }
    private void OnCollisionEnter(Collision collision){
        if (!collision.collider.tag.Contains("bin") || isDrage)
            return;
        bool flag = false;
        if (gameObject.tag.Contains("recycle") && collision.collider.tag.Contains("recycle"))
            flag = true;
        else if (gameObject.tag.Contains("wet") && collision.collider.tag.Contains("wet"))
            flag = true;
        else if (gameObject.tag.Contains("harmful") && collision.collider.tag.Contains("harmful"))
            flag = true;
        else if (gameObject.tag.Contains("dry") && collision.collider.tag.Contains("dry"))
            flag = true;
        if(flag == true)
            Global.Glo_score = Global.Glo_score += 5;
        else
            Global.Glo_score = Global.Glo_score -=3;
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update(){
        OnMouseDrag();
        Debug.Log(Global.currentRub);
    }
}
