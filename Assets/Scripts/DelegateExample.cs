using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateExample : MonoBehaviour
{
    private void Start()
    {
        TimerManager.MyDelg d =
            () =>
            {
                FunctionWithIntArgs("first time");
            };

        d.Invoke();
        d = FunctionCall;

        string s = "second time";
        TimerManager.Instance.AddDelegateTimer(() => { FunctionWithIntArgs(s); }, 3f, 5);
        TimerManager.Instance.AddDelegateTimer(d, 6f, 1);
    }

    private void Update()
    {
        TimerManager.Instance.Refresh();
    }

    private void FunctionCall()
    {
        print("message");
    }

    private void FunctionWithIntArgs(string a)
    {
        print("You argument is : " + a);
    }
}
