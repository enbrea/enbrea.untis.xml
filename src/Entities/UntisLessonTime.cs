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
    /// Represents the Untis XML type "XmlLessonTime" 
    /// </summary>
    public class UntisLessonTime
    {
        /// <summary>
        /// Weekday
        /// </summary>
        public DayOfWeek Day { get; set; }

        /// <summary>
        /// End time
        /// </summary>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// Room reference
        /// </summary>
        public string RoomId { get; set; }

        /// <summary>
        /// Time slot 
        /// </summary>
        public uint Slot { get; set; }

        /// <summary>
        /// If this lesson time is part of a larger slot group (e.g. a double slot) this shows 
        /// the slot group end time
        /// </summary>
        public TimeSpan? SlotGroupEndTime { get; set; }

        /// <summary>
        /// If this lesson time is part of a larger slot group (e.g. a double slot) this points to 
        /// the first slot within the slot group
        /// </summary>
        public uint? SlotGroupFirstSlot { get; set; }

        /// <summary>
        /// If this lesson time is part of a larger slot group (e.g. a double slot) this points to 
        /// the last slot within the slot group
        /// </summary>
        public uint? SlotGroupLastSlot { get; set; }

        /// <summary>
        /// If this lesson time is part of a larger slot group (e.g. a double slot) this shows 
        /// the slot group start time
        /// </summary>
        public TimeSpan? SlotGroupStartTime { get; set; }

        /// <summary>
        /// Start time
        /// </summary>
        public TimeSpan StartTime { get; set; }
    }
}
