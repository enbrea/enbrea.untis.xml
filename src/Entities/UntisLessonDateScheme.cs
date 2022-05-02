#region ENBREA UNTIS.XML - Copyright (C) 2022 STÜBER SYSTEMS GmbH
/*
 *    ENBREA UNTIS.XML
 *
 *    Copyright (C) 2022 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0.
 *
 */

#endregion ENBREA - Copyright (C) 2022 STÜBER SYSTEMS GmbH

namespace Enbrea.Untis.Xml
{
    /// <summary>
    /// Represents the Untis XML type "XmlLessonDateScheme" 
    /// </summary>
    public class UntisLessonDateScheme : UntisEntityWithShortName
    {
        public string LongName { get; set; }
        public string DateScheme { get; set; }
        public UntisPeriodicWeekSet PeriodicWeeks { get; set; }
    }
}