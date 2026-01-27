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
        public IEnumerable<ProjectFormValuesCustomEntity> GetAllProjectFormValues()
        {
            return context.project_form_values
                                    .Select(p => new ProjectFormValuesCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfProjectForm = p.IdfProjectForm,
                                        IdfProject = p.IdfProject,
                                        FormDateTime = p.FormDateTime,
                                        DateTime = p.DateTime,
                                    }).ToList();
        }

        public ProjectFormValuesCustomEntity GetProjectFormValuebyId(long id)
        {
            return context.project_form_values
                        .Where(c => c.Id == id)
                                    .Select(p => new ProjectFormValuesCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfProjectForm = p.IdfProjectForm,
                                        IdfProject = p.IdfProject,
                                        FormDateTime = p.FormDateTime,
                                        DateTime = p.DateTime
                                    }).Single();
        }

        public CommonResponse SaveProjectFormValue(project_form_values ProjectFormValue)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
            try
            {
                switch (ProjectFormValue.Id)
                {
                    case (long)AbmEnum.IsNew:
                        var newProjectFormValue = new project_form_values
                        {
                            IdfProjectForm = ProjectFormValue.IdfProjectForm,
                            IdfProject = ProjectFormValue.IdfProject,
                            FormDateTime = ProjectFormValue.FormDateTime,
                            DateTime = DateTime.Now
                        };
                        context.project_form_values.Add(newProjectFormValue);
                        context.SaveChanges();
                        break;
                    default:
                        var project_form_valuedb = context.project_form_values.Where(c => c.Id == ProjectFormValue.Id).FirstOrDefault();
                        project_form_valuedb.IdfProjectForm = ProjectFormValue.IdfProjectForm;
                        project_form_valuedb.IdfProject = ProjectFormValue.IdfProject;
                        project_form_valuedb.FormDateTime = ProjectFormValue.FormDateTime;
                        project_form_valuedb.DateTime = DateTime.Now;
                        context.project_form_values.Update(project_form_valuedb);
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

        public CommonResponse DeleteProjectFormValues(long projectFormValueId)
        {
            CommonResponse result = new CommonResponse(); 
            var ProjectFormValueToDelete = GetProjectFormValuebyId(projectFormValueId);
            if (ProjectFormValueToDelete == null)
                result.Result = false;
            context.project_form_values.Remove(ProjectFormValueToDelete);
            var deleted = context.SaveChanges();
            result.Result = deleted > 0;
            return result;
        }

        public IEnumerable<ProjectFormValuesCustomEntity> GetAllProjectFormValuesByProjectFormAndProject(long idProjectForm, long idProject)
        {
            return context.project_form_values.Where(p=>p.IdfProject==idProject && p.IdfProjectForm==idProjectForm)
                                    .Select(p => new ProjectFormValuesCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfProjectForm = p.IdfProjectForm,
                                        IdfProject = p.IdfProject,
                                        DateTime = p.DateTime,
                                        FormDateTime = p.FormDateTime,
                                    }).ToList();
        }

        public CommonResponse SaveProjectFormValueWithDetail(project_form_values ProjectFormValue, project_form_field_values[] ProjectFormFieldValues)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
            try
            {
                project_form_values projectFormValueFromDB = null;
                if (ProjectFormValue.Id>0)
                {
                    projectFormValueFromDB = context.project_form_values.Where(p => p.Id == ProjectFormValue.Id).SingleOrDefault();
                }
                if (projectFormValueFromDB==null)
                {
                    ProjectFormValue.Id = 0;
                    ProjectFormValue.DateTime = DateTime.Now;
                    context.project_form_values.Add(ProjectFormValue);
                    context.SaveChanges();  
                    projectFormValueFromDB = context.project_form_values.Where(p => p.Id == ProjectFormValue.Id).Single();
                }
                foreach (project_form_field_values project_form_field_value in ProjectFormFieldValues)
                {
                    project_form_field_value.IdfProjectFormValue = projectFormValueFromDB.Id;
                    project_form_field_values project_form_field_valueFromDB = context.project_form_field_values.Where(p => p.IdfProjectFormValue == project_form_field_value.IdfProjectFormValue && p.IdfFormField == project_form_field_value.IdfFormField).SingleOrDefault();
                    if (project_form_field_valueFromDB == null)
                    {
                        context.project_form_field_values.Add(project_form_field_value);
                        context.SaveChanges();
                    }
                    else
                    {
                        project_form_field_valueFromDB.Value = project_form_field_value.Value;
                        context.project_form_field_values.Update(project_form_field_valueFromDB);
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
        public bool UpdateProjectFormValueImage(long id, string fileName)
        {
            var newRecord = new clients_images
            {
                Name = fileName,
                IdfClient = id
            };

            context.clients_images.Add(newRecord);
            context.SaveChanges();

            context.clients.Where(c => c.Id == id).Single().IdfImg = newRecord.Id;
            context.SaveChanges();

            return true;
        }
    }
}
