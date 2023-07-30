using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static Cinemachine.CinemachineTargetGroup;

public class Item : MonoBehaviour
{
    private int cheries = 0;
    private int kiwi = 0;
    private int dautay = 0;

    [SerializeField] private Text cheriesText;
    [SerializeField] private Text kiwiText;
    [SerializeField] private Text daytayText;

    [SerializeField] private AudioSource soundCollectEffect;

     void Start()
    {

        // tk mới tạo k có điểm
        /*if (LoginUser.loginresponsemodel.score >= 0)
        {
            // load lại điểm
            cheries = LoginUser.loginresponsemodel.score;
            kiwi = LoginUser.loginresponsemodel.score;
            dautay = LoginUser.loginresponsemodel.score;
            cheriesText.text = cheries + " ";
            kiwiText.text = kiwi + " ";
            daytayText.text = dautay + " ";
        }*/

/*        // tk mới tạo k có vị trí
        if (LoginUser.loginresponsemodel.positionX != "")
        {
            // load vị trí, ép về kiểu float vì vị trí là float
            var posX = float.Parse(LoginUser.loginresponsemodel.positionX);
            var posY = float.Parse(LoginUser.loginresponsemodel.positionY);
            var posZ = float.Parse(LoginUser.loginresponsemodel.positionZ);

            transform.position = new Vector3(posX, posY, posZ);
        }*/

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            soundCollectEffect.Play();
            Destroy(collision.gameObject);
            cheries++;
            cheriesText.text = " " + cheries;
        }
        if (collision.gameObject.CompareTag("Kiwi"))
        {
            soundCollectEffect.Play();
            Destroy(collision.gameObject);
            kiwi++;
            kiwiText.text = " " + kiwi;
        }
        if (collision.gameObject.CompareTag("DauTay"))
        {
            soundCollectEffect.Play();
            Destroy(collision.gameObject);
            dautay++;
            daytayText.text = " " + dautay;
        }
        else if (collision.gameObject.CompareTag("Save"))
        {
            Debug.Log("Save>>>>>");
            Saveposition();
        }
    }



    public void Save()
    {
        var user = LoginUser.loginresponsemodel.username;
        Scoremodel scoremodel = new Scoremodel(user, cheries,kiwi,dautay);

        StartCoroutine(SaveAPI(scoremodel));
        SaveAPI(scoremodel);
    }

    // lưu điểm
    IEnumerator SaveAPI(Scoremodel scoremodel)
    {
        //…

        // model thành chuỗi json
        string jsonStringRequest = JsonConvert.SerializeObject(scoremodel);

        var request = new UnityWebRequest("http://localhost:3000/saveScore", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        // nhận giá trị là json
        request.SetRequestHeader("Content-Type", "application/json");

        // trả về kết quả xong check 
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            // .text.toString vì nó sẽ trả về cho mình 1 chuỗi
            var jsonString = request.downloadHandler.text.ToString();
            // chuỗi json thành model
            ScoreResponseModel scoreResponseModel = JsonConvert.DeserializeObject<ScoreResponseModel>(jsonString);
            if (scoreResponseModel.status == 1)
            {
                
            }
            else
            {
            
            }

            request.Dispose();
        }
    }


    // lưu vị trí
    public void Saveposition()
    {
        var user = LoginUser.loginresponsemodel.username;
        var x = transform.position.x;
        var y = transform.position.y;
        var z = transform.position.z;
        PositionModel positionmodel = new PositionModel(user, x.ToString(), y.ToString(), z.ToString());

        StartCoroutine(SavePositionAPI(positionmodel));
        SavePositionAPI(positionmodel);
    }

    IEnumerator SavePositionAPI(PositionModel positionmodel)
    {
        //…

        // model thành chuỗi json
        string jsonStringRequest = JsonConvert.SerializeObject(positionmodel);

        var request = new UnityWebRequest("http://localhost:3000/savePosition", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        // nhận giá trị là json
        request.SetRequestHeader("Content-Type", "application/json");

        // trả về kết quả xong check 
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            // .text.toString vì nó sẽ trả về cho mình 1 chuỗi
            var jsonString = request.downloadHandler.text.ToString();
            // chuỗi json thành model
            ScoreResponseModel scoreResponseModel = JsonConvert.DeserializeObject<ScoreResponseModel>(jsonString);
            if (scoreResponseModel.status == 1)
            {
                Debug.Log(positionmodel.positionX + positionmodel.positionY);
            }
            else
            {

            }
            request.Dispose();
        }
    }

}
