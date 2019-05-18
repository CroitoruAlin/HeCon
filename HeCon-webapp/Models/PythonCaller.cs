using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace HeCon_webapp.Models
{
    public class PythonCaller
    {
        private static PythonCaller python;

        public static PythonCaller getPythonCaller()
        {
            if (python == null)
                python = new PythonCaller();
            return python;

        }

        public Prediction callPython(string imagePath)
        {
            string url = "http://127.0.0.1:5000/";

            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-type", "application/json");
            request.RequestFormat = DataFormat.Json;
            RequestObject requestObject = new RequestObject();
            requestObject.name = imagePath;
            request.AddJsonBody(requestObject);

            var response = client.Execute<Prediction>(request);

            return response.Data;

        }
    }
}