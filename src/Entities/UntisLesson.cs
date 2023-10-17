#region ENBREA UNTIS.XML - Copyright (C) 2023 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA UNTIS.XML
 *    
 *    Copyright (C) 2023 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using System;
using System.Collections.Generic;

namespace Enbrea.Untis.Xml
{
    /// <summary>
    /// Represents the Untis XML type "XmlLesson" 
    /// </summary>
    public class UntisLesson : UntisEntityWithShortName
    {
        public string BackgroundColor { get; set; }

        public string Block { get; set; }

        public List<string> ClassIds { get; set; }

        public string DateSchemeId { get; set; }
        
        public TimeSpan? Duration { get; set; }
        
        public DateOnly? EndDate { get; set; }
        
        public string ForegroundColor { get; set; }
        
        public string ForeignKey { get; set; }
        
        public string Occurence { get; set; }
        
        public uint Slots { get; set; }
        
        public DateOnly? StartDate { get; set; }
        
        public List<string> StudentGroupIds { get; set; }

        public List<string> StudentIds { get; set; }

        public string SubjectId { get; set; }

        public string TeacherId { get; set; }

        public string Text { get; set; }

        public string TimeGridId { get; set; }

        public List<UntisLessonTime> Times { get; set; }

        public DateOnly ValidFrom { get; set; }

        public DateOnly ValidTo { get; set; }

        public UntisPeriodicWeek? Week { get; set; }
    }
}