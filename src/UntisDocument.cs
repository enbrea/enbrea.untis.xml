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
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Enbrea.Untis.Xml
{
    /// <summary>
    /// An object representing the content of a xml export file from Untis
    /// </summary>
    public class UntisDocument
    {
        public List<UntisClass> Classes = [];
        public List<UntisDepartment> Departments = [];
        public List<UntisDescription> Descriptions = [];
        public UntisGeneralSettings GeneralSettings = new();
        public List<UntisHoliday> Holidays = [];
        public List<UntisLessonDateScheme> LessonDateSchemes = [];
        public List<UntisLesson> Lessons = [];
        public List<UntisLessonsTable> LessonsTables = [];
        public List<UntisReductionReason> ReductionReasons = [];
        public List<UntisReduction> Reductions = [];
        public List<UntisRoomGroup> RoomGroups = [];
        public List<UntisRoom> Rooms = [];
        public List<UntisStudentGroup> StudentGroups = [];
        public List<UntisStudent> Students = [];
        public List<UntisSubject> Subjects = [];
        public List<UntisTeacher> Teachers = [];
        public List<UntisTimeGrid> TimeGrids = [];

        private readonly string ClassXPathExpr = "//xs:document/xs:classes/xs:class";
        private readonly string DepartmentXPathExpr = "//xs:document/xs:departments/xs:department";
        private readonly string DescriptionXPathExpr = "//xs:document/xs:descriptions/xs:description";
        private readonly string GeneralSettingsXPathExpr = "//xs:document/xs:general";
        private readonly string HolidayXPathExpr = "//xs:document/xs:holidays/xs:holiday";
        private readonly string LessonDateSchemeXPathExpr = "//xs:document/xs:lesson_date_schemes/xs:lesson_date_scheme";
        private readonly string LessonsTableXPathExpr = "//xs:document/xs:lesson_tables/xs:lesson_table";
        private readonly string LessonXPathExpr = "//xs:document/xs:lessons/xs:lesson";
        private readonly string ReductionReasonXPathExpr = "//xs:document/xs:reduction_reasons/xs:reduction_reason";
        private readonly string ReductionXPathExpr = "//xs:document/xs:reductions/xs:reduction";
        private readonly string RoomGroupXPathExpr = "//xs:document/xs:roomgroups/xs:roomgroup";
        private readonly string RoomXPathExpr = "//xs:document/xs:rooms/xs:room";
        private readonly string StudentGroupXPathExpr = "//xs:document/xs:studentgroups/xs:studentgroup";
        private readonly string StudentXPathExpr = "//xs:document/xs:students/xs:student";
        private readonly string SubjectXPathExpr = "//xs:document/xs:subjects/xs:subject";
        private readonly string TeacherXPathExpr = "//xs:document/xs:teachers/xs:teacher";
        private readonly string TimeGridSlotPathExpr = "//xs:document/xs:timeperiods/xs:timeperiod";

        /// <summary>
        /// Initializes a new instance of the <see cref="UntisDocument"/> class.
        /// </summary>
        /// <param name="xmlDoc">XML document</param>
        public UntisDocument(XDocument xmlDoc)
        {
            var xmlNamespaceManager = new XmlNamespaceManager(new NameTable());
            xmlNamespaceManager.AddNamespace("xs", "https://untis.at/untis/XmlInterface");

            ReadElements(xmlDoc, xmlNamespaceManager, GeneralSettingsXPathExpr, (e) => ReadGeneralSettings(e));
            ReadElements(xmlDoc, xmlNamespaceManager, ClassXPathExpr, (e) => ReadClasses(e));
            ReadElements(xmlDoc, xmlNamespaceManager, DepartmentXPathExpr, (e) => ReadDepartments(e));
            ReadElements(xmlDoc, xmlNamespaceManager, DescriptionXPathExpr, (e) => ReadDescriptions(e));
            ReadElements(xmlDoc, xmlNamespaceManager, HolidayXPathExpr, (e) => ReadHolidays(e));
            ReadElements(xmlDoc, xmlNamespaceManager, LessonDateSchemeXPathExpr, (e) => ReadLessonDateSchemes(e));
            ReadElements(xmlDoc, xmlNamespaceManager, LessonsTableXPathExpr, (e) => ReadLessonsTables(e));
            ReadElements(xmlDoc, xmlNamespaceManager, LessonXPathExpr, (e) => ReadLessons(e));
            ReadElements(xmlDoc, xmlNamespaceManager, RoomGroupXPathExpr, (e) => ReadRoomGroups(e));
            ReadElements(xmlDoc, xmlNamespaceManager, RoomXPathExpr, (e) => ReadRooms(e));
            ReadElements(xmlDoc, xmlNamespaceManager, StudentGroupXPathExpr, (e) => ReadStudentGroups(e));
            ReadElements(xmlDoc, xmlNamespaceManager, StudentXPathExpr, (e) => ReadStudents(e));
            ReadElements(xmlDoc, xmlNamespaceManager, SubjectXPathExpr, (e) => ReadSubjects(e));
            ReadElements(xmlDoc, xmlNamespaceManager, TeacherXPathExpr, (e) => ReadTeachers(e));
            ReadElements(xmlDoc, xmlNamespaceManager, ReductionXPathExpr, (e) => ReadReductions(e));
            ReadElements(xmlDoc, xmlNamespaceManager, ReductionReasonXPathExpr, (e) => ReadReductionReasons(e));
            ReadElements(xmlDoc, xmlNamespaceManager, TeacherXPathExpr, (e) => ReadTeachers(e));
            ReadElements(xmlDoc, xmlNamespaceManager, TimeGridSlotPathExpr, (e) => ReadTimeGridSlots(e, GeneralSettings.TermBeginDate));
        }

        /// <summary>
        /// Creates a new instance of a <see cref="UntisDocument"/> class by loading a xml stream 
        /// </summary>
        /// <param name="xmlStream">XML stream</param>
        /// <returns>New instance of a <see cref="UntisDocument"/> class</returns>
        public static UntisDocument Load(Stream xmlStream)
        {
            return new UntisDocument(XDocument.Load(xmlStream, LoadOptions.None));
        }

        /// <summary>
        /// Creates a new instance of a <see cref="UntisDocument"/> class by loading a xml file
        /// </summary>
        /// <param name="xmlFile">XML file info</param>
        /// <returns>New instance of a <see cref="UntisDocument"/> class</returns>
        public static UntisDocument Load(FileInfo xmlFile)
        {
            using var xmlStream = new FileStream(xmlFile.FullName, FileMode.Open);
            return Load(xmlStream);
        }

        /// <summary>
        /// Creates a new instance of a <see cref="UntisDocument"/> class by loading a xml file
        /// </summary>
        /// <param name="xmlFile">XML file name</param>
        /// <returns>New instance of a <see cref="UntisDocument"/> class</returns>
        public static UntisDocument Load(string xmlFile)
        {
            return Load(new FileInfo(xmlFile));
        }

        /// <summary>
        /// Creates a new instance of a <see cref="UntisDocument"/> class by loading a xml stream 
        /// </summary>
        /// <param name="xmlStream">XML stream</param>
        /// <returns>New instance of a <see cref="UntisDocument"/> class</returns>
        public static async Task<UntisDocument> LoadAsync(Stream xmlStream)
        {
            return new UntisDocument(await XDocument.LoadAsync(xmlStream, LoadOptions.None, default));
        }

        /// <summary>
        /// Creates a new instance of a <see cref="UntisDocument"/> class by loading a xml file
        /// </summary>
        /// <param name="xmlFile">XML file info</param>
        /// <returns>New instance of a <see cref="UntisDocument"/> class</returns>
        public static async Task<UntisDocument> LoadAsync(FileInfo xmlFile)
        {
            using var xmlStream = new FileStream(xmlFile.FullName, FileMode.Open);
            return await LoadAsync(xmlStream);
        }

        /// <summary>
        /// Creates a new instance of a <see cref="UntisDocument"/> class by loading a xml file
        /// </summary>
        /// <param name="xmlFile">XML file name</param>
        /// <returns>New instance of a <see cref="UntisDocument"/> class</returns>
        public static async Task<UntisDocument> LoadAsync(string xmlFile)
        {
            return await LoadAsync(new FileInfo(xmlFile));
        }

        /// <summary>
        /// Maps XML node to <see cref="UntisClass"/> instance
        /// </summary>
        /// <param name="xmlElement">XML node</param>
        private void ReadClasses(XElement xmlElement)
        {
            Classes.Add(new UntisClass
            {
                Id = xmlElement.Attribute("id").Value,
                LongName = xmlElement.GetValueOrDefault("longname"),
                Alias = xmlElement.GetValueOrDefault("alias"),
                ForegroundColor = xmlElement.GetValueOrDefault("forecolor"),
                BackgroundColor = xmlElement.GetValueOrDefault("backcolor"),
                Text = xmlElement.GetValueOrDefault("text"),
                RoomId = xmlElement.GetReferenceIdOrDefault("class_room"),
                DepartmentId = xmlElement.GetReferenceIdOrDefault("class_department"),
                DescriptionId = xmlElement.GetReferenceIdOrDefault("class_description"),
                TeacherId = xmlElement.GetReferenceIdOrDefault("class_teacher"),
                LessonTableId = xmlElement.GetReferenceIdOrDefault("lessonstable"),
                Level = xmlElement.GetValueOrDefault("classlevel"),
                ValidFrom = xmlElement.GetDateOrDefault("begindate"),
                ValidTo = xmlElement.GetDateOrDefault("eenddate"),
                ForeignKey = xmlElement.GetValueOrDefault("foreignkey"),
                TimeGridId = xmlElement.GetValueOrDefault("timegrid"),
                NumberOfFemaleStudents = xmlElement.GetUIntOrDefault("studentsfemale"),
                NumberOfMaleStudents = xmlElement.GetUIntOrDefault("studentsmale"),
                GroupNumber = xmlElement.GetValueOrDefault("class_group_number"),
                MasterClassId = xmlElement.GetValueOrDefault("master_class"),
                ExternalName = xmlElement.GetValueOrDefault("external_name"),
                Flags = xmlElement.GetValueOrDefault("flags")
            });
        }

        /// <summary>
        /// Maps XML node to <see cref="UntisDepartment"/> instance
        /// </summary>
        /// <param name="xmlElement">XML node</param>
        private void ReadDepartments(XElement xmlElement)
        {
            Departments.Add(new UntisDepartment
            {
                Id = xmlElement.Attribute("id").Value,
                LongName = xmlElement.GetValueOrDefault("longname"),
                ForegroundColor = xmlElement.GetValueOrDefault("forecolor"),
                BackgroundColor = xmlElement.GetValueOrDefault("backcolor"),
                ForeignKey = xmlElement.GetValueOrDefault("foreignkey")
            });
        }

        /// <summary>
        /// Maps XML node to <see cref="UntisDescription"/> instance
        /// </summary>
        /// <param name="xmlElement">XML node</param>
        private void ReadDescriptions(XElement xmlElement)
        {
            Descriptions.Add(new UntisDescription
            {
                Id = xmlElement.Attribute("id").Value,
                LongName = xmlElement.GetValueOrDefault("longname"),
                ForegroundColor = xmlElement.GetValueOrDefault("forecolor"),
                BackgroundColor = xmlElement.GetValueOrDefault("backcolor"),
                ForeignKey = xmlElement.GetValueOrDefault("foreignkey"),
                Flags = xmlElement.GetValueOrDefault("flags")
            });
        }

        /// <summary>
        /// Iterates through XML document by XPath query and executes for every found xml node the 
        /// given action
        /// </summary>
        /// <param name="xmlDoc">XML document</param>
        /// <param name="xmlNamespaceManager">XML namespace</param>
        /// <param name="xPathExpression">XPath query expression</param>
        /// <param name="action">Action for found XML nodes</param>
        private void ReadElements(
            XDocument xmlDoc,
            XmlNamespaceManager xmlNamespaceManager,
            string xPathExpression,
            Action<XElement> action)
        {
            var xmlElements = xmlDoc.XPathSelectElements(xPathExpression, xmlNamespaceManager);

            foreach (XElement xmlElement in xmlElements)
            {
                action(xmlElement);
            }
        }

        /// <summary>
        /// Maps XML node to <see cref="UntisGeneralSettings"/> instance
        /// </summary>
        /// <param name="xmlElement">XML node</param>
        private void ReadGeneralSettings(XElement xmlElement)
        {
            GeneralSettings.SchoolName = xmlElement.GetValueOrDefault("schoolname");
            GeneralSettings.SchoolNo = xmlElement.GetValueOrDefault("schoolnumber");
            GeneralSettings.SchoolType = xmlElement.GetValueOrDefault("schooltype");
            GeneralSettings.SchoolYearBeginDate = xmlElement.GetDate("schoolyearbegindate");
            GeneralSettings.SchoolYearEndDate = xmlElement.GetDate("schoolyearenddate");
            GeneralSettings.TermName = xmlElement.GetValueOrDefault("termname");
            GeneralSettings.TermBeginDate = xmlElement.GetDate("termbegindate");
            GeneralSettings.TermEndDate = xmlElement.GetDate("termenddate");
            GeneralSettings.TermEndDate = xmlElement.GetDate("termenddate");
            GeneralSettings.WeekPeriodicity = xmlElement.GetUIntOrDefault("week_periodicity");
        }

        /// <summary>
        /// Maps XML node to <see cref="UntisHoliday"/> instance
        /// </summary>
        /// <param name="xmlElement">XML node</param>
        private void ReadHolidays(XElement xmlElement)
        {
            Holidays.Add(new UntisHoliday
            {
                Id = xmlElement.Attribute("id").Value,
                Type = xmlElement.GetHolidayType("type"),
                LongName = xmlElement.GetValue("longname"),
                StartTime = xmlElement.GetDate("starttime"),
                EndTime = xmlElement.GetDate("endtime")
            });
        }

        /// <summary>
        /// Maps XML node to <see cref="UntisLessonDateScheme"/> instance
        /// </summary>
        /// <param name="xmlElement">XML node</param>
        private void ReadLessonDateSchemes(XElement xmlElement)
        {
            LessonDateSchemes.Add(new UntisLessonDateScheme
            {
                Id = xmlElement.Attribute("id").Value,
                LongName = xmlElement.GetValueOrDefault("longname"),
                DateScheme = xmlElement.GetValue("date_scheme"),
                PeriodicWeeks = xmlElement.GetPeriodicWeeks("periodic_weeks")
            });
        }

        /// <summary>
        /// Maps XML node to <see cref="UntisLesson"/> instance
        /// </summary>
        /// <param name="xmlElement">XML node</param>
        private void ReadLessons(XElement xmlElement)
        {
            Lessons.Add(new UntisLesson
            {
                Id = xmlElement.Attribute("id").Value,
                Slots = xmlElement.GetUIntOrDefault("periods"),
                Duration = xmlElement.GetDurationOrDefault("duration"),
                TeacherId = xmlElement.GetReferenceIdOrDefault("lesson_teacher"),
                TeacherValue = xmlElement.GetUIntOrDefault("teacher_value"),
                TeacherStatCode= xmlElement.GetValueOrDefault("teacher_statcode"),
                SubjectId = xmlElement.GetReferenceIdOrDefault("lesson_subject"),
                ClassIds = xmlElement.GetReferenceIdArray("lesson_classes", "CL"),
                StudentGroupIds = xmlElement.GetReferenceIdArray("lesson_studentgroups", "SG"),
                StudentIds = xmlElement.GetReferenceIdArray("lesson_students", "ST"),
                DateSchemeId = xmlElement.GetReferenceIdOrDefault("lesson_date_scheme"),
                StartDate = xmlElement.GetDateOrDefault("begindate"),
                EndDate = xmlElement.GetDateOrDefault("enddate"),
                ValidFrom = xmlElement.GetDate("effectivebegindate"),
                ValidTo = xmlElement.GetDate("effectiveenddate"),
                Block = xmlElement.GetValueOrDefault("block"),
                Week = xmlElement.GetPeriodicWeekOrDefault("week"),
                TimeGridId = xmlElement.GetValueOrDefault("timegrid"),
                Occurence = xmlElement.GetValueOrDefault("occurence"),
                ForegroundColor = xmlElement.GetValueOrDefault("forecolor"),
                BackgroundColor = xmlElement.GetValueOrDefault("backcolor"),
                Times = xmlElement.GetTimeElements("times", xmlElement.GetDate("effectivebegindate")),
                StatCodes = xmlElement.GetValueOrDefault("statcodes"),
                Periods = xmlElement.GetUIntOrDefault("periods"),
                YearlyPeriods = xmlElement.GetUIntOrDefault("yearly_periods"),
                Description = xmlElement.GetValueOrDefault("lesson_description"),
                Text = xmlElement.GetValueOrDefault("text"),
                Text1 = xmlElement.GetValueOrDefault("text1"),
                Text2 = xmlElement.GetValueOrDefault("text2"),
                Flags = xmlElement.GetValueOrDefault("flags"),
                ForeignKey = xmlElement.GetValueOrDefault("foreignkey"),
                ForeignData = xmlElement.GetValueOrDefault("foreigndata")
            });
        }

        /// <summary>
        /// Maps XML node to <see cref="UntisLessonsTable"/> instance
        /// </summary>
        /// <param name="xmlElement">XML node</param>
        private void ReadLessonsTables(XElement xmlElement)
        {
            LessonsTables.Add(new UntisLessonsTable
            {
                Id = xmlElement.Attribute("id").Value,
                LongName = xmlElement.GetValueOrDefault("longname"),
                Entries = xmlElement.GetLessonTableSubjects("lesson_table_subjects"),
                ForegroundColor = xmlElement.GetValueOrDefault("forecolor"),
                BackgroundColor = xmlElement.GetValueOrDefault("backcolor")
            });
        }

        /// <summary>
        /// Maps XML node to <see cref="UntisReductionReason"/> instance
        /// </summary>
        /// <param name="xmlElement">XML node</param>
        private void ReadReductionReasons(XElement xmlElement)
        {
            ReductionReasons.Add(new UntisReductionReason
            {
                Id = xmlElement.Attribute("id").Value,
                LongName = xmlElement.GetValueOrDefault("longname"),
                Description = xmlElement.GetValueOrDefault("reduction_description"),
                Text = xmlElement.GetValueOrDefault("text"),
                ForegroundColor = xmlElement.GetValueOrDefault("forecolor"),
                BackgroundColor = xmlElement.GetValueOrDefault("backcolor")
            });
        }

        /// <summary>
        /// Maps XML node to <see cref="UntisReduction"/> instance
        /// </summary>
        /// <param name="xmlElement">XML node</param>
        private void ReadReductions(XElement xmlElement)
        {
            Reductions.Add(new UntisReduction
            {
                Id = xmlElement.Attribute("id").Value,
                TeacherId = xmlElement.GetValueOrDefault("reduction_teacher"),
                ReasonId = xmlElement.GetValueOrDefault("reduction_reason"),
                Value = xmlElement.GetDouble("value"),
                Text = xmlElement.GetValueOrDefault("text"),
                BeginDate = xmlElement.GetDateOrDefault("begindate"),
                EndDate = xmlElement.GetDateOrDefault("enddate")
            });
        }

        /// <summary>
        /// Maps XML node to <see cref="UntisRoomGroup"/> instance
        /// </summary>
        /// <param name="xmlElement">XML node</param>
        private void ReadRoomGroups(XElement xmlElement)
        {
            RoomGroups.Add(new UntisRoomGroup
            {
                Id = xmlElement.Attribute("id").Value,
                LongName = xmlElement.GetValueOrDefault("longname"),
                RoomIds = xmlElement.GetReferenceIdList("rooms"),
                ForegroundColor = xmlElement.GetValueOrDefault("forecolor"),
                BackgroundColor = xmlElement.GetValueOrDefault("backcolor"),
                ForeignKey = xmlElement.GetValueOrDefault("foreignkey"),
                ForeignData = xmlElement.GetValueOrDefault("foreigndata")
            });
        }

        /// <summary>
        /// Maps XML node to <see cref="UntisRoom"/> instance
        /// </summary>
        /// <param name="xmlElement">XML node</param>
        private void ReadRooms(XElement xmlElement)
        {
            Rooms.Add(new UntisRoom
            {
                Id = xmlElement.Attribute("id").Value,
                LongName = xmlElement.GetValueOrDefault("longname"),
                ForegroundColor = xmlElement.GetValueOrDefault("forecolor"),
                BackgroundColor = xmlElement.GetValueOrDefault("backcolor"),
                GroupId = xmlElement.GetReferenceIdOrDefault("roomgroups"),
                DescriptionId = xmlElement.GetReferenceIdOrDefault("room_description"),
                DepartmentId = xmlElement.GetReferenceIdOrDefault("room_department"),
                Text = xmlElement.GetValueOrDefault("text"),
                ExternalName = xmlElement.GetValueOrDefault("external_name"),
                ForeignKey = xmlElement.GetValueOrDefault("foreignkey"),
                Capacity = xmlElement.GetUIntOrDefault("capacity"),
                Flags = xmlElement.GetValueOrDefault("flags"),
            });
        }

        /// <summary>
        /// Maps XML node to <see cref="UntisStudentGroup"/> instance
        /// </summary>
        /// <param name="xmlElement">XML node</param>
        private void ReadStudentGroups(XElement xmlElement)
        {
            StudentGroups.Add(new UntisStudentGroup
            {
                Id = xmlElement.Attribute("id").Value,
                LongName = xmlElement.GetValueOrDefault("longname"),
                ClassIds = xmlElement.GetReferenceIdList("classes"),
                SubjectId = xmlElement.GetReferenceIdOrDefault("subject"),
                ForegroundColor = xmlElement.GetValueOrDefault("forecolor"),
                BackgroundColor = xmlElement.GetValueOrDefault("backcolor")
            });
        }

        /// <summary>
        /// Maps XML node to <see cref="UntisStudent"/> instance
        /// </summary>
        /// <param name="xmlElement">XML node</param>
        private void ReadStudents(XElement xmlElement)
        {
            Students.Add(new UntisStudent
            {
                Id = xmlElement.Attribute("id").Value,
                LongName = xmlElement.GetValueOrDefault("longname"),
                LastName = xmlElement.GetValueOrDefault("surname"),
                FirstName = xmlElement.GetValueOrDefault("forename"),
                Gender = xmlElement.GetGenderOrDefault("gender"),
                Birthdate = xmlElement.GetDateOrDefault("birthdate"),
                IdNumber = xmlElement.GetValueOrDefault("idnumber"),
                Email = xmlElement.GetValueOrDefault("email"),
                ClassId = xmlElement.GetReferenceIdOrDefault("student_class"),
                DescriptionId = xmlElement.GetReferenceIdOrDefault("student_description"),
                ForegroundColor = xmlElement.GetValueOrDefault("forecolor"),
                BackgroundColor = xmlElement.GetValueOrDefault("backcolor"),
                ForeignKey = xmlElement.GetValueOrDefault("foreignkey"),
                Text = xmlElement.GetValueOrDefault("text"),
                Flags = xmlElement.GetValueOrDefault("flags")
            });
        }

        /// <summary>
        /// Maps XML node to <see cref="UntisSubject"/> instance
        /// </summary>
        /// <param name="xmlElement">XML node</param>
        private void ReadSubjects(XElement xmlElement)
        {
            Subjects.Add(new UntisSubject
            {
                Id = xmlElement.Attribute("id").Value,
                LongName = xmlElement.GetValueOrDefault("longname"),
                Alias = xmlElement.GetValueOrDefault("alias"),
                ForegroundColor = xmlElement.GetValueOrDefault("forecolor"),
                BackgroundColor = xmlElement.GetValueOrDefault("backcolor"),
                Group = xmlElement.GetValueOrDefault("subjectgroup"),
                Text = xmlElement.GetValueOrDefault("text"),
                RoomId = xmlElement.GetReferenceIdOrDefault("subject_room"),
                DescriptionId = xmlElement.GetReferenceIdOrDefault("subject_description"),
                ForeignKey = xmlElement.GetValueOrDefault("foreignkey"),
                ForeignData = xmlElement.GetValueOrDefault("foreigndata"),
                Flags = xmlElement.GetValueOrDefault("flags"),
            });
        }

        /// <summary>
        /// Maps XML node to <see cref="UntisTeacher"/> instance
        /// </summary>
        /// <param name="xmlElement">XML node</param>
        private void ReadTeachers(XElement xmlElement)
        {
            Teachers.Add(new UntisTeacher
            {
                Id = xmlElement.Attribute("id").Value,
                LongName = xmlElement.GetValueOrDefault("longname"),
                Alias = xmlElement.GetValueOrDefault("alias"),
                FirstName = xmlElement.GetValueOrDefault("forename"),
                LastName = xmlElement.GetValueOrDefault("surname"),
                Title = xmlElement.GetValueOrDefault("title"),
                Gender = xmlElement.GetGenderOrDefault("gender"),
                Birthdate = xmlElement.GetDateOrDefault("birthdate"),
                Status = xmlElement.GetValueOrDefault("status"),
                Email = xmlElement.GetValueOrDefault("email"),
                Phone = xmlElement.GetValueOrDefault("phone"),
                Mobile = xmlElement.GetValueOrDefault("cellphonenumber"),
                ForegroundColor = xmlElement.GetValueOrDefault("forecolor"),
                BackgroundColor = xmlElement.GetValueOrDefault("backcolor"),
                Text = xmlElement.GetValueOrDefault("text"),
                Text2 = xmlElement.GetValueOrDefault("text2"),
                DescriptionId = xmlElement.GetReferenceIdOrDefault("teacher_description"),
                DepartmentId = xmlElement.GetReferenceIdOrDefault("teacher_department"),
                RoomId = xmlElement.GetReferenceIdOrDefault("teacher_room"),
                Qualifications = xmlElement.GetTeacherQualifications("teacher_qualifications"),
                ForeignKey = xmlElement.GetValueOrDefault("foreignkey"),
                StaffNo = xmlElement.GetValueOrDefault("payrollnumber"),
                StaffNo2 = xmlElement.GetValueOrDefault("personnel_number_2"),
                WeekTarget = xmlElement.GetValueOrDefault("weektarget"),
                ExternalName = xmlElement.GetValueOrDefault("external_name")
            });
        }

        /// <summary>
        /// Maps XML node to <see cref="UntisTimeGridSlot"/> instance
        /// </summary>
        /// <param name="xmlElement">XML node</param>
        /// <param name="startDate">Reference date for weekday calculation</param>
        private void ReadTimeGridSlots(XElement xmlElement, DateOnly startDate)
        {
            var timeGridName = xmlElement.GetValueOrDefault("timegrid");

            var timeGrid = TimeGrids.Find(x => x.Name == timeGridName);

            if (timeGrid == null)
            {
                timeGrid = new UntisTimeGrid() { Name = timeGridName };
                TimeGrids.Add(timeGrid);
            }

            timeGrid.Slots.Add(new UntisTimeGridSlot
            {
                Id = xmlElement.Attribute("id").Value,
                Period = xmlElement.GetUInt("period"),
                Day = xmlElement.GetDay("day", startDate),
                StartTime = xmlElement.GetTime("starttime"),
                EndTime = xmlElement.GetTime("endtime"),
                Label = xmlElement.GetValueOrDefault("label"),
                ForeignKey = xmlElement.GetValueOrDefault("foreignkey"),
            });
        }
    }
}
