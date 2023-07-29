using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterRespnseModel
{

    public RegisterRespnseModel(int status, string notification)
    {
        this.status = status;
        this.notification = notification;
    }

    public int status { get; set; }
    public string notification { get; set; }
}
