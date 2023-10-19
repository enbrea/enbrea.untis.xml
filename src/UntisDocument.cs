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
        public List<UntisClass> Classes = new();
        public List<UntisDepartment> Departments = new();
        public List<UntisDescription> Descriptions = new();
        public UntisGeneralSettings GeneralSettings = new();
        public List<UntisHoliday> Holidays = new();
        public List<UntisLessonDateScheme> LessonDateSchemes = new();
        public List<UntisLesson> Lessons = new();
        public List<UntisLessonsTable> LessonsTables = new();
        public List<UntisRoom> Rooms = new ();
        public List<UntisStudentGroup> StudentGroups = new();
        public List<UntisStudent> Students = new();
        public List<UntisSubject> Subjects = new();
        public List<UntisTeacher> Teachers = new();
        public List<UntisTimeGrid> TimeGrids = new();

        private readonly string ClassXPathExpr = "//xs:document/xs:classes/xs:class";
        private readonly string DepartmentXPathExpr = "//xs:document/xs:departments/xs:department";
        private readonly string DescriptionXPathExpr = "//xs:document/xs:descriptions/xs:description";
        private readonly string GeneralSettingsXPathExpr = "//xs:document/xs:general";
        private readonly string HolidayXPathExpr = "//xs:document/xs:holidays/xs:holiday";
        private readonly string LessonDateSchemeXPathExpr = "//xs:document/xs:lesson_date_schemes/xs:lesson_date_scheme";
        private readonly string LessonsTableXPathExpr = "//xs:document/xs:lesson_tables/xs:lesson_table";
        private readonly string LessonXPathExpr = "//xs:document/xs:lessons/xs:lesson";
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
            ReadElements(xmlDoc, xmlNamespaceManager, RoomXPathExpr, (e) => ReadRooms(e));
            ReadElements(xmlDoc, xmlNamespaceManager, StudentGroupXPathExpr, (e) => ReadStudentGroups(e));
            ReadElements(xmlDoc, xmlNamespaceManager, StudentXPathExpr, (e) => ReadStudents(e));
            ReadElements(xmlDoc, xmlNamespaceManager, SubjectXPathExpr, (e) => ReadSubjects(e));
            ReadElements(xmlDoc, xmlNamespaceManager, TeacherXPathExpr, (e) => ReadTeachers(e));
            ReadElements(xmlDoc, xmlNamespaceManager, TimeGridSlotPathExpr, (e) => ReadTimeGridSlots(e));
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
                ForeignKey = xmlElement.GetValueOrDefault("foreignkey")
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
            GeneralSettings.SchoolNo = xmlElement.GetUIntOrDefault("schoolnumber");
            GeneralSettings.SchoolYearBeginDate = xmlElement.GetDate("schoolyearbegindate");
            GeneralSettings.SchoolYearEndDate = xmlElement.GetDate("schoolyearenddate");
            GeneralSettings.TermName = xmlElement.GetValueOrDefault("termname");
            GeneralSettings.TermBeginDate = xmlElement.GetDate("termbegindate");
            GeneralSettings.TermEndDate = xmlElement.GetDate("termenddate");
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
                Times = xmlElement.GetTimeElements("times")
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
                DescriptionId = xmlElement.GetReferenceIdOrDefault("room_description"),
                DepartmentId = xmlElement.GetReferenceIdOrDefault("room_department"),
                Text = xmlElement.GetValueOrDefault("text"),
                ForeignKey = xmlElement.GetValueOrDefault("foreignkey"),
                Capacity = xmlElement.GetUIntOrDefault("capacity")
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
                Text = xmlElement.GetValueOrDefault("text")
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
                ForeignKey = xmlElement.GetValueOrDefault("foreignkey")
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
                ForeignKey = xmlElement.GetValueOrDefault("foreignkey")
            });
        }

        /// <summary>
        /// Maps XML node to <see cref="UntisTimeGridSlot"/> instance
        /// </summary>
        /// <param name="xmlElement">XML node</param>
        private void ReadTimeGridSlots(XElement xmlElement)
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
                Day = xmlElement.GetDay("day"),
                StartTime = xmlElement.GetTime("starttime"),
                EndTime = xmlElement.GetTime("endtime"),
                Label = xmlElement.GetValueOrDefault("label"),
                ForeignKey = xmlElement.GetValueOrDefault("foreignkey"),
            });
        }
    }
}
