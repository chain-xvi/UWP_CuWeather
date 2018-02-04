using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace BackgroundWeatherRetrieving
{
    public sealed class BackgroundWeatherRetrievalTask : IBackgroundTask
    {
        BackgroundTaskDeferral deferral;
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            deferral = taskInstance.GetDeferral();



            deferral.Complete();
        }
    }
}
