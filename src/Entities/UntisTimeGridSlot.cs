#region ENBREA UNTIS.XML - Copyright (C) 2020 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA UNTIS.XML
 *    
 *    Copyright (C) 2020 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using System;

namespace Enbrea.Untis.Xml
{
    /// <summary>
    /// Represents together woith <see cref="UntisTimeGrid"/>  the Untis XML type "XmlTimegridSlot" 
    /// </summary>
    public class UntisTimeGridSlot : UntisEntity
    {
        public DayOfWeek Day { get; set; }
        public TimeSpan EndTime { get; set; }
        public string ForeignKey { get; set; }
        public string Label { get; set; }
        public uint Period { get; set; }
        public TimeSpan StartTime { get; set; }
    }
}
