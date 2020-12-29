using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public class Service_Time : IGameService {

        public double TimeStamp {
            get { return GetTimeStamp(DateTime.Now); }
        }

        private DateTime GetTime(long timestamp) {
            DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long ITime = timestamp * 10000000;
            TimeSpan toNow = new TimeSpan(ITime);
            return dateTimeStart.Add(toNow);
        }

        public double GetTimeStamp(DateTime time) {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (time - startTime).TotalSeconds;
        }

        void System.IDisposable.Dispose() {
        }

    }
}