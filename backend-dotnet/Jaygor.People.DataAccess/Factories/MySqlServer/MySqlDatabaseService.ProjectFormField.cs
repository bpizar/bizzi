using System;
using System.Collections.Generic;
using System.Linq;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Enumerators;
using JayGor.People.Entities.Responses;
using Microsoft.EntityFrameworkCore;


namespace JayGor.People.DataAccess.Factories.MySqlServer
{
    public partial class MySqlDatabaseService : IDatabaseService    
    {
        public IEnumerable<ProjectFormFieldsCustomEntity> GetAllProjectFormFields()
        {
            return context.project_form_fields
                                    .Select(p => new ProjectFormFieldsCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfProjectForm = p.IdfProjectForm,
                                        IdfFormField= p.IdfFormField,
                                    }).ToList();
        }

        public ProjectFormFieldsCustomEntity GetProjectFormFieldbyId(long id)
        {
            return context.project_form_fields
                        .Where(c => c.Id == id)
                                    .Select(p => new ProjectFormFieldsCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfProjectForm = p.IdfProjectForm,
                                        IdfFormField = p.IdfFormField,
                                    }).Single();
        }

        public CommonResponse SaveProjectFormField(project_form_fields ProjectFormField)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
            try
            {
                switch (ProjectFormField.Id)
                {
                    case (long)AbmEnum.IsNew:
                        var newProjectFormField = new project_form_fields
                        {
                            IdfProjectForm = ProjectFormField.IdfProjectForm,
                            IdfFormField = ProjectFormField.IdfFormField,
                        };
                        context.project_form_fields.Add(newProjectFormField);
                        context.SaveChanges();
                        break;
                    default:
                        var project_form_fielddb = context.project_form_fields.Where(c => c.Id == ProjectFormField.Id).FirstOrDefault();
                        project_form_fielddb.IdfProjectForm = ProjectFormField.IdfProjectForm;
                        project_form_fielddb.IdfFormField = ProjectFormField.IdfFormField;
                        context.project_form_fields.Update(project_form_fielddb);
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

        public CommonResponse DeleteProjectFormFields(long projectFormFieldId)
        {
            CommonResponse result = new CommonResponse(); 
            var ProjectFormFieldToDelete = GetProjectFormFieldbyId(projectFormFieldId);
            if (ProjectFormFieldToDelete == null)
                result.Result = false;
            context.project_form_fields.Remove(ProjectFormFieldToDelete);
            var deleted = context.SaveChanges();
            result.Result = deleted > 0;
            return result;
        }

        public CommonResponse RemoveProjectFormField(long projectFormId, long FormFieldId)
        {
            CommonResponse result = new CommonResponse();

            var ProjectFormFieldToDelete = context.project_form_fields
            .Where(c => c.IdfFormField == FormFieldId && c.IdfProjectForm == projectFormId)
                        .Select(p => new ProjectFormFieldsCustomEntity
                        {
                            Id = p.Id,
                            IdfProjectForm = p.IdfProjectForm,
                            IdfFormField = p.IdfFormField,
                        }).Single();
            if (ProjectFormFieldToDelete == null)
                result.Result = false;
            context.project_form_fields.Remove(ProjectFormFieldToDelete);
            var deleted = context.SaveChanges();
            result.Result = deleted > 0;
            return result;
        }
        public CommonResponse AddProjectFormField(long idProjectForm, string name, string description, string placeholder, string dataType, string constraints)
        {
            CommonResponse result = new CommonResponse();
            var formFieldToAdd = context.form_fields
            .Where(c => c.Name== name && 
                    c.Description == description &&
                    c.Placeholder == placeholder &&
                    c.Datatype == dataType &&
                    c.Constraints == constraints
                    )
                        .Select(p => p).SingleOrDefault();
            if (formFieldToAdd == null)
            {
                formFieldToAdd = new form_fields
                {
                    Name = name,
                    Description = description,
                    Placeholder = placeholder,
                    Datatype = dataType,
                    Constraints = constraints
                };
                context.form_fields.Add(formFieldToAdd);
                var form_fieldsadded = context.SaveChanges();
            }
            var projectFormFieldToAdd = new project_form_fields { IdfProjectForm = idProjectForm , IdfFormField = formFieldToAdd.Id };
            context.project_form_fields.Add(projectFormFieldToAdd);
            var added = context.SaveChanges();
            result.Result = added > 0;
            return result;
        }

    }
}
