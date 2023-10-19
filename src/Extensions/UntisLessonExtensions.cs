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

using System;
using System.Collections.Generic;

namespace Enbrea.Untis.Xml
{
    /// <summary>
    /// Extensions for <see cref="UntisLesson"/>
    /// </summary>
    public static class UntisLessonExtensions
    {
        public static IEnumerable<DateOnly> GetDateInstances(this UntisLesson lesson, DateOnly OccurenceStartDate, DayOfWeek day)
        {
            static int DaysBetween(DateOnly d1, DateOnly d2)
            {
                return (d1.ToDateTime(TimeOnly.MinValue) - d2.ToDateTime(TimeOnly.MinValue)).Days;
            }

            if (!string.IsNullOrEmpty(lesson.Occurence))
            {
                var startDate = lesson.ValidFrom;
                var endDate = lesson.ValidTo;
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
                    var d = DaysBetween(currentDate, OccurenceStartDate);

                    if ((lesson.Occurence.Length >= d) && (lesson.Occurence[d] == '1'))
                    {
                        yield return currentDate;
                    }

                    currentDate = currentDate.AddDays(7);
                }
            }
        }

        public static DateOnly GetFirstDateInstance(this UntisLesson lesson, DayOfWeek day)
        {
            var startDate = lesson.ValidFrom;
            var currentDate = startDate;

            if (currentDate.DayOfWeek > day)
            {
                currentDate = currentDate.AddDays(7).AddDays(day - startDate.DayOfWeek);
            }
            else if (currentDate.DayOfWeek < day)
            {
                currentDate = currentDate.AddDays(day - startDate.DayOfWeek);
            }

            return currentDate;
        }
    }
}