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
    /// Represents the Untis XML type "XmlStudent" 
    /// </summary>
    public class UntisStudent : UntisEntityWithShortName
    {
        public string BackgroundColor { get; set; }
        public DateTime? Birthdate { get; set; }
        public string ClassId { get; set; }
        public string DescriptionId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string ForegroundColor { get; set; }
        public string ForeignKey { get; set; }
        public UntisGender? Gender { get; set; }
        public string IdNumber { get; set; }
        public string LastName { get; set; }
        public string LongName { get; set; }
        public string Text { get; set; }
    }
}
