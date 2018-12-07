using System;
using System.Collections.Generic;
using System.Text;

namespace MDispatch.Models
{
    public class Ask
    {
        public int Id { get; set; }
        public string BodyAsk { get; set; }
        public string AnswerAsk { get; set; }
        public string DescriptionAsk { get; set; }
        public int Index { get; set; }

        public Ask(string bodyAsk, string answerAsk, string descriptionAsk, int index)
        {
            BodyAsk = bodyAsk;
            AnswerAsk = answerAsk;
            DescriptionAsk = descriptionAsk;
            Index = index;
        }
    }
}
