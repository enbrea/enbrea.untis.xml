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

using System.Collections.Generic;

namespace Enbrea.Untis.Xml
{
    /// <summary>
    /// Represents the Untis XML type "XmlStudentGroup" 
    /// </summary>
    public class UntisStudentGroup : UntisEntityWithShortName
    {
        public string BackgroundColor { get; set; }
        public List<string> ClassIds { get; set; }
        public string ForegroundColor { get; set; }
        public string LongName { get; set; }
        public string SubjectId { get; set; }
    }
}
