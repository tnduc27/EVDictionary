using EVDictionary.Models;
using EVDictionary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVDictionary.Services
{
    public class WordService : IWordService
    {
        private readonly IWordRepository _wordRepository;
        public WordService()
            {
            _wordRepository = new WordRepository();
            }

        public List<Word> SearchWords(string searchTerm)
        {
            return _wordRepository.SearchWords(searchTerm);
        }
        public List<Word> GetWords()
        {
            return _wordRepository.GetWords();
        }
        public void AddWords(Word w)
        {
            _wordRepository.AddWords(w);
        }
        public void UpdateWords(Word w)
        {
            _wordRepository.UpdateWords(w);
        }
        public void DeleteWords(int wordId) 
        {
            _wordRepository.DeleteWords(wordId);
        }
    }
}
