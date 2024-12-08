using EVDictionary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVDictionary.DAO
{
    public class WordTypeDAO
    {
        public static List<WordType> GetWordTypes()
        {
            var list = new List<WordType>();
            try
            {
                using var db = new EvdictionaryContext();
                list = db.WordTypes.ToList(); 
            }
            catch (Exception ex) { }
            return list;
        }
    }
}
