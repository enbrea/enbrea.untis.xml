#region ENBREA UNTIS.XML - Copyright (C) STÜBER SYSTEMS GmbH
/*    
 *    ENBREA UNTIS.XML
 *    
 *    Copyright (C) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using System;

namespace Enbrea.Untis.Xml
{
    /// <summary>
    /// Represents the Untis XML type "XmlTeacherQualification" 
    /// </summary>
    public class UntisTeacherQualification
    {
        public string SubjectId { get; set; }
        public string FromLevel { get; set; }
        public string ToLevel { get; set; }
    }
}
