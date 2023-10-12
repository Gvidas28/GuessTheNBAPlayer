using GuessTheNBAPlayer.Model.Entities.Enums;

namespace GuessTheNBAPlayer.Model.Entities.Internal
{
    public class Player
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string PictureURL { get; set; }
    }
}