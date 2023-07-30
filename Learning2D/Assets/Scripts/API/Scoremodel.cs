using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoremodel
{
    public Scoremodel(string username, int score1, int score2, int score3)
    {
        this.username = username;
        this.score1 = score1;
        this.score2 = score2;
        this.score3 = score3;
    }

    public string username { get; set; }
    public int score1 { get; set; }
    public int score2 { get; set; }
    public int score3 { get; set; }
}
