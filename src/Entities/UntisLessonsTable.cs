#region ENBREAUNTIS.XML  - Copyright (C) 2020 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA UNTIS.XML
 *    
 *    Copyright (C) 2020 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using System.Collections.Generic;

namespace Enbrea.Untis.Xml
{
    /// <summary>
    /// Represents the Untis XML type "XmlLessonsTable" 
    /// </summary>
    public class UntisLessonsTable : UntisEntityWithShortName
    {
        public string BackgroundColor { get; set; }
        public List<UntisLessonsTableEntry> Entries { get; set; }
        public string ForegroundColor { get; set; }
        public string LongName { get; set; }
        public string Text { get; set; }
    }
}
