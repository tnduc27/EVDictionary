using EVDictionary.DAO;
using EVDictionary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVDictionary.Repositories
{
    public class WordTypeRepository : IWordTypeRepository
    {
        public List<WordType> GetWordTypes() => WordTypeDAO.GetWordTypes();
    }
}
