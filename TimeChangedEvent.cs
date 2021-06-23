﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BagageSystem_WPF
{
    class TimeChangedEvent : EventArgs
    {
        private DateTime currentTime;

        public DateTime CurrentTime
        {
            get { return currentTime; }
            set { currentTime = value; }
        }

        public TimeChangedEvent(DateTime currentTime)
        {
            this.currentTime = currentTime;
        }
    }
}
