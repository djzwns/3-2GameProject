using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

    Transform player;
    new Transform camera;

    public float fXSmooth;
    public float fYSmooth;

    public float fXMargin;
    public float fYMargin;

    public float fCamPosY;

    //Vector2 vMaxXY;
    //Vector2 vMinXY;

    float fCamtoPlayer;

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        camera = transform;
        fCamtoPlayer = Mathf.Abs(player.position.z - camera.position.z);

        //// 백그라운드의 bound 를 받아옴
        //Bounds bgBounds = GameObject.Find("background").GetComponent<Collider>().bounds;

        // 카메라 왼쪽위, 오른쪽 아래의 값을 월드 좌표로 받아옴
        //Vector3 vCamTopLeft = GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0, 0, 0));
        //Vector3 vCamBottomRight = GetComponent<Camera>().ViewportToWorldPoint(new Vector3(1, 1, 0));

        //// 받아온 정보들로 Max Min 좌표 값 입력
        //vMaxXY.x = bgBounds.max.x - vCamBottomRight.x;
        //vMaxXY.y = bgBounds.max.y - vCamBottomRight.y;

        //vMinXY.x = bgBounds.min.x - vCamTopLeft.x;
        //vMinXY.y = bgBounds.min.y - vCamTopLeft.y;
    }

    void FixedUpdate()
    {
        float fCamX = camera.position.x;
        float fCamY = camera.position.y;

        // 무조건 따라가지 않음, 캐릭터가 어느정도 움직여야 따라가게
        if(CheckX())
            fCamX = Mathf.Lerp(camera.position.x, player.position.x, fXSmooth * Time.deltaTime);

        if(CheckY())
            fCamY = Mathf.Lerp(camera.position.y, player.position.y, fYSmooth * Time.deltaTime);

        //// min max 값
        //fCamX = Mathf.Clamp(fCamX, vMinXY.x, vMaxXY.x);
        //fCamY = Mathf.Clamp(fCamY, vMinXY.y, vMaxXY.y);

        // 최종 좌표로 카메라 이동
        transform.position = new Vector3(fCamX, fCamY + fCamPosY, player.position.z - fCamtoPlayer);
    }

    bool CheckX()
    {
        return Mathf.Abs(camera.position.x - player.position.x) > fXMargin;
    }

    bool CheckY()
    {
        return Mathf.Abs(camera.position.y - player.position.y) > fYMargin;
    }
}
