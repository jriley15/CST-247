using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/*
 * Jordan Riley
 * 4-22-2018
 * BibleVerse Model class
 * Contains data fields and attributes for Bible verses
 * 
 */


namespace Benchmark.Models
{
    public class BibleVerse
    {


        private String testamentSelection;
        private String bookSelection;
        private int chapterNumber;
        private int verseNumber;
        private String verseText;


        public BibleVerse(string testamentSelection, string bookSelection, int chapterNumber, int verseNumber, string verseText)
        {
            this.TestamentSelection = testamentSelection;
            this.BookSelection = bookSelection;
            this.ChapterNumber = chapterNumber;
            this.VerseNumber = verseNumber;
            this.VerseText = verseText;
        }

        public BibleVerse()
        {
            this.TestamentSelection = "";
            this.BookSelection = "";
            this.ChapterNumber = 0;
            this.VerseNumber = 0;
            this.VerseText = "";
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

        [Required]
        public string VerseText { get => verseText; set => verseText = value; }





    }
}