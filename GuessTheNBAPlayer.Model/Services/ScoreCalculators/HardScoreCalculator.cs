using GuessTheNBAPlayer.Model.Entities.Enums;
using GuessTheNBAPlayer.Model.Entities.Internal;

namespace GuessTheNBAPlayer.Model.Services.ScoreCalculators
{
    public class HardScoreCalculator : IScoreCalculator
    {
        private readonly IPictureService _pictureService;

        public HardScoreCalculator(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }

        public Difficulty Difficulty => Difficulty.Hard;

        public bool CalculateScore(CalculateScoreArgs args, out int newScore)
        {
            newScore = args.CurrentScore;

            if (args.ChosenTeamID != args.Player.Team.ID)
            {
                newScore -= 3;
                return false;
            }

            var weight = HelperService.DeterminePlayerWeight(args.Player.WeightPounds);
            var height = HelperService.DeterminePlayerHeight(args.Player.HeightFeet, args.Player.HeightInches);
            var position = HelperService.DeterminePlayerPosition(args.Player.Position);
            var pictureUrl = _pictureService.GetFirstGoogleImage($"{args.Player.FirstName} {args.Player.LastName} NBA");

            if (height is "Unknown" || weight is "Unknown")
                newScore++;

            if (position is PlayerPosition.Unknown)
                newScore++;

            if (pictureUrl is "")
                newScore++;

            newScore += 2;
            return true;
        }
    }
}