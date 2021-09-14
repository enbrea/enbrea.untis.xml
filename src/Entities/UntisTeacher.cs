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
    /// Represents the Untis XML type "XmlTeacher" 
    /// </summary>
    public class UntisTeacher : UntisEntityWithShortName
    {
        public string Alias { get; set; }
        public string BackgroundColor { get; set; }
        public DateTime? Birthdate { get; set; }
        public string DepartmentId { get; set; }
        public string DescriptionId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string ForegroundColor { get; set; }
        public string ForeignKey { get; set; }
        public UntisGender? Gender { get; set; }
        public string LastName { get; set; }
        public string LongName { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string RoomId { get; set; }
        public string Status { get; set; }
        public string Text { get; set; }
        public string Text2 { get; set; }
        public string Title { get; set; }
    }
}
