using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Activities.Tracking;
using RapidDoc.Models.Repository;
using RapidDoc.Models.DomainModels;


namespace RapidDoc.Activities
{
    public sealed class WFWaitingPersonBookmark : NativeActivity
    {
        public WFWaitingPersonBookmark() : base() { }
        public InArgument<DocumentState> inputStep { get; set; }
        public InArgument<string> inputBookmarkName { get; set; }
        public OutArgument<DocumentState> outputStep { get; set; }
        public OutArgument<string> outputCurrentUser { get; set; }
        public OutArgument<Dictionary<String, Object>> outputDocumentData { get; set; }

        protected override bool CanInduceIdle
        {
            get
            {
                return true;
            }
        }

        NativeActivityContext CreateTrackingRecord(DocumentState stepParametr, NativeActivityContext context)
        {
            var customRecord = new CustomTrackingRecord("RespondActivityRecord");
            customRecord.Data.Add("outputStep", context.GetValue<DocumentState>(inputStep));
            context.Track(customRecord);
            return context;
        }

        protected override void Execute(NativeActivityContext context)
        {
            context = CreateTrackingRecord(context.GetValue<DocumentState>(inputStep), context);
            context.CreateBookmark(context.GetValue<string>(inputBookmarkName), 
                new BookmarkCallback(this.resumeBookmark));
        }

        void resumeBookmark(NativeActivityContext context, Bookmark bookmark, object obj)
        {
            context = CreateTrackingRecord(context.GetValue<DocumentState>(inputStep), context);
            IDictionary<string, object> inputArguments = (IDictionary<string, object>)obj;
            context.SetValue(outputStep, (DocumentState)inputArguments["inputStep"]);
            context.SetValue(outputCurrentUser, (string)inputArguments["inputCurrentUser"]);
            context.SetValue(outputDocumentData, (Dictionary<String, Object>)inputArguments["documentData"]);
        }

    }
}
