using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic;

    public enum CardsEnum
    {
        c1 = 11,  // Ace of Clubs
        c2 = 2,   // Two of Clubs
        c3 = 3,   // Three of Clubs
        c4 = 4,   // Four of Clubs
        c5 = 5,   // Five of Clubs
        c6 = 6,   // Six of Clubs
        c7 = 7,   // Seven of Clubs
        c8 = 8,   // Eight of Clubs
        c9 = 9,   // Nine of Clubs
        c10 = 10, // Ten of Clubs
        cj = 10,  // Jack of Clubs
        cq = 10,  // Queen of Clubs
        ck = 10,  // King of Clubs

        d1 = 11,  // Ace of Diamonds
        d2 = 2,   // Two of Diamonds
        d3 = 3,   // Three of Diamonds
        d4 = 4,   // Four of Diamonds
        d5 = 5,   // Five of Diamonds
        d6 = 6,   // Six of Diamonds
        d7 = 7,   // Seven of Diamonds
        d8 = 8,   // Eight of Diamonds
        d9 = 9,   // Nine of Diamonds
        d10 = 10, // Ten of Diamonds
        dj = 10,  // Jack of Diamonds
        dq = 10,  // Queen of Diamonds
        dk = 10,  // King of Diamonds

        h1 = 11,  // Ace of Hearts
        h2 = 2,   // Two of Hearts
        h3 = 3,   // Three of Hearts
        h4 = 4,   // Four of Hearts
        h5 = 5,   // Five of Hearts
        h6 = 6,   // Six of Hearts
        h7 = 7,   // Seven of Hearts
        h8 = 8,   // Eight of Hearts
        h9 = 9,   // Nine of Hearts
        h10 = 10, // Ten of Hearts
        hj = 10,  // Jack of Hearts
        hq = 10,  // Queen of Hearts
        hk = 10,  // King of Hearts

        s1 = 11,  // Ace of Spades
        s2 = 2,   // Two of Spades
        s3 = 3,   // Three of Spades
        s4 = 4,   // Four of Spades
        s5 = 5,   // Five of Spades
        s6 = 6,   // Six of Spades
        s7 = 7,   // Seven of Spades
        s8 = 8,   // Eight of Spades
        s9 = 9,   // Nine of Spades
        s10 = 10, // Ten of Spades
        sj = 10,  // Jack of Spades
        sq = 10,  // Queen of Spades
        sk = 10   // King of Spades
}

public enum CardRank
{
    Ace = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Jack = 10,
    Queen = 10,
    King = 10,
}

public enum CardSuit
{
    h = 1, // heart
    d = 2, // diamonds
    c = 3, // clubs
    s = 4  // spades
}

