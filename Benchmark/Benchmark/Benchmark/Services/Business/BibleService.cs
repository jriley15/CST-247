using Benchmark.Models;
using Benchmark.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/*
 * Jordan Riley
 * 4-22-2018
 * BibleService class
 * Contains basic functions for bible data manipulation and 
 * handles transfer of data to the DAO
 * 
 */


namespace Benchmark.Services.Business
{
    public class BibleService
    {



        public Boolean createVerse(BibleVerse bibleVerse)
        {

            BibleDAO bibleDAO = new BibleDAO();

            return bibleDAO.createVerse(bibleVerse);

        }


        public BibleVerse findVerse(SearchRequest searchRequest)
        {

            BibleDAO bibleDAO = new BibleDAO();

            return bibleDAO.findVerse(searchRequest);

        }



    }
}