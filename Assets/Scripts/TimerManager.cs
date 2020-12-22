using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager
{
    #region Singleton
    private static TimerManager instance;

    public static TimerManager Instance
    {
        get
        {
            if (instance == null) 
                instance = new TimerManager();
            return instance;
        }
    }
    #endregion

    public delegate void MyDelg();

    List<DelegateTimer> timers = new List<DelegateTimer>();

    public void AddDelegateTimer(MyDelg myDelg, float time, int callCount)
    {
        DelegateTimer timer = new DelegateTimer(myDelg, time + Time.time, callCount);
        timers.Add(timer);
    }

    public void Refresh()
    {
        for (int i = timers.Count-1; i >= 0; i--)
        {
            if(timers[i].invokeTime <= Time.time)
            {
                if(timers[i].callCount > 0)
                {
                    for (int r = 0; r < timers[i].callCount; r++)
                    {
                        try
                        {
                            timers[i].delegateToInvoke.Invoke();
                        }
                        catch(System.Exception e)
                        {
                            Debug.LogError("Error on invocation: " + e.ToString());
                        }
                    }
                }

                timers.RemoveAt(i);
            }
        }
    }

    private class DelegateTimer
    {
        public float invokeTime;
        public int callCount;
        public MyDelg delegateToInvoke;

        public DelegateTimer(MyDelg myDelg, float time, int callCount)
        {
            this.invokeTime = time;
            this.callCount = callCount;
            this.delegateToInvoke = myDelg;
        }
    }
}
