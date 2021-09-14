#region ENBREA UNTIS.XML - Copyright (C) 2021 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA UNTIS.XML
 *    
 *    Copyright (C) 2021 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using System;

namespace Enbrea.Untis.Xml
{
    /// <summary>
    /// Represents the Untis XML type "XmlHoliday" 
    /// </summary>
    public class UntisHoliday : UntisEntityWithShortName
    {
        public DateTime EndTime { get; set; }
        public string LongName { get; set; }
        public DateTime StartTime { get; set; }
        public UntisHolidayType Type { get; set; }
    }
}
