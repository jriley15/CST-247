using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/*
 * Jordan Riley
 * 4-22-2018
 * SearchRequest Model class
 * Contains data fields and attributes for Search requests
 * 
 */




namespace Benchmark.Models
{
    public class SearchRequest
    {


        private String testamentSelection;
        private String bookSelection;
        private int chapterNumber;
        private int verseNumber;


        public SearchRequest(string testamentSelection, string bookSelection, int chapterNumber, int verseNumber)
        {
            this.TestamentSelection = testamentSelection;
            this.BookSelection = bookSelection;
            this.ChapterNumber = chapterNumber;
            this.VerseNumber = verseNumber;
        }

        public SearchRequest()
        {
            this.TestamentSelection = "";
            this.BookSelection = "";
            this.ChapterNumber = 0;
            this.VerseNumber = 0;
        }

        [Required]
        public string TestamentSelection { get => testamentSelection; set => testamentSelection = value; }

        [Required]
        public string BookSelection { get => bookSelection; set => bookSelection = value; }


        [Required]
        [RegularExpression(@"^[0-9]+$")]
        public int ChapterNumber { get => chapterNumber; set => chapterNumber = value; }


        [Required]
        [RegularExpression(@"^[0-9]+$")]
        public int VerseNumber { get => verseNumber; set => verseNumber = value; }


    }
}