namespace BackEndUpch.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Type { get; set; }      
        public int? Seats { get; set; }       
        public string Color { get; set; }     
        public string Notes { get; set; }     
    }
    public class CreateCarDto
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Type { get; set; }
        public int? Seats { get; set; }
        public string Color { get; set; }
        public string Notes { get; set; }
        public decimal? Price { get; set; }  // opcional si agregas Price
    }

}
