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
        public IEnumerable<ProjectFormFieldValuesCustomEntity> GetAllProjectFormFieldValues()
        {
            return context.project_form_field_values
                                    .Select(p => new ProjectFormFieldValuesCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfProjectFormValue = p.IdfProjectFormValue,
                                        IdfFormField = p.IdfFormField,
                                        Value = p.Value,
                                    }).ToList();
        }

        public ProjectFormFieldValuesCustomEntity GetProjectFormFieldValuebyId(long id)
        {
            return context.project_form_field_values
                        .Where(c => c.Id == id)
                                    .Select(p => new ProjectFormFieldValuesCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfProjectFormValue = p.IdfProjectFormValue,
                                        IdfFormField = p.IdfFormField,
                                        Value = p.Value,
                                    }).Single();
        }

        public CommonResponse SaveProjectFormFieldValue(project_form_field_values ProjectFormFieldValue)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
            try
            {
                switch (ProjectFormFieldValue.Id)
                {
                    case (long)AbmEnum.IsNew:
                        var newProjectFormFieldValue = new project_form_field_values
                        {
                            IdfProjectFormValue = ProjectFormFieldValue.IdfProjectFormValue,
                            IdfFormField = ProjectFormFieldValue.IdfFormField,
                            Value = ProjectFormFieldValue.Value,
                        };
                        context.project_form_field_values.Add(newProjectFormFieldValue);
                        context.SaveChanges();
                        break;
                    default:
                        var project_form_field_valuedb = context.project_form_field_values.Where(c => c.Id == ProjectFormFieldValue.Id).FirstOrDefault();
                        project_form_field_valuedb.IdfProjectFormValue = ProjectFormFieldValue.IdfProjectFormValue;
                        project_form_field_valuedb.IdfFormField = ProjectFormFieldValue.IdfFormField;
                        project_form_field_valuedb.Value = ProjectFormFieldValue.Value;
                        context.project_form_field_values.Update(project_form_field_valuedb);
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

        public CommonResponse DeleteProjectFormFieldValues(long projectFormFieldValueId)
        {
            CommonResponse result = new CommonResponse(); 
            var ProjectFormFieldValueToDelete = GetProjectFormFieldValuebyId(projectFormFieldValueId);
            if (ProjectFormFieldValueToDelete == null)
                result.Result = false;
            context.project_form_field_values.Remove(ProjectFormFieldValueToDelete);
            var deleted = context.SaveChanges();
            result.Result = deleted > 0;
            return result;
        }

        public IEnumerable<ProjectFormFieldValuesCustomEntity> GetAllProjectFormFieldValuesByProjectFormAndProject(long idProjectForm, long idProject)
        {
            return context.project_form_field_values
                .Join(context.project_form_values,
                                          sffv => sffv.IdfProjectFormValue,
                                          sfv => sfv.Id,
                                          (sffv, sfv) => new { sffv, sfv })
                .Where(p => p.sfv.IdfProject == idProject && p.sfv.IdfProjectForm == idProjectForm)
                                    .Select(p => new ProjectFormFieldValuesCustomEntity
                                    {
                                        Id = p.sffv.Id,
                                        IdfProjectFormValue = p.sffv.IdfProjectFormValue,
                                        IdfFormField = p.sffv.IdfFormField,
                                        Value = p.sffv.Value,
                                    }).ToList();
        }

        public IEnumerable<ProjectFormFieldValuesCustomEntity> GetAllProjectFormFieldValuesByProjectFormValue(long idProjectFormValue)
        {
            return context.project_form_field_values
                .Where(p => p.IdfProjectFormValue == idProjectFormValue )
                                    .Select(p => new ProjectFormFieldValuesCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfProjectFormValue = p.IdfProjectFormValue,
                                        IdfFormField = p.IdfFormField,
                                        Value = p.Value,
                                    }).ToList();
        }
    }
}
