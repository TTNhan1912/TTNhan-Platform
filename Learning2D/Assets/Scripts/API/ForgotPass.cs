using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;

public class ForgotPass : MonoBehaviour
{
    [SerializeField] private TMP_InputField textUser, textOtp, textNewPass, textConfirm;
    [SerializeField] private TMP_Text textError;
    [SerializeField] private GameObject reset, send, login;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // gửi OTP
    public void Send()
    {
        var user = textUser.text;
        OTPmodel otpmodel = new OTPmodel(user);

        StartCoroutine(SendOTP(otpmodel));
        SendOTP(otpmodel);
    }

        IEnumerator SendOTP(OTPmodel otpmodel)
    {
        //…
        string jsonStringRequest = JsonConvert.SerializeObject(otpmodel);

        var request = new UnityWebRequest("https://hoccungminh.dinhnt.com/fpt/send-otp", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            var jsonString = request.downloadHandler.text.ToString();
            RegisterRespnseModel scoreResponseModel = JsonConvert.DeserializeObject<RegisterRespnseModel>(jsonString);
            if(scoreResponseModel.status == 1)
            {
                // load panel rs
                reset.SetActive(true);
                send.SetActive(false);
            }
            else
            {
                textError.text = scoreResponseModel.notification;
               
            }
        }
        request.Dispose();
    }

    public void Reset()
    {
        var newPass = textNewPass.text;
        var confirmPass = textConfirm.text;

        if (newPass.Equals(confirmPass))
        {
            var user = textUser.text;
            int otp = int.Parse(textOtp.text);
            ResetPassModel resetPassmodel = new ResetPassModel(user, newPass, otp);
            StartCoroutine(ResetPassAPI(resetPassmodel));
            ResetPassAPI(resetPassmodel);
        }
        else
        {
            Debug.Log("nhập sai xác nhận mật khẩu !!!!!!!!!");
        }
    }

    // goi API rsPass
    IEnumerator ResetPassAPI(ResetPassModel resetPassmodel)
    {
        //…
        string jsonStringRequest = JsonConvert.SerializeObject(resetPassmodel);

        var request = new UnityWebRequest("https://hoccungminh.dinhnt.com/fpt/reset-password", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            var jsonString = request.downloadHandler.text.ToString();
            RegisterRespnseModel scoreResponseModel = JsonConvert.DeserializeObject<RegisterRespnseModel>(jsonString);
            if (scoreResponseModel.status == 1)
            {
                reset.SetActive(false);
                login.SetActive(true);
            }
            else
            {
                //  hiển thị lỗi
                textError.text = scoreResponseModel.notification;
            }
        }
        request.Dispose();
    }


}
