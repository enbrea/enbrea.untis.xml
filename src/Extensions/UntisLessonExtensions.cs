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
using System.Collections.Generic;

namespace Enbrea.Untis.Xml
{
    /// <summary>
    /// Extensions for <see cref="UntisLesson"/>
    /// </summary>
    public static class UntisLessonExtensions
    {
        public static IEnumerable<DateTime> GetDateInstances(this UntisLesson lesson, DateTime OccurenceStartDate, DayOfWeek day)
        {
            if ((!string.IsNullOrEmpty(lesson.Occurence)) && (lesson.ValidFrom != null) && (lesson.ValidTo != null))
            {
                var startDate = (DateTime)lesson.ValidFrom;
                var endDate = (DateTime)lesson.ValidTo;
                var currentDate = startDate;

                if (currentDate.DayOfWeek > day)
                {
                    currentDate = currentDate.AddDays(7).AddDays(day - startDate.DayOfWeek);
                }
                else if (currentDate.DayOfWeek < day)
                {
                    currentDate = currentDate.AddDays(day - startDate.DayOfWeek);
                }

                while (currentDate <= endDate)
                {
                    var d = (currentDate - OccurenceStartDate).Days;

                    if ((lesson.Occurence.Length >= d) && (lesson.Occurence[d] == '1'))
                    {
                        yield return currentDate;
                    }

                    currentDate = currentDate.AddDays(7);
                }
            }
        }
    }
}