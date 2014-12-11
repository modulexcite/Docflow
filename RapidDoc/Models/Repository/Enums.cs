using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RapidDoc.Attributes;

namespace RapidDoc.Models.Repository
{
    public enum DocumentState : byte
    {
        [LocalizedDescAttributre("Created", typeof(RapidDoc.Resources.Enums.Enums))]
        Created = 0,

        [LocalizedDescAttributre("Agreement", typeof(RapidDoc.Resources.Enums.Enums))]
        Agreement = 1,

        [LocalizedDescAttributre("Completed", typeof(RapidDoc.Resources.Enums.Enums))]
        Completed = 2,

        [LocalizedDescAttributre("Cancelled", typeof(RapidDoc.Resources.Enums.Enums))]
        Cancelled = 3,

        [LocalizedDescAttributre("Execution", typeof(RapidDoc.Resources.Enums.Enums))]
        Execution = 4,

        [LocalizedDescAttributre("Closed", typeof(RapidDoc.Resources.Enums.Enums))]
        Closed = 5
    }

    public enum TrackerType : byte
    {
        Waiting = 0,
        Approved = 1,
        Cancelled = 2,
        NonActive = 3
    }

    public enum SLAStatusList : byte
    {
        NoWarning = 0,
        Warning = 1,
        Disturbance = 2
    }

    public enum DateType : byte
    {
        [LocalizedDescAttributre("WorkingDay", typeof(RapidDoc.Resources.Enums.Enums))]
        WorkingDay = 0,

        [LocalizedDescAttributre("DayOff", typeof(RapidDoc.Resources.Enums.Enums))]
        DayOff = 1,
    }

    public enum EmailTemplateType : byte
    {
        Default = 0,
        Comment = 1,
        Delegation = 2,
        SLAStatus = 3
    }

    public enum HistoryType : byte
    {
        NewDocument = 0,
        ApproveDocument = 1,
        CancelledDocument = 2,
        NewComment = 3,
        AddReader = 4,
        RemoveReader = 5
    }

    public enum FilterType : byte
    {
        Role = 0,

        Login = 1,

        Predicate = 2,

        Other = 3
    }
}