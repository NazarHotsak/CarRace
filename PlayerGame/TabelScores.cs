using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRace
{
    internal class TabelScores
    {
        public const string pathScores = "scores.txt";
        public const string firstScoresOfPlayer = "0/0/0/0/0";
        public static int IndexNewScore = -1;

        public static int score { get; private set; }

        public static void AddScore()
        {
            score += 10;
        }

        public static int[] ParseTableScoreToArray()
        {
            string tableScores;
            using (StreamReader streamReader = new StreamReader(pathScores))
            {
                tableScores = streamReader.ReadToEnd();
            }

            int[] scores = new int[5];
            string[] str = tableScores.Split('/');
            for (int i = 0; i < str.Length; i++)
            {
                scores[i] = int.Parse(str[i]);
            }
            return scores;
        }

        public void UpdateTableScore()
        {

            int[] score = ParseTableScoreToArray();
            int smallerNumber = -1;

            for (int i = 0; i < score.Length; i++)
            {
                if (score[i] < TabelScores.score)
                {
                    smallerNumber = i;
                    IndexNewScore = i;
                    break;
                }
            }

            if (smallerNumber == -1)
            {
                return;
            }

            for (int i = 1, rearrange = score.Length - 2; i <= score.Length - (smallerNumber + 1); i++, rearrange--)
            {
                score[rearrange + 1] = score[rearrange];
            }
            score[smallerNumber] = TabelScores.score;

            StringBuilder NewTableScore = new StringBuilder();
            for (int i = 0; i < score.Length; i++)
            {
                NewTableScore.Append(score[i].ToString());
                if (score.Length - 1 != i)
                {
                    NewTableScore.Append('/');
                }
            }

            using StreamWriter streamWriter = new StreamWriter(pathScores, false);
            streamWriter.Write(NewTableScore);
        }
    }
}
