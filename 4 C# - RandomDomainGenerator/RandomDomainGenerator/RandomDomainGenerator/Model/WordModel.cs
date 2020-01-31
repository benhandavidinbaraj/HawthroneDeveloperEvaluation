using System;
using System.Collections.Generic;
using System.Text;

namespace RandomDomainGenerator.Model
{
    public class WordModel
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { this._id = value; }
        }

        private string _word;
        public string Word
        {
            get { return _word; }
            set { this._word = value; }
        }

        public WordModel(int id, string word)
        {
            this.Id = id;
            this.Word = word;
        }
    }
}
