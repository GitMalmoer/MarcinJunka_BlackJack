using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Acess.Entities;

namespace Data_Access.Entities
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public ICollection<GameResolution> GameResolutions { get; set; }
        public ICollection<PlacedBet> PlacedBets { get; set; }

        public Game()
        {
            GameResolutions = new List<GameResolution>();
            PlacedBets = new List<PlacedBet>();
        }
    }
}
