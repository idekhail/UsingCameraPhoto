using System;
using System.Collections.Generic;
using System.Text;

using SQLite;
namespace XF_UsingCamera.Model
{
    public class Users
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Img1 { get; set; }
        public string Img2 { get; set; }
    }
}
