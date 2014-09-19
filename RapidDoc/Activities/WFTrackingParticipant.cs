using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Activities.Tracking;

namespace RapidDoc.Activities
{
    public class WFTrackingParticipant : TrackingParticipant
    {
        public IDictionary<string, object> Outputs { get; set; }

        protected override void Track(TrackingRecord record, TimeSpan timeout)
        {
            if (record != null)
            {
                if (record is CustomTrackingRecord)
                {
                    var customTrackingRecord = record as CustomTrackingRecord;
                    Outputs = customTrackingRecord.Data;
                }
            }
        }
    }
}
