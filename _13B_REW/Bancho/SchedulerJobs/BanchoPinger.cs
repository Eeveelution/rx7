using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _13B_REW.Bancho.Managers;
using _13B_REW.Bancho.Packets;
using EeveeTools.Utilities.PollingJobScheduler;

namespace _13B_REW.Bancho.SchedulerJobs {
    public class BanchoPinger : AsyncSchedulableJob {
        public override TimeSpan ExecuteTimeout { get; } = TimeSpan.FromSeconds(15);
        public override async Task ExecuteJob() {
            foreach (ClientOsu clientOsu in ClientManager.ClientsByUserId.Values) {
                clientOsu.Ping();
            }
        }
    }
}
