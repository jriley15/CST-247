using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/**
 * Error
 * Authors: Jordan Riley & Antonio Jabrail
 * Date: 4-29-2018
 * Contains Error model data fields
 * 
 */

namespace CLC.Models
{
    public class Error
    {

        /** Error model class **/

        private string content;

        public Error(string content)
        {
            this.content = content;
        }

        public string Content { get => content; set => content = value; }
    }
}