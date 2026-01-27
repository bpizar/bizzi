using JayGor.People.DataAccess;
using JayGor.People.Entities.Responses;
using System.Threading.Tasks;
using System;
using JayGor.People.Entities.Entities;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
       
        private IDatabaseService dataAccessLayer;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:FacilSFE.WebApiNegocio.Negocio"/> class.
        /// </summary>
        /// <param name="ds">The Ds.</param>
        public BussinnessLayer(IDatabaseService ds)
        {
             this.dataAccessLayer = ds;
        }

        public string Ping()
        {
            return dataAccessLayer.Ping();
            
        }

        public CommonResponse CommonSaveError(string errorDescription, long idfuser = 1)
        {          
            return dataAccessLayer.CommonSaveError(errorDescription, idfuser);
        }

        public CommonResponse CommonSaveError(GenericPair inputs, long idfuser = 1)
        {
            return dataAccessLayer.CommonSaveError(inputs.Description, idfuser);
        }

        public async Task<bool> UploadFile(byte[] fileContent,string serverPath,long FileName,string location)
        {
            var response = new CommonResponse();

            var generateName = Guid.NewGuid().ToString();

            var path = string.Format("{0}/{1}.png", serverPath, generateName);
            System.IO.File.WriteAllBytes(path, fileContent);

            // return dataAccessLayer.UpdateIdentityImage(FileName, generateName);

            switch(location.ToLower())
            {
                case "users":
					return dataAccessLayer.UpdateIdentityImage(FileName, generateName);
                case "clients":
                    return dataAccessLayer.UpdateClientImage(FileName, generateName);
                case "staffformvalues":
                    staff_form_image_values data = new staff_form_image_values();
                    data.IdfStaff = FileName % 10000;
                    data.IdfStaffForm = FileName / 10000;
                    data.Image = generateName;
                    return dataAccessLayer.SaveStaffFormImageValue(data).Result;
                case "clientformvalues":
                    client_form_image_values clientformimagevalue = new client_form_image_values();
                    clientformimagevalue.IdfClient = FileName % 10000;
                    clientformimagevalue.IdfClientForm = FileName / 10000;
                    clientformimagevalue.Image = generateName;
                    return dataAccessLayer.SaveClientFormImageValue(clientformimagevalue).Result;
                case "projectformvalues":
                    project_form_image_values projectformimagevalue = new project_form_image_values();
                    projectformimagevalue.IdfProject = FileName % 10000;
                    projectformimagevalue.IdfProjectForm = FileName / 10000;
                    projectformimagevalue.Image = generateName;
                    return dataAccessLayer.SaveProjectFormImageValue(projectformimagevalue).Result;
            }

            return false;
        }

        //public void SaveFileFR(byte[] fileContent, string serverPath, long FileName)
        //{
        //    var response = new CommonResponse();

        //    var generateName = Guid.NewGuid().ToString();

        //    var path = string.Format("{0}/{1}.fr", serverPath, generateName);
        //    System.IO.File.WriteAllBytes(path, fileContent);

        //    //switch (location.ToLower())
        //    //{
        //    //    case "users":
        //    //        return dataAccessLayer.UpdateIdentityImage(FileName, generateName);
        //    //    case "clients":
        //    //        return dataAccessLayer.UpdateClientImage(FileName, generateName);
        //    //}
        //}


        public DateTime FixTime(DateTime timeInput, int timeDifference)
        {
            timeInput = timeInput.AddHours(timeDifference);
            return timeInput;
        }


    }
}
