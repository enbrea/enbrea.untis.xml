#region ENBREA UNTIS.XML - Copyright (C) 2022 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA UNTIS.XML 
 *    
 *    Copyright (C) 2022 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using System;

namespace Enbrea.Untis.Xml
{
    /// <summary>
    /// Represents the Untis XML type "XmlClass"
    /// </summary>
    public class UntisClass : UntisEntityWithShortName
    {
        public string Alias { get; set; }
        public string BackgroundColor { get; set; }
        public string DepartmentId { get; set; }
        public string DescriptionId { get; set; }
        public string ForegroundColor { get; set; }
        public string ForeignKey { get; set; }
        public string LessonTableId { get; set; }
        public string Level { get; set; }
        public string LongName { get; set; }
        public string RoomId { get; set; }
        public string TeacherId { get; set; }
        public string Text { get; set; }
        public string TimeGridId { get; set; }
        public DateOnly? ValidFrom { get; set; }
        public DateOnly? ValidTo { get; set; }
    }
}
