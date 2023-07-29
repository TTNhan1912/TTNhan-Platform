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

public class RegisterUser : MonoBehaviour
{
    public TMP_InputField edtUser, edtPass;
    public TMP_Text txtError;
    public Selectable first;
    private EventSystem ev;
    public Button btnRegister;
    // Start is called before the first frame update
    void Start()
    {
        ev = EventSystem.current;
        first.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            btnRegister.onClick.Invoke();
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
    public void Checkregister()
    {

        var user = edtUser.text;
        var pass = edtPass.text;

        Registermodel registermodel = new Registermodel(user, pass);
        StartCoroutine(Register(registermodel));
        Register(registermodel);
    }
    IEnumerator Register(Registermodel registermodel)
    {
        //…

        // model thành chuỗi json
        string jsonStringRequest = JsonConvert.SerializeObject(registermodel);

        var request = new UnityWebRequest("http://localhost:3000/register", "POST");
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
            RegisterRespnseModel registerRespnseModel = JsonConvert.DeserializeObject<RegisterRespnseModel>(jsonString);
            if (registerRespnseModel.status == 1)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                txtError.text = registerRespnseModel.notification;
            }
            request.Dispose();
        }
    }
}
