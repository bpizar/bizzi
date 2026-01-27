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
        public IEnumerable<FormFieldsCustomEntity> GetAllFormFields()
        {
            return context.form_fields
                                    .Select(p => new FormFieldsCustomEntity
                                    {
                                        Id = p.Id,
                                        Name = p.Name,
                                        Description = p.Description,
                                        Placeholder = p.Placeholder,
                                        Datatype = p.Datatype,
                                        Constraints=p.Constraints
                                    }).ToList();
        }
        public FormFieldsCustomEntity GetFormFieldbyId(long id)
        {
            return context.form_fields
                        .Where(c => c.Id == id)
                                    .Select(p => new FormFieldsCustomEntity
                                    {
                                        Id = p.Id,
                                        Name = p.Name,
                                        Description = p.Description,
                                        Placeholder = p.Placeholder,
                                        Datatype = p.Datatype,
                                        Constraints = p.Constraints
                                    }).Single();
        }

        public CommonResponse SaveFormField(form_fields Form_fields)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
            try
            {
                switch (Form_fields.Id)
                {
                    case (long)AbmEnum.IsNew:
                        var newForm_fields = new form_fields
                        {
                            Name = Form_fields.Name,
                            Description = Form_fields.Description,
                            Placeholder = Form_fields.Placeholder,
                            Datatype = Form_fields.Datatype,
                            Constraints = Form_fields.Constraints
                        };
                        context.form_fields.Add(newForm_fields);
                        context.SaveChanges();
                        break;
                    default:
                        var form_fieldsdb = context.form_fields.Where(c => c.Id == Form_fields.Id).FirstOrDefault();
                        form_fieldsdb.Name = Form_fields.Name;
                        form_fieldsdb.Description = Form_fields.Description;
                        form_fieldsdb.Placeholder = Form_fields.Placeholder;
                        form_fieldsdb.Datatype = Form_fields.Datatype;
                        form_fieldsdb.Constraints = Form_fields.Constraints;
                        context.form_fields.Update(form_fieldsdb);
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
        public CommonResponse DeleteFormField(long formfieldsId)
        {
            CommonResponse result = new CommonResponse();
            var FormFieldsToDelete = GetFormFieldbyId(formfieldsId);
            if (FormFieldsToDelete == null)
                result.Result = false;
            context.form_fields.Remove(FormFieldsToDelete);
            var deleted = context.SaveChanges();
            result.Result = deleted > 0;
            return result;
        }

        public IEnumerable<FormFieldsCustomEntity> GetAllFormFieldsByClientForm(long IdClientForm)
        {
            var query = from cff in context.client_form_fields
                        join ff in context.form_fields on cff.IdfFormField equals ff.Id
                        where (cff.IdfClientForm == IdClientForm)
                        orderby cff.Position
                        select new FormFieldsCustomEntity
                        {
                            Id = ff.Id,
                            Name = ff.Name,
                            Description = ff.Description,
                            Placeholder = ff.Placeholder,
                            Constraints = ff.Constraints,
                            Datatype = ff.Datatype,
                            Position = cff.Position,
                            IsEnabled = cff.IsEnabled
                        };
            return query.Distinct().ToList();
        }

        public IEnumerable<FormFieldsCustomEntity> GetAllFormFieldsByProjectForm(long IdProjectForm)
        {
            var query = from pff in context.project_form_fields
                        join ff in context.form_fields on pff.IdfFormField equals ff.Id
                        where (pff.IdfProjectForm == IdProjectForm)
                        orderby pff.Position
                        select new FormFieldsCustomEntity
                        {
                            Id = ff.Id,
                            Name = ff.Name,
                            Description = ff.Description,
                            Placeholder = ff.Placeholder,
                            Constraints = ff.Constraints,
                            Datatype = ff.Datatype,
                            Position = pff.Position,
                            IsEnabled = pff.IsEnabled
                        };
            return query.Distinct().ToList();
        }

        public IEnumerable<FormFieldsCustomEntity> GetAllFormFieldsByStaffForm(long IdStaffForm)
        {
            var query = from sff in context.staff_form_fields
                        join ff in context.form_fields on sff.IdfFormField equals ff.Id
                        where (sff.IdfStaffForm == IdStaffForm)
                        orderby sff.Position
                        select new FormFieldsCustomEntity
                        {
                            Id = ff.Id,
                            Name = ff.Name,
                            Description = ff.Description,
                            Placeholder = ff.Placeholder,
                            Constraints = ff.Constraints,
                            Datatype = ff.Datatype,
                            Position = sff.Position,
                            IsEnabled = sff.IsEnabled
                        };
            return query.Distinct().ToList();
        }
    }
}
