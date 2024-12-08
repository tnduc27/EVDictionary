using EVDictionary.Models;
using EVDictionary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVDictionary.Services
{
    public class WordTypeService : IWordTypeService
    {
        private readonly IWordTypeRepository wordTypeRepository;
        public WordTypeService()
        {
            wordTypeRepository = new WordTypeRepository();
        }
        public List<WordType> GetWordTypes()
        {
           return wordTypeRepository.GetWordTypes();
        }
    }
}
