namespace FlatFileGenerator.DataGenerator.Business
{
    public static class NameGenerator
    {
        private static readonly Random Random = new();
        private static readonly string[] firstName = 
        [
            "Anne", "Mette", "Kirsten", "Hanne", "Anna", "Helle", "Maria", "Susanne", "Lene", "Marianne", "Charlotte",
            "Inge", "Lone", "Else", "Jette",
            "Karen", "Pia", "Bente", "Inger", "Birthe", "Louise", "Tina", "Camilla", "Lisbeth", "Mia", "Christina",
            "Anette", "Nina", "Randi", "Pernille", "Peter", "Michael", "Lars", "Thomas",
            "Jens", "Henrik", "Søren", "Christian", "Martin", "Jan", "Niels", "Anders", "Morten", "Jesper", "Hans",
            "Jørgen", "Mads", "Per", "Ole", "Rasmus", "Poul", "Erik", "Nikolaj", "Kim",
            "Carsten", "Bo", "Allan", "Tom", "Leif", "Bjarne"
        ];
        private static readonly string[] middleName = 
        [
            "Sky", "Storm", "Noor", "River", "Robin", "Charlie", "Kim", "Alex", "Bo", "Chris", "Morgan", "Billie", "Lou", 
            "Sam", "Sasha", "Taylor", "Skyler", "Jordan", "Phoenix", "Jesse", "Rene", "Lee", "Jamie", "Rio", "Quinn", "Casey", 
            "Ellis", "Sunny", "Angel", "Dakota", "Noel", "Sage", "Indigo", "Avery", "Blake", "Remy", "Ocean", "Marley", "Zion", 
            "Jules", "Lumi", "Miki", "Lex", "Stormy", "Tobi", "Riley", "Harley", "Luca", "Timo"
        ];
        private static readonly string[] lastName = 
        [
            "Jensen", "Nielsen", "Hansen", "Pedersen", "Andersen", "Christensen", "Larsen", "Sørensen", "Rasmussen",
            "Petersen", "Madsen", "Jørgensen", "Olsen", "Mortensen", "Poulsen", "Johansen", "Kristensen",
            "Thomsen", "Eriksen", "Mikkelsen", "Lund", "Andreasen", "Bentsen", "Christiansen", "Dahl", "Ebbesen", "Fischer", "Gade", "Hald", "Iversen",
            "Jørgensen", "Kjær", "Lind", "Møller", "Nielsen", "Olesen", "Petersen", "Qvist", "Rasmussen",
            "Sørensen", "Thomsen", "Uldal", "Vestergaard", "Wagner"
        ];

        public static string GetName() 
        {
            return firstName[Random.Next(firstName.Length)] + " " + middleName[Random.Next(middleName.Length)] + " " + lastName[Random.Next(lastName.Length)];
        }
    }
}
