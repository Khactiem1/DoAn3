using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTO;
using DAL;

namespace TKSR.Models
{
    public class ClassModels
    {
        public TaiKhoan user { get ; set; }
        public List<MuaThe> DSTheMua { get ; set ; }
    }
}