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

namespace Enbrea.Untis.Xml
{
    /// <summary>
    /// Represents the Untis XML type "XmlLessonsTableEntry"
    /// </summary>
    public class UntisLessonsTableEntry
    {
        public uint? MaxPeriods { get; set; }
        public uint? MinPeriods { get; set; }
        public string SubjectId { get; set; }
    }
}
