﻿using System;

namespace MovieApp.web.Entity
{

    public class WatchLaterMovie
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public DateTime AddedDate { get; set; }

        public Movie Movie { get; set; }
    }
}