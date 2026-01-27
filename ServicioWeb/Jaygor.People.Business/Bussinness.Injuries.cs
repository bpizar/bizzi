using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using Polenter.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
// using SharpSerializer;




namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        //public h_injuries Injury { get; set; }
        //public List<h_degree_of_injury> DegreeOfInjuryList { get; set; } = new List<h_degree_of_injury>();
        //public List<StaffCustomEntity> Staffs { get; set; } = new List<StaffCustomEntity>();


        public bool GetInjuryById(long idPeriod,
                                  long idInjury,
                                  long idClient,                                  
                                  out h_injuries Injury,
                                  out List<h_degree_of_injury> DegreeOfInjuryList,
                                  out List<StaffCustomEntity> Staffs,
                                  out List<h_catalogCustom> Catalog,
                                    out string clientName,
                                    out string clientImg,
                                    out List<List<PointBody>> points,
                                    out List<ProjectCustomEntity> projects)
        {

            var isnew = idInjury < 0;

            Injury = new h_injuries();
            DegreeOfInjuryList = new List<h_degree_of_injury>();
            Staffs = new List<StaffCustomEntity>();
            Catalog = new List<h_catalogCustom>();
            clientName = "";
            clientImg = "";
            points = new List<List<PointBody>>();

            // projects = dataAccessLayer.GetProjectClientByIdPeriodIdClient(idPeriod, idClient);

            projects = dataAccessLayer.GetProjectClientByIdPeriodIdClient(idPeriod, idClient).Select(x => new
            ProjectCustomEntity
            {
                Id = x.IdfProject,
                Description = x.Description,
                Color = x.Color
            }).ToList();




            DegreeOfInjuryList = dataAccessLayer.GetDegreeOfInjuries();

            var cat = dataAccessLayer.GetInjuryCatalog();


            if(isnew)
            {
                if (idPeriod > 0)
                {
                    Staffs = dataAccessLayer.GetStaffForIncident(idPeriod).ToList();
                }

                Injury.DateOfInjury = DateTime.Now;
                Injury.DateReportedSupervisor = DateTime.Now;

                //Injury.Id = -1;

                Catalog.AddRange(cat);
            }
            else
            {
                Injury = dataAccessLayer.GetInjuryById(idInjury);
                Staffs = dataAccessLayer.GetStaffForIncident(Injury.IdfPeriod).ToList();
               

                var catval = dataAccessLayer.GetCatalogInjuryValues(idInjury);

                foreach (h_injury_values cv in catval)
                {
                    var found = cat.Where(c => c.id == cv.idfCatalog);

                    if (found != null)
                    {
                        found.Single().Value = cv.Value;
                        found.Single().IdValue = cv.id;
                    }
                }

                Catalog.AddRange(cat);

                XmlDocument doc2 = new XmlDocument();
                doc2.LoadXml(Injury.BodySerialized);

                //Assuming doc is an XML document containing a serialized object and objType is a System.Type set to the type of the object.
                XmlNodeReader reader = new XmlNodeReader(doc2.DocumentElement);


                XmlSerializer ser2 = new XmlSerializer(points.GetType());
                object obj = ser2.Deserialize(reader);
                //List<List<PointBody>> myObj = (List<List<PointBody>>)obj;
                points = (List<List<PointBody>>)obj;
                Injury.BodySerialized = string.Empty;
            }

           //List<h_medical_remindersCustom> medicalRemindersAux = new List<h_medical_remindersCustom>();
            var clientAux = dataAccessLayer.GetClientById(isnew ? idClient : Injury.IdfClient);
            clientName = clientAux.FullName;
            clientImg = clientAux.Img;
                                 

            return true;
        }


        public bool SaveInjury(h_injuries Injury,
                               List<h_catalogCustom> Catalog,
                               List<List<PointBody>> Points,
                                int timeDifference,
                               out long idInjuryOut)
        {


            Injury.DateOfInjury = Injury.DateOfInjury.AddHours(timeDifference);



            //var catalogAux = Catalog.Select(x => new h_catalog
            //{
            //    Id = x.Id,
            //    State = x.State,
            //    Description = x.Description,
            //    IdentifierGroup = x.IdentifierGroup,
            //    Type = x.Type,
            //    h_incident_values = x.h_incident_values,
            //    h_injury_values = x.h_injury_values,                 
            //}).ToList();


            var catalogValues = Catalog.Select(x => new
             h_injury_values
             {
                    id = x.IdValue,
                    idfCatalog = x.id,
                    idfInjury = Injury.Id,
                    Value = x.Value
             }).ToList();

            //var stream = new MemoryStream();
            //var settings = new SharpSerializerBinarySettings(BinarySerializationMode.SizeOptimized);
            //var serializer = new SharpSerializer(settings);


            //7serializer.Serialize(Points, stream);

            // stream.ToString();


            XmlSerializer ser = new XmlSerializer(Points.GetType());
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.IO.StringWriter writer = new System.IO.StringWriter(sb);
            ser.Serialize(writer, Points);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(sb.ToString());


            var txtToBDD = string.Empty;
            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
            {
                doc.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                txtToBDD = stringWriter.GetStringBuilder().ToString();
            }


            Injury.BodySerialized = txtToBDD;

            //serializer.Deserialize(stream);




            dataAccessLayer.SaveInjury(Injury, catalogValues, Points, out idInjuryOut);


            return true;
        }

    }
}