using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Text;

public class LoginUser : MonoBehaviour
{
    public TMP_InputField edtUser, edtPass; 
    public TMP_Text txtError;
    public Selectable first;
    private EventSystem ev;
    public Button btnLogin;
    public static LoginResponseModel loginresponsemodel;
    // Start is called before the first frame update
    void Start()
    {
        edtUser.text = "trannhan2552@gmail.com";
        edtPass.text = "123";
        ev = EventSystem.current;
        first.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            btnLogin.onClick.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = ev.currentSelectedGameObject
                .GetComponent<Selectable>()
                .FindSelectableOnDown();
            if (next != null) next.Select();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Selectable next = ev.currentSelectedGameObject
                .GetComponent<Selectable>()
                .FindSelectableOnUp();
            if (next != null) next.Select();
        }
    }
    public void Checklogin()
    {



        StartCoroutine(Login());
        Login();
    }


    IEnumerator Login()
    {
        //…
        var user = edtUser.text;
        var pass = edtPass.text;

        Usermodel usermodel = new Usermodel(user, pass);
        // model thành chuỗi json
        string jsonStringRequest = JsonConvert.SerializeObject(usermodel);


        var request = new UnityWebRequest("http://localhost:3000/login", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        // nhận giá trị là json
        request.SetRequestHeader("Content-Type", "application/json");
        Debug.Log(request);

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
            loginresponsemodel = JsonConvert.DeserializeObject<LoginResponseModel>(jsonString);
            if (loginresponsemodel.status == 1)
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                txtError.text = loginresponsemodel.notification;
            }
            request.Dispose();
        }
    }
}
