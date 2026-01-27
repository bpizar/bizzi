using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Enumerators;
using JayGor.People.Entities.Responses;
using Microsoft.EntityFrameworkCore;


namespace JayGor.People.DataAccess.Factories.MySqlServer
{
    public partial class MySqlDatabaseService : IDatabaseService    
    {
        public IEnumerable<StaffFormsCustomEntity> GetAllStaffForms()
        {
            return context.staff_forms
                                    .Select(p => new StaffFormsCustomEntity
                                    {
                                        Id = p.Id,
                                        Name = p.Name,
                                        Description = p.Description,
                                        Information = p.Information,
                                        Template=p.Template,
                                        TemplateFile=p.TemplateFile,
                                        IdfRecurrence=p.IdfRecurrence,
                                        IdfRecurrenceDetail=p.IdfRecurrenceDetail
                                    }).ToList();
        }
        public StaffFormsCustomEntity GetStaffFormbyId(long id)
        {
            return context.staff_forms
                        .Where(c => c.Id == id)
                                    .Select(p => new StaffFormsCustomEntity
                                    {
                                        Id = p.Id,
                                        Name= p.Name,
                                        Description=p.Description,
                                        Information=p.Information,
                                        Template=p.Template,
                                        TemplateFile=p.TemplateFile,
                                        IdfRecurrence = p.IdfRecurrence,
                                        IdfRecurrenceDetail = p.IdfRecurrenceDetail
                                    }).Single();
        }

        public CommonResponse SaveStaffForm(staff_forms StaffForm)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
            try
            {
                switch (StaffForm.Id)
                {
                    case (long)AbmEnum.IsNew:
                        var newStaffForm = new staff_forms
                        {
                            Name = StaffForm.Name,
                            Description = StaffForm.Description,
                            Information = StaffForm.Information,
                            Template = StaffForm.Template,
                            TemplateFile = StaffForm.TemplateFile,
                            IdfRecurrence = StaffForm.IdfRecurrence,
                            IdfRecurrenceDetail = StaffForm.IdfRecurrenceDetail
                        };
                        context.staff_forms.Add(newStaffForm);
                        context.SaveChanges();
                        break;
                    default:
                        var staff_formdb = context.staff_forms.Where(c => c.Id == StaffForm.Id).FirstOrDefault();
                        staff_formdb.Name = StaffForm.Name;
                        staff_formdb.Description = StaffForm.Description;
                        staff_formdb.Information = StaffForm.Information;
                        staff_formdb.Template = StaffForm.Template;
                        staff_formdb.TemplateFile = StaffForm.TemplateFile;
                        staff_formdb.IdfRecurrence = StaffForm.IdfRecurrence;
                        staff_formdb.IdfRecurrenceDetail = StaffForm.IdfRecurrenceDetail;
                        context.staff_forms.Update(staff_formdb);
                        context.SaveChanges();
                        break;
                }
                transaction.Commit();
                result.Result = true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            return result;
        }
        public CommonResponse DeleteStaffForms(long staffFormId)
        {
            CommonResponse result = new CommonResponse(); 
            var StaffFormToDelete = GetStaffFormbyId(staffFormId);
            if (StaffFormToDelete == null)
                result.Result = false;
            context.staff_forms.Remove(StaffFormToDelete);
            var deleted = context.SaveChanges();
            result.Result = deleted > 0;

            return result;
        }
        public IEnumerable<StaffFormbyStaffCustomEntity> GetAllStaffFormsByStaff(long IdStaff)
        {
            return context.staff_forms.Select(p => new StaffFormbyStaffCustomEntity
            {
                Id = p.Id,
                IdfStaffFormValue = p.Template.StartsWith("Image") ? context.staff_form_image_values.Where(q => q.IdfStaffForm == p.Id && q.IdfStaff == IdStaff).OrderByDescending(q => q.FormDateTime).Select(q => q.Id).FirstOrDefault() : context.staff_form_values.Where(q => q.IdfStaffForm == p.Id && q.IdfStaff == IdStaff).OrderByDescending(q => q.FormDateTime).Select(q => q.Id).FirstOrDefault(),
                Name = p.Name,
                Description = p.Description,
                Information = p.Information,
                Template = p.Template,
                TemplateFile=p.TemplateFile,
                FormDateTime = p.Template.StartsWith("Image") ? context.staff_form_image_values.Where(q => q.IdfStaffForm == p.Id && q.IdfStaff == IdStaff).OrderByDescending(q => q.FormDateTime).Select(q => q.FormDateTime).FirstOrDefault().ToString() : context.staff_form_values.Where(q => q.IdfStaffForm == p.Id && q.IdfStaff == IdStaff).OrderByDescending(q => q.FormDateTime).Select(q => q.FormDateTime).FirstOrDefault().ToString(),
                //NextDate = context.staff_form_values.OrderByDescending(q=>q.DateTime).Where(q => q.IdfStaffForm == p.Id && q.IdfStaff == IdStaff).Select(q => q.FormDateTime).Max(),
                IdfRecurrence = p.IdfRecurrence,
                IdfRecurrenceDetail = p.IdfRecurrenceDetail,
                quantity = p.Template.StartsWith("Image") ? context.staff_form_image_values.Where(q => q.IdfStaffForm == p.Id && q.IdfStaff == IdStaff).Select(q => q.Id).Count() : context.staff_form_values.Where(q => q.IdfStaffForm == p.Id && q.IdfStaff == IdStaff).Select(q => q.Id).Count()
            }).ToList();
        }

        public IEnumerable<StaffFormbyStaffCustomEntity> GetAllStaffFormsByStaffandStaffForm(long IdStaff, long IdStaffForm)
        {
            StaffFormsCustomEntity staffForm = GetStaffFormbyId(IdStaffForm);
            if (staffForm != null)
            {
                if (staffForm.Template.StartsWith("Image"))
                {
                    return context.staff_form_image_values.Where(p => p.IdfStaff == IdStaff && p.IdfStaffForm == IdStaffForm).Select(p => new StaffFormbyStaffCustomEntity
                    {
                        Id = p.Id,
                        IdfStaffFormValue = p.Id,
                        Name = staffForm.Name,
                        Description = staffForm.Description,
                        Information = staffForm.Information,
                        Template = staffForm.Template,
                        TemplateFile = staffForm.TemplateFile,
                        FormDateTime = p.FormDateTime.ToString(),
                        IdfRecurrence = staffForm.IdfRecurrence,
                        IdfRecurrenceDetail = staffForm.IdfRecurrenceDetail,
                        quantity = 1
                    }).OrderBy(p => p.FormDateTime).ToList();
                }
                else
                {
                    return context.staff_form_values.Where(p => p.IdfStaff == IdStaff && p.IdfStaffForm == IdStaffForm).Select(p => new StaffFormbyStaffCustomEntity
                    {
                        Id = p.Id,
                        IdfStaffFormValue = p.Id,
                        Name = staffForm.Name,
                        Description = staffForm.Description,
                        Information = staffForm.Information,
                        Template = staffForm.Template,
                        TemplateFile = staffForm.TemplateFile,
                        FormDateTime = p.FormDateTime.ToString(),
                        IdfRecurrence = staffForm.IdfRecurrence,
                        IdfRecurrenceDetail = staffForm.IdfRecurrenceDetail,
                        quantity = 1
                    }).OrderBy(p => p.FormDateTime).ToList();
                }
            }
            else
                return null;
        }

        public CommonResponse SaveStaffFormWithReminders(staff_forms StaffForm, staff_form_reminders[] StaffFormReminders, FormFieldsCustomEntity[] FormFields)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
            try
            {
                StaffForm.Template = StaffForm.Template ?? "";
                staff_forms staffFormFromDB = null;
                if (StaffForm.Id > 0)
                {
                    staffFormFromDB = context.staff_forms.Where(p => p.Id == StaffForm.Id).SingleOrDefault();
                }
                if (staffFormFromDB == null)
                {
                    StaffForm.Id = 0;
                    context.staff_forms.Add(StaffForm);
                    context.SaveChanges();
                    staffFormFromDB = context.staff_forms.Where(p => p.Id == StaffForm.Id).Single();
                }
                else
                {
                    staffFormFromDB.Name = StaffForm.Name;
                    staffFormFromDB.Description = StaffForm.Description;
                    staffFormFromDB.Information = StaffForm.Information;
                    staffFormFromDB.Template = StaffForm.Template;
                    staffFormFromDB.TemplateFile = StaffForm.TemplateFile;
                    staffFormFromDB.IdfRecurrence = StaffForm.IdfRecurrence;
                    staffFormFromDB.IdfRecurrenceDetail = StaffForm.IdfRecurrenceDetail;
                    context.staff_forms.Update(staffFormFromDB);
                    context.SaveChanges();
                }
                foreach (staff_form_reminders staff_form_reminder in StaffFormReminders)
                {
                    staff_form_reminder.IdfStaffForm = staffFormFromDB.Id;
                    staff_form_reminders staff_form_reminderFromDB = context.staff_form_reminders.Where(p => p.IdfStaffForm == staff_form_reminder.IdfStaffForm && p.IdfReminderLevel == staff_form_reminder.IdfReminderLevel).FirstOrDefault();
                    if (staff_form_reminderFromDB == null)
                    {
                        context.staff_form_reminders.Add(staff_form_reminder);
                        context.SaveChanges();
                        staff_form_reminderFromDB = context.staff_form_reminders.Where(p => p.IdfStaffForm == staff_form_reminder.IdfStaffForm && p.IdfReminderLevel == staff_form_reminder.IdfReminderLevel).FirstOrDefault();
                    }
                    else
                    {
                        //staff_form_reminderFromDB.IdfReminderLevel = staff_form_reminder.IdfReminderLevel;
                        staff_form_reminderFromDB.IdfPeriodType = staff_form_reminder.IdfPeriodType;
                        staff_form_reminderFromDB.IdfPeriodValue = staff_form_reminder.IdfPeriodValue;
                        context.staff_form_reminders.Update(staff_form_reminderFromDB);
                        context.SaveChanges();
                    }
                    staff_form_reminder_users[] staff_form_reminder_userForDelete = context.staff_form_reminder_users.Where(p => p.IdfStaffFormReminder == staff_form_reminderFromDB.Id && !staff_form_reminder.IdfUsers.Contains( p.IdfUser)).ToArray();
                    context.staff_form_reminder_users.RemoveRange(staff_form_reminder_userForDelete);
                    context.SaveChanges();

                    foreach (long idfuser in staff_form_reminder.IdfUsers)
                    {
                        staff_form_reminder_users staff_form_reminder_userFromDB = context.staff_form_reminder_users.Where(p => p.IdfStaffFormReminder == staff_form_reminderFromDB.Id && p.IdfUser == idfuser).FirstOrDefault();
                        if (staff_form_reminder_userFromDB == null)
                        {
                            staff_form_reminder_users staff_form_reminder_user = new staff_form_reminder_users();
                            staff_form_reminder_user.IdfStaffFormReminder = staff_form_reminderFromDB.Id;
                            staff_form_reminder_user.IdfUser = idfuser;
                            context.staff_form_reminder_users.Add(staff_form_reminder_user);
                            context.SaveChanges();
                        }
                    }
                }
                foreach (FormFieldsCustomEntity customFormField in FormFields)
                {
                    staff_form_fields staff_form_fieldFromDB = context.staff_form_fields.Where(p => p.IdfStaffForm == StaffForm.Id && p.IdfFormField == customFormField.Id).FirstOrDefault();
                    if (staff_form_fieldFromDB == null)
                    {
                        form_fields formField = new form_fields();
                        formField.Name = customFormField.Name;
                        formField.Description = customFormField.Description;
                        formField.Placeholder = customFormField.Placeholder;
                        formField.Datatype = customFormField.Datatype;
                        formField.Constraints = customFormField.Constraints;
                        context.form_fields.Add(formField);
                        context.SaveChanges();
                        staff_form_fields staffFormField = new staff_form_fields();
                        staffFormField.IdfFormField = formField.Id;
                        staffFormField.IdfStaffForm = StaffForm.Id;
                        staffFormField.Position = customFormField.Position;
                        staffFormField.IsEnabled = customFormField.IsEnabled;
                        context.staff_form_fields.Add(staffFormField);
                        context.SaveChanges();
                    }
                    else
                    {
                        form_fields form_fieldFromDB = context.form_fields.Where(p => p.Id== staff_form_fieldFromDB.IdfFormField).FirstOrDefault();
                        form_fieldFromDB.Placeholder = customFormField.Placeholder;
                        form_fieldFromDB.Description = customFormField.Description;
                        context.form_fields.Update(form_fieldFromDB);
                        context.SaveChanges();
                        staff_form_fieldFromDB.Position = customFormField.Position;
                        staff_form_fieldFromDB.IsEnabled = customFormField.IsEnabled;
                        context.staff_form_fields.Update(staff_form_fieldFromDB);
                        context.SaveChanges();
                    }
                }
                transaction.Commit();
                result.Result = true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            return result;
        }
    }
}
