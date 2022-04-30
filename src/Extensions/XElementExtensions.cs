#region ENBREA UNTIS.XML - Copyright (C) 2022 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA UNTIS.XML
 *    
 *    Copyright (C) 2022 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Enbrea.Untis.Xml
{
    /// <summary>
    /// Extensions for <see cref="XElement"/>
    /// </summary>
    public static class XElementExtensions
    {
        public const string UntisDateFormat = "yyyyMMdd";
        public const string UntisDurationFormat = "hh:mm";
        public const string UntisTimeFormat = "hhmm";

        public static DateOnly GetDate(this XElement xElement, string name)
        {
            var subElement = xElement.Elements().FirstOrDefault(x => x.Name.LocalName == name);
            if (subElement != null)
            {
                return DateOnly.ParseExact(subElement.Value,
                    UntisDateFormat,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None);
            }
            else
            {
                throw new InvalidDataException($"Value not found.");
            }
        }

        public static DateOnly GetDateOrDefault(this XElement xElement, string name)
        {
            var subElement = xElement.Elements().FirstOrDefault(x => x.Name.LocalName == name);
            if (subElement != null)
            {
                return DateOnly.ParseExact(subElement.Value,
                    UntisDateFormat,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None);
            }
            else
            {
                return default;
            }
        }

        public static DayOfWeek GetDay(this XElement xElement, string name)
        {
            var subElement = xElement.Elements().FirstOrDefault(x => x.Name.LocalName == name);
            if (subElement != null)
            {
                return subElement.Value switch
                {
                    "1" => DayOfWeek.Monday,
                    "2" => DayOfWeek.Tuesday,
                    "3" => DayOfWeek.Wednesday,
                    "4" => DayOfWeek.Thursday,
                    "5" => DayOfWeek.Friday,
                    "6" => DayOfWeek.Saturday,
                    "7" => DayOfWeek.Sunday,
                    _ => throw new Exception("Error")
                };
            }
            else
            {
                throw new InvalidDataException($"Value not found.");
            }
        }

        public static TimeSpan? GetDurationOrDefault(this XElement xElement, string name)
        {
            var subElement = xElement.Elements().FirstOrDefault(x => x.Name.LocalName == name);
            if (subElement != null)
            {
                return TimeSpan.ParseExact(subElement.Value,
                    UntisDurationFormat,
                    CultureInfo.InvariantCulture,
                    TimeSpanStyles.None);
            }
            else
            {
                return default;
            }
        }

        public static UntisGender? GetGenderOrDefault(this XElement xElement, string name)
        {
            var subElement = xElement.Elements().FirstOrDefault(x => x.Name.LocalName == name);
            if (subElement != null)
            {
                return subElement.Value switch
                {
                    "F" => UntisGender.Female,
                    "M" => UntisGender.Male,
                    "D" => UntisGender.Divers,
                    _ => null
                };
            }
            else
            {
                return default;
            }
        }

        public static UntisHolidayType GetHolidayType(this XElement xElement, string name)
        {
            var subElement = xElement.Elements().FirstOrDefault(x => x.Name.LocalName == name);
            if (subElement != null)
            {
                return subElement.Value switch
                {
                    "Public holiday" => UntisHolidayType.Public,
                    "School holidays" => UntisHolidayType.School,
                    _ => throw new Exception("Error")
                };
            }
            else
            {
                throw new InvalidDataException($"Value not found.");
            }
        }

        public static List<UntisLessonsTableEntry> GetLessonTableSubjects(this XElement xElement, string name)
        {
            var subjectList = new List<UntisLessonsTableEntry>();
            var subElement = xElement.Elements().FirstOrDefault(x => x.Name.LocalName == name);
            if (subElement != null)
            {
                foreach (var xmlElement in subElement.Elements())
                {
                    subjectList.Add(new UntisLessonsTableEntry
                    {
                        SubjectId = xmlElement.Attribute("id").Value,
                        MinPeriods = xmlElement.Attribute("min_periods") != null ? uint.Parse(xmlElement.Attribute("min_periods").Value) : (uint?)null,
                        MaxPeriods = xmlElement.Attribute("min_periods") != null ? uint.Parse(xmlElement.Attribute("max_periods").Value) : (uint?)null
                    }); ;
                }
            }
            return subjectList;
        }

        public static UntisPeriodicWeek? GetPeriodicWeekOrDefault(this XElement xElement, string name)
        {
            var subElement = xElement.Elements().FirstOrDefault(x => x.Name.LocalName == name);
            if (subElement != null)
            {
                var xmlElement = subElement.Elements().FirstOrDefault();
                if (xmlElement != null)
                {
                    return xmlElement.Name.LocalName switch
                    {
                        "WeekA" => UntisPeriodicWeek.WeekA,
                        "WeekB" => UntisPeriodicWeek.WeekB,
                        "WeekC" => UntisPeriodicWeek.WeekC,
                        "WeekD" => UntisPeriodicWeek.WeekD,
                        "WeekE" => UntisPeriodicWeek.WeekE,
                        "WeekF" => UntisPeriodicWeek.WeekF,
                        "WeekG" => UntisPeriodicWeek.WeekG,
                        "WeekH" => UntisPeriodicWeek.WeekH,
                        "WeekI" => UntisPeriodicWeek.WeekI,
                        "WeekJ" => UntisPeriodicWeek.WeekJ,
                        "WeekK" => UntisPeriodicWeek.WeekK,
                        "WeekL" => UntisPeriodicWeek.WeekL,
                        "WeekM" => UntisPeriodicWeek.WeekM,
                        "WeekN" => UntisPeriodicWeek.WeekN,
                        "WeekO" => UntisPeriodicWeek.WeekO,
                        "WeekP" => UntisPeriodicWeek.WeekP,
                        _ => throw new NotSupportedException($"Value {xmlElement.Name.LocalName} is not supported.")
                    };
                }
                else
                {
                    return default;
                }
            }
            else
            {
                return default;
            }
        }

        public static UntisPeriodicWeekSet GetPeriodicWeeks(this XElement xElement, string name)
        {
            var result = (UntisPeriodicWeekSet)0;
            var subElement = xElement.Elements().FirstOrDefault(x => x.Name.LocalName == name);
            if (subElement != null)
            {
                foreach (var xmlElement in subElement.Elements())
                {
                    result = xmlElement.Name.LocalName switch
                    {
                        "WeekA" => result | UntisPeriodicWeekSet.WeekA,
                        "WeekB" => result | UntisPeriodicWeekSet.WeekB,
                        "WeekC" => result | UntisPeriodicWeekSet.WeekC,
                        "WeekD" => result | UntisPeriodicWeekSet.WeekD,
                        "WeekE" => result | UntisPeriodicWeekSet.WeekE,
                        "WeekF" => result | UntisPeriodicWeekSet.WeekF,
                        "WeekG" => result | UntisPeriodicWeekSet.WeekG,
                        "WeekH" => result | UntisPeriodicWeekSet.WeekH,
                        "WeekI" => result | UntisPeriodicWeekSet.WeekI,
                        "WeekJ" => result | UntisPeriodicWeekSet.WeekJ,
                        "WeekK" => result | UntisPeriodicWeekSet.WeekK,
                        "WeekL" => result | UntisPeriodicWeekSet.WeekL,
                        "WeekM" => result | UntisPeriodicWeekSet.WeekM,
                        "WeekN" => result | UntisPeriodicWeekSet.WeekN,
                        "WeekO" => result | UntisPeriodicWeekSet.WeekO,
                        "WeekP" => result | UntisPeriodicWeekSet.WeekP,
                        _ => throw new NotSupportedException($"Value {xmlElement.Name.LocalName} is not supported.")
                    };
                }
            }
            return result; 
        }

        public static string GetReferenceId(this XElement xElement, string name)
        {
            var subElement = xElement.Elements().FirstOrDefault(x => x.Name.LocalName == name);
            if (subElement != null)
            {
                return subElement.Attribute("id").Value;
            }
            else
            {
                throw new InvalidDataException($"Reference not found.");
            }
        }

        public static List<string> GetReferenceIdArray(this XElement xElement, string name, string prefix)
        {
            var idList = new List<string>();
            var subElement = xElement.Elements().FirstOrDefault(x => x.Name.LocalName == name);
            if (subElement != null)
            {
                foreach (Match match in Regex.Matches(subElement.Attribute("id").Value.Trim(), $"(?<id>({prefix}_)(.(?!{prefix}))*)"))
                {
                    idList.Add(match.Groups["id"].Value);
                }
            }
            return idList;
        }

        public static List<string> GetReferenceIdList(this XElement xElement, string name)
        {
            var idList = new List<string>();
            var subElement = xElement.Elements().FirstOrDefault(x => x.Name.LocalName == name);
            if (subElement != null)
            {
                foreach (var xmlElement in subElement.Elements())
                {
                    idList.Add(xmlElement.Attribute("id").Value);
                }
            }
            return idList;
        }

        public static string GetReferenceIdOrDefault(this XElement xElement, string name)
        {
            var subElement = xElement.Elements().FirstOrDefault(x => x.Name.LocalName == name);
            if (subElement != null)
            {
                return subElement.Attribute("id").Value;
            }
            else
            {
                return default;
            }
        }

        public static TimeSpan GetTime(this XElement xElement, string name)
        {
            var subElement = xElement.Elements().FirstOrDefault(x => x.Name.LocalName == name);
            if (subElement != null)
            {
                return TimeSpan.ParseExact(subElement.Value,
                    UntisTimeFormat,
                    CultureInfo.InvariantCulture,
                    TimeSpanStyles.None);
            }
            else
            {
                throw new InvalidDataException($"Value not found.");
            }
        }

        public static List<UntisLessonTime> GetTimeElements(this XElement xElement, string name)
        {
            var lessonTimeList = new List<UntisLessonTime>();
            var subElement = xElement.Elements().FirstOrDefault(x => x.Name.LocalName == name);
            if (subElement != null)
            {
                var linkedLessonTimeList = new List<UntisLessonTime>();
                foreach (var xmlElement in subElement.Elements())
                {
                    var newTimeElement = new UntisLessonTime
                    {
                        Day = xmlElement.GetDay("assigned_day"),
                        Slot = xmlElement.GetUIntOrDefault("assigned_period"),
                        StartTime = xmlElement.GetTime("assigned_starttime"),
                        EndTime = xmlElement.GetTime("assigned_endtime"),
                        RoomIds = xmlElement.GetReferenceIdArray("assigned_room", "RM")
                    };
                    var prevLessonTime = linkedLessonTimeList.LastOrDefault();
                    if ((prevLessonTime != null) && (prevLessonTime.Day == newTimeElement.Day) && (prevLessonTime.Slot == newTimeElement.Slot - 1))
                    {
                        foreach (var linkedLessonTime in linkedLessonTimeList)
                        {
                            linkedLessonTime.SlotGroupLastSlot = newTimeElement.Slot;
                            linkedLessonTime.SlotGroupEndTime = newTimeElement.EndTime;
                        }
                        newTimeElement.SlotGroupFirstSlot = prevLessonTime.SlotGroupFirstSlot != null ? prevLessonTime.SlotGroupFirstSlot : prevLessonTime.Slot;
                        newTimeElement.SlotGroupStartTime = prevLessonTime.SlotGroupFirstSlot != null ? prevLessonTime.SlotGroupStartTime : prevLessonTime.StartTime;
                    }
                    else 
                    {
                        linkedLessonTimeList.Clear();
                    }
                    linkedLessonTimeList.Add(newTimeElement);
                    lessonTimeList.Add(newTimeElement);
                }
            }
            return lessonTimeList;
        }

        public static uint GetUInt(this XElement xElement, string name)
        {
            var subElement = xElement.Elements().FirstOrDefault(x => x.Name.LocalName == name);
            if (subElement != null)
            {
                return uint.Parse(subElement.Value);
            }
            else
            {
                throw new InvalidDataException($"Value not found.");
            }
        }

        public static uint GetUIntOrDefault(this XElement xElement, string name)
        {
            var subElement = xElement.Elements().FirstOrDefault(x => x.Name.LocalName == name);
            if (subElement != null)
            {
                return uint.Parse(subElement.Value);
            }
            else
            {
                return default;
            }
        }

        public static string GetValue(this XElement xElement, string name)
        {
            var subElement = xElement.Elements().FirstOrDefault(x => x.Name.LocalName == name);
            if (subElement != null)
            {
                return subElement.Value;
            }
            else
            {
                throw new InvalidDataException($"Value not found.");
            }
        }

        public static string GetValueOrDefault(this XElement xElement, string name)
        {
            var subElement = xElement.Elements().FirstOrDefault(x => x.Name.LocalName == name);
            if (subElement != null)
            {
                return subElement.Value;
            }
            else
            {
                return default;
            }
        }
    }
}