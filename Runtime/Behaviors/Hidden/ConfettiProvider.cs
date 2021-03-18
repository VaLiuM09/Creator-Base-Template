using System;
using UnityEngine;

public class ConfettiProvider : MonoBehaviour
{
    public const string DefaultConfettiPrefabPath = "Prefabs/InnoactiveConfettiMachine";
    
    public static readonly Season[] Seasons = new[]
    {
        new Season("Valentines Day", "", new DateTime(DateTime.Today.Year, 2, 14)),
        new Season("St. Patrick's Day", "Prefabs/StPatricksDayConfettiMachine", new DateTime(DateTime.Today.Year, 3, 17)),
        new Season("Christmas", "", new DateTime(DateTime.Today.Year, 12, 12), new DateTime(DateTime.Today.Year, 12, 25)),
        new Season("Halloween", "", new DateTime(DateTime.Today.Year, 10, 30))
    };

    public class Season
    {
        public string Name { get; }
        public string PathToConfetti { get; }
        public DateTime StartDate { get; }
        public DateTime? EndDate { get; }

        public Season(string name, string pathToConfetti, DateTime startDate)
        {
            Name = name;
            PathToConfetti = pathToConfetti;
            StartDate = startDate;
            EndDate = null;
        }
        
        public Season(string name, string pathToConfetti, DateTime startDate, DateTime endDate)
        {
            Name = name;
            PathToConfetti = pathToConfetti;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
    
    public static GameObject GetConfettiPrefab(bool allowSeasonal = true)
    {
        string path = DefaultConfettiPrefabPath;
        if (allowSeasonal)
        {
            Season season = GetCurrentSeason();
            if (season != null)
            {
                path = season.PathToConfetti;
            }
        }
        return Resources.Load<GameObject>(path);
    }

    private static Season GetCurrentSeason()
    {
        DateTime now = DateTime.Now;
        int day = DateTime.Today.Day;
        int month = DateTime.Today.Month;
        
        foreach (Season season in Seasons)
        {
            if (season.StartDate.Day == day && season.StartDate.Month == month)
            {
                return season;
            }

            if (season.EndDate != null)
            {
                if ((now > season.StartDate) && (now < season.EndDate))
                {
                    return season;
                }
            }
        }

        return null;
    }
}
