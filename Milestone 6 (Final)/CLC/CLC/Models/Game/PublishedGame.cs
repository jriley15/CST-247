﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/**
 * PublishedGame
 * Authors: Jordan Riley & Antonio Jabrail
 * Date: 4-29-2018
 * Contains stats model data fields
 * 
 */

namespace CLC.Models.Game
{

    [DataContract]
    public class PublishedGame
    {
        [DataMember]
        int id;
        int gridid;
        [DataMember]
        int userid;
        [DataMember]
        int clicks;

        public PublishedGame(int id, int gridid, int userid, int clicks)
        {
            this.id = id;
            this.gridid = gridid;
            this.userid = userid;
            this.clicks = clicks;
        }

        public int Id { get => id; set => id = value; }
        public int Gridid { get => gridid; set => gridid = value; }
        public int Userid { get => userid; set => userid = value; }
        public int Clicks { get => clicks; set => clicks = value; }
    }
}