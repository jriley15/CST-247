using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



/*
 * Jordan Riley
 * 4-22-2018
 * ActionResponse Model class
 * Contains data fields for action responses
 * 
 */


namespace Benchmark.Models
{

    public class ActionResponse
    {


        private String response;

        public ActionResponse(string result)
        {
            this.Response = result;
        }

        public ActionResponse()
        {
            Response = "";
        }

        public string Response { get => response; set => response = value; }
    }
}