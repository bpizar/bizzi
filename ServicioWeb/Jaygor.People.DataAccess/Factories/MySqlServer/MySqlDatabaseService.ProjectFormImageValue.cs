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
        public IEnumerable<ProjectFormImageValuesCustomEntity> GetAllProjectFormImageValues()
        {
            return context.project_form_image_values
                                    .Select(p => new ProjectFormImageValuesCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfProjectForm = p.IdfProjectForm,
                                        IdfProject = p.IdfProject,
                                        Image = p.Image,
                                        FormDateTime = p.FormDateTime,
                                        DateTime = p.DateTime,
                                    }).ToList();
        }

        public ProjectFormImageValuesCustomEntity GetProjectFormImageValuebyId(long id)
        {
            return context.project_form_image_values
                        .Where(c => c.Id == id)
                                    .Select(p => new ProjectFormImageValuesCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfProjectForm = p.IdfProjectForm,
                                        IdfProject = p.IdfProject,
                                        Image = p.Image,
                                        FormDateTime = p.FormDateTime,
                                        DateTime = p.DateTime
                                    }).Single();
        }

        public CommonResponse SaveProjectFormImageValue(project_form_image_values ProjectFormImageValue)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
            try
            {
                project_form_image_values projectFormImageValueFromDB = null;
                if (ProjectFormImageValue.Id > 0)
                {
                    projectFormImageValueFromDB = context.project_form_image_values.Where(p => p.Id == ProjectFormImageValue.Id).SingleOrDefault();
                }
                if (projectFormImageValueFromDB is null)
                {
                    ProjectFormImageValue.Id = 0;
                    ProjectFormImageValue.DateTime = DateTime.Now;
                    context.project_form_image_values.Add(ProjectFormImageValue);
                    context.SaveChanges();
                    projectFormImageValueFromDB = context.project_form_image_values.Where(p => p.Id == ProjectFormImageValue.Id).Single();
                }
                else
                {
                    projectFormImageValueFromDB.Image = ProjectFormImageValue.Image?? projectFormImageValueFromDB.Image;
                    projectFormImageValueFromDB.DateTime = DateTime.Now;
                    projectFormImageValueFromDB.FormDateTime = ProjectFormImageValue.FormDateTime;
                    context.project_form_image_values.Update(projectFormImageValueFromDB);
                    context.SaveChanges();
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

        public CommonResponse DeleteProjectFormImageValues(long projectFormImageValueId)
        {
            CommonResponse result = new CommonResponse(); 
            var ProjectFormImageValueToDelete = GetProjectFormImageValuebyId(projectFormImageValueId);
            if (ProjectFormImageValueToDelete == null)
                result.Result = false;
            context.project_form_image_values.Remove(ProjectFormImageValueToDelete);
            var deleted = context.SaveChanges();
            result.Result = deleted > 0;
            return result;
        }

        public ProjectFormImageValuesCustomEntity GetProjectFormImageValueByProjectFormAndProject(long idProjectForm, long idProject)
        {
            return context.project_form_image_values.Where(p=>p.IdfProject==idProject && p.IdfProjectForm==idProjectForm)
                                    .Select(p => new ProjectFormImageValuesCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfProjectForm = p.IdfProjectForm,
                                        IdfProject = p.IdfProject,
                                        Image = p.Image,
                                        FormDateTime = p.FormDateTime,
                                        DateTime = p.DateTime
                                    }).FirstOrDefault();
        }   
    }
}
