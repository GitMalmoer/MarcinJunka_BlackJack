using Business_Logic.MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcinJunka_BlackJack.DTOs
{
    class PersonDTO
    {
        public List<GameCard> Hand { get; set; }
        public int CurrentScore { get; set; }

        public int? Saldo { get; set; }
    }
}
