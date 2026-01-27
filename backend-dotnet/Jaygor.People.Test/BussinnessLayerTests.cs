using System;
using JayGor.People.Bussinness;
using Xunit;
using System.IO;
using Microsoft.Extensions.Configuration;
using Polenter.Serialization;
using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;
using System.Text;
using System.Linq;
using System.Collections;
using Polenter.Serialization.Advanced;
using Polenter.Serialization.Advanced.Serializing;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Xml;

namespace Jaygor.People.Test
{
    public class BussinnessLayerTests
    {
        private readonly BussinnessLayer bussinnessLayer;// = new BussinnessLayer();


        IConfiguration configuration;

        public BussinnessLayerTests()
        {
            configuration = new ConfigurationBuilder()
                               .SetBasePath(Directory.GetCurrentDirectory()) // Directory where the json files are located
                               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                               .Build();

        }


        //[Fact]
        //public void GetProjectsDailyLogsMobile_Test()
        //{
        //    var idUser = 87;
        //    var res = bussinnessLayer.GetProjectsDailyLogs_Mobile(idUser);
        //    Assert.NotNull(res);
        //}

        //[Fact]
        //public void xxxx()
        //{
        //    var idUser = 87;
        //    var res = bussinnessLayer.GetProjectsDailyLogs_Mobile(idUser);
        //    Assert.NotNull(res);
        //}





        //public static string SerilizeObject(Object obj)
        //{
        //    if (obj == null) return null;

        //    MemoryStream memoryStream = new MemoryStream();
        //    BinaryFormatter formatter = new BinaryFormatter();
        //    formatter.Serialize(memoryStream, obj);
        //    return Encoding.ASCII.GetString(memoryStream.ToArray());
        //}

        //public static Object DeSerilizeObject(string str)
        //{
        //    if (str == null) return null;

        //    MemoryStream memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(str));
        //    BinaryFormatter formatter = new BinaryFormatter();
        //    return formatter.Deserialize(memoryStream);
        //}



        [Fact]
        public void SerializeDesearilize_Test()
        {




            //var cosa = new cosaX();
            // cosa.lista = new List<List<PointBody>>();
            // cosa.lista.Add(new List<PointBody> { new PointBody { x = "1", y = "1", color = "red", time = "001" } , new PointBody { x = "2", y = "2", color = "blue", time = "002" } });

            var cosa = new List<List<PointBody>>();
            cosa.Add(new List<PointBody> { new PointBody { x = 1, y = 1, color = "red", time = 001 }, new PointBody { x = 2, y = 2, color = "blue", time = 002 } });



            // Assuming obj is an instance of an object
            XmlSerializer ser = new XmlSerializer(cosa.GetType());
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.IO.StringWriter writer = new System.IO.StringWriter(sb);
            ser.Serialize(writer, cosa);

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


            //doc.Save()




            XmlDocument doc2 = new XmlDocument();
            doc2.LoadXml(txtToBDD);

            //Assuming doc is an XML document containing a serialized object and objType is a System.Type set to the type of the object.
            XmlNodeReader reader = new XmlNodeReader(doc2.DocumentElement);


            XmlSerializer ser2 = new XmlSerializer(cosa.GetType());
            object obj = ser2.Deserialize(reader);
            // Then you just need to cast obj into whatever type it is eg:

            //cosaX myObj = (cosaX)obj;

            List<List<PointBody>> myObj = (List<List<PointBody>>)obj;




            //var txt = SerilizeObject(cosa);


            //var cosa2 = DeSerilizeObject(txt) as cosaX;


            //txt = txt + "a";

            //  IFormatter formatter = new BinaryFormatter();

            //  Stream stream = new MemoryStream();


            //  formatter.Serialize(stream, cosa);

            //  StreamReader reader = new StreamReader(stream);

            //// byte[] bytes = new byte[stream.Length];
            //stream.Position = 0;

            ////var txt = stream.Read(bytes, 0, (int)stream.Length);
            //var txt = reader.ReadToEnd(); // .Read(bytes, 0, (int)stream.Length);

            //stream.Close();



            //// ********

            //IFormatter formatter2 = new BinaryFormatter();

            //var stream2 = new MemoryStream();
            //var writer = new StreamWriter(stream2);
            ////var writer = new BurstBinaryWriter(new TypeNameConverter() )
            //writer.Write(txt);
            //writer.Flush();
            //stream2.Position = 0;

            //// var txtRes = (List<string>)  formatter.Deserialize(stream2);
            //var cosares =  formatter2.Deserialize(stream2) as cosaX;

            //stream2.Close();

            ////Stream streamRes = new MemoryStream()










            //  var settings = new SharpSerializerBinarySettings();
            //  settings.IncludeAssemblyVersionInTypeName = true;
            //  settings.IncludeCultureInTypeName = true;
            //  settings.IncludePublicKeyTokenInTypeName = true;
            //  settings.Mode = BinarySerializationMode.Burst;


            //  var stream = new MemoryStream();
            //  // var settings = new SharpSerializerBinarySettings(BinarySerializationMode.SizeOptimized);

            //  var serializer = new SharpSerializer(settings);
            //  // var serializer = new SharpSerializer(true);




            //  //var Points = new List<List<PointBody>>();
            //  //Points.Add(new List<PointBody> { new PointBody { x="1",y="1", color ="red", time="0001" } });



            //var cosa = new List<string>();
            //cosa.Add("abc");



            // // serializer.Serialize(Points, stream);
            // serializer.Serialize(cosa, stream);


            // //StreamReader reader = new StreamReader(stream);
            // //// string text = reader.ReadToEnd();

            // byte[] bytes = new byte[stream.Length];
            // stream.Position = 0;
            // stream.Read(bytes, 0, (int)stream.Length);

            //  string data = Encoding.ASCII.GetString(bytes);


            //// var toBdd = reader.ReadToEnd();



            // Assert.True(data.Length > 0);



            // // ******



            // var serializer2 = new SharpSerializer(settings);

            // //byte[] byteArray = Encoding.ASCII.GetBytes(data);
            // //MemoryStream stream2 = new MemoryStream(byteArray);

            // var stream2 = new MemoryStream();
            // //var writer = new StreamWriter(stream2);
            // var writer = new BurstBinaryWriter(new TypeNameConverter() )
            // writer.Write(data);
            // writer.Flush();
            // stream2.Position = 0;
            //// return stream;



            // var cosa2 = new List<string>();



            // // stream.Position = 0;
            // //cosa2 = (List<string>) 
            // serializer2.Deserialize(stream2);


            // Assert.True(cosa2.Count > 0);


            //// Assert.Equal(Points, Points2);



            ////serializer.Deserialize(stream);

        }

        //public BurstBinaryReader(ITypeNameConverter typeNameConverter, Encoding encoding)
        //{
        //    if (typeNameConverter == null) throw new ArgumentNullException("typeNameConverter");
        //    if (encoding == null) throw new ArgumentNullException("encoding");
        //    _typeNameConverter = typeNameConverter;
        //    _encoding = encoding;
        //}


    }

   



     
}

//[Serializable]
//public class cosaX
//{
//    //public List<string> lista { get; set; }
//    public List<List<PointBody>> lista { get; set; }
//}